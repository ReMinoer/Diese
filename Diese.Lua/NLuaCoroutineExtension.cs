using System;
using System.Collections.Generic;
using System.Linq;
using NLua;
using NLua.Exceptions;

namespace Diese.Lua
{
    static public class NLuaCoroutineExtension
    {
        static public void DoCoroutineManager(this NLua.Lua lua)
        {
            lua.DoString(LuaTools.CoroutineManager);
        }

        static public void CreateCoroutine(this NLua.Lua lua, LuaFunction function, string name)
        {
            lua.GetFunction("coroutine.manager.create").Call(function, name);
        }

        static public void CreateCoroutine(this NLua.Lua lua, string functionPath, string substituteName = null)
        {
            CreateCoroutine(lua, lua.GetFunction(functionPath), substituteName ?? functionPath);
        }

        static public LuaCoroutineResult ResumeCoroutine(this NLua.Lua lua, string name)
        {
            object[] luaResult = lua.GetFunction("coroutine.manager.resume").Call(name, 0);
            object[] results = ExtractResultsFromTable((LuaTable)luaResult[0]);

            LuaCoroutineResult coroutineResult = (bool)results[0]
                ? new LuaCoroutineResult {IsValid = true, Results = results.Skip(1).ToArray()}
                : new LuaCoroutineResult {IsValid = false, ErrorMessage = (string)results[1]};

            return coroutineResult;
        }

        static public LuaCoroutineResult ResumeCoroutine(this NLua.Lua lua, double elapsedTime, string name)
        {
            object[] luaResult = lua.GetFunction("coroutine.manager.resume").Call(name, elapsedTime);
            object[] results = ExtractResultsFromTable((LuaTable)luaResult[0]);

            LuaCoroutineResult coroutineResult = (bool)results[0]
                ? new LuaCoroutineResult {IsValid = true, Results = results.Skip(1).ToArray()}
                : new LuaCoroutineResult {IsValid = false, ErrorMessage = (string)results[1]};

            return coroutineResult;
        }

        static public object[] ResumeCoroutine(this NLua.Lua lua, string name, out bool isValid)
        {
            object[] luaResult = lua.GetFunction("coroutine.manager.resume").Call(name, 0);
            object[] results = ExtractResultsFromTable((LuaTable)luaResult[0]);
            isValid = (bool)results[0];
            return results.Skip(1).ToArray();
        }

        static public LuaCoroutineStatus StatusCoroutine(this NLua.Lua lua, string name)
        {
            var status = (string)lua.GetFunction("coroutine.manager.status").Call(name).First();
            LuaCoroutineStatus result;
            Enum.TryParse(status, true, out result);
            return result;
        }

        static public Dictionary<string, LuaCoroutineResult> UpdateCoroutines(this NLua.Lua lua, double elapsedTime = 0)
        {
            var coroutineResults = new Dictionary<string, LuaCoroutineResult>();
            object[] callResult = lua.GetFunction("coroutine.manager.update").Call(elapsedTime);

            var table = callResult.First() as LuaTable;
            Dictionary<object, object> tempDico = lua.GetTableDict(table);
            foreach (KeyValuePair<object, object> pair in tempDico)
            {
                object[] values = ExtractResultsFromTable((LuaTable)pair.Value);

                LuaCoroutineResult coroutineResult;
                if ((bool)values[0])
                    coroutineResult = new LuaCoroutineResult {IsValid = true, Results = values.Skip(1).ToArray()};
                else
                {
                    var exception = values[1] as LuaScriptException;
                    if (exception != null)
                        coroutineResult = new LuaCoroutineResult
                        {
                            IsValid = false,
                            ErrorMessage = exception.Source + exception.Message
                        };
                    else
                        coroutineResult = new LuaCoroutineResult {IsValid = false, ErrorMessage = values[1].ToString()};
                }

                coroutineResults.Add((string)pair.Key, coroutineResult);
            }

            return coroutineResults;
        }

        static public bool ExistsCoroutine(this NLua.Lua lua, string name)
        {
            return (bool)lua.GetFunction("coroutine.manager.exists").Call(name).First();
        }

        static private object[] ExtractResultsFromTable(LuaTable luaTable)
        {
            var results = new object[luaTable.Values.Count];
            luaTable.Values.CopyTo(results, 0);
            return results;
        }
    }
}