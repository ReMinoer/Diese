using System;
using System.Collections.Generic;
using System.Linq;
using NLua;

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
            lua.GetFunction("CoroutineManager.Create").Call(name, function);
        }

        static public void CreateCoroutine(this NLua.Lua lua, string functionPath, string substituteName = null)
        {
            CreateCoroutine(lua, lua.GetFunction(functionPath), substituteName ?? functionPath);
        }

        static public LuaCoroutineResult ResumeCoroutine(this NLua.Lua lua, string name)
        {
            object[] results = lua.GetFunction("CoroutineManager.Resume").Call(name);
            return (bool)results[0]
                ? new LuaCoroutineResult {IsValid = true, Results = results.Skip(1).ToArray()}
                : new LuaCoroutineResult {IsValid = false, ErrorMessage = (string)results[1]};
        }

        static public object[] ResumeCoroutine(this NLua.Lua lua, string name, out bool isValid)
        {
            object[] results = lua.GetFunction("CoroutineManager.Resume").Call(name);
            isValid = (bool)results[0];
            return results.Skip(1).ToArray();
        }

        static public LuaCoroutineStatus StatusCoroutine(this NLua.Lua lua, string name)
        {
            var status = (string)lua.GetFunction("CoroutineManager.Status").Call(name).First();
            LuaCoroutineStatus result;
            Enum.TryParse(status, true, out result);
            return result;
        }

        static public Dictionary<string, LuaCoroutineResult> UpdateCoroutines(this NLua.Lua lua)
        {
            var coroutineResults = new Dictionary<string, LuaCoroutineResult>();
            object[] callResult = lua.GetFunction("CoroutineManager.Update").Call();

            var table = callResult.First() as LuaTable;
            Dictionary<object, object> tempDico = lua.GetTableDict(table);
            foreach (KeyValuePair<object, object> pair in tempDico)
            {
                var table2 = (LuaTable)pair.Value;
                var values = new object[table2.Values.Count];
                table2.Values.CopyTo(values, 0);

                var result = new LuaCoroutineResult {IsValid = (bool)values[0]};
                if (result.IsValid)
                    result.Results = values.Skip(1).ToArray();
                else
                    result.ErrorMessage = (string)values[1];

                coroutineResults.Add((string)pair.Key, result);
            }

            return coroutineResults;
        }

        static public bool ExistsCoroutine(this NLua.Lua lua, string name)
        {
            return (bool)lua.GetFunction("CoroutineManager.Exists").Call(name).First();
        }
    }
}