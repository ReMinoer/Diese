using System.Collections.Generic;
using NUnit.Framework;

namespace Diese.Lua.Test
{
    public class CoroutineManagerTest
    {
        [Test]
        public void RunCoroutine()
        {
            using (var lua = new NLua.Lua())
            {
                lua.DoCoroutineManager();
                lua.DoString("function test() return 'truc', 2 end");

                lua.CreateCoroutine("test");
                bool isValid;
                object[] results = lua.ResumeCoroutine("test", out isValid);

                Assert.IsTrue(isValid);
                Assert.IsTrue((string)results[0] == "truc");
                Assert.IsTrue((int)(double)results[1] == 2);
            }
        }

        [Test]
        public void ResumeCoroutine()
        {
            using (var lua = new NLua.Lua())
            {
                lua.DoCoroutineManager();
                lua.DoString("function test() local x = 1; coroutine.yield(x); return 2 end");

                lua.CreateCoroutine("test");
                bool isValid;
                object[] results = lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(isValid);
                Assert.IsTrue((int)(double)results[0] == 1);

                results = lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(isValid);
                Assert.IsTrue((int)(double)results[0] == 2);

                results = lua.ResumeCoroutine("test", out isValid);
                Assert.IsFalse(isValid);
                Assert.IsTrue((string)results[0] == "cannot resume dead coroutine");
            }
        }

        [Test]
        public void ResumeCoroutineBis()
        {
            using (var lua = new NLua.Lua())
            {
                lua.DoCoroutineManager();
                lua.DoString("function test() x = 1; coroutine.yield(x); return 2 end");

                lua.CreateCoroutine("test");
                LuaCoroutineResult result = lua.ResumeCoroutine("test");

                Assert.IsTrue(result.IsValid);
                Assert.IsTrue((int)(double)result.Results[0] == 1);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Suspended);

                result = lua.ResumeCoroutine("test");

                Assert.IsTrue(result.IsValid);
                Assert.IsTrue((int)(double)result.Results[0] == 2);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);

                result = lua.ResumeCoroutine("test");

                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ErrorMessage == "cannot resume dead coroutine");
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);
            }
        }

        [Test]
        public void UpdateCoroutines()
        {
            using (var lua = new NLua.Lua())
            {
                lua.LoadCLRPackage();
                lua.DoCoroutineManager();
                lua.DoString("function test() return 'hi' end " + "function test2() coroutine.yield() return true end "
                             + "function test3() return 3,4 end");

                lua.CreateCoroutine("test");
                lua.CreateCoroutine("test2");
                lua.CreateCoroutine("test3");

                Dictionary<string, LuaCoroutineResult> results = lua.UpdateCoroutines();

                Assert.IsTrue(results["test"].IsValid);
                Assert.IsTrue((string)results["test"].Results[0] == "hi");
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);

                Assert.IsTrue(results["test2"].IsValid);
                Assert.IsTrue(lua.StatusCoroutine("test2") == LuaCoroutineStatus.Suspended);

                Assert.IsTrue(results["test3"].IsValid);
                Assert.IsTrue((int)(double)results["test3"].Results[0] == 3);
                Assert.IsTrue((int)(double)results["test3"].Results[1] == 4);
                Assert.IsTrue(lua.StatusCoroutine("test3") == LuaCoroutineStatus.Dead);

                results = lua.UpdateCoroutines();

                Assert.IsFalse(results["test"].IsValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);

                Assert.IsTrue(results["test2"].IsValid);
                Assert.IsTrue((bool)results["test2"].Results[0]);
                Assert.IsTrue(lua.StatusCoroutine("test2") == LuaCoroutineStatus.Dead);

                Assert.IsFalse(results["test3"].IsValid);
                Assert.IsTrue(lua.StatusCoroutine("test3") == LuaCoroutineStatus.Dead);
            }
        }

        [Test]
        public void SleepCoroutine()
        {
            using (var lua = new NLua.Lua())
            {
                lua.LoadCLRPackage();
                lua.DoCoroutineManager();
                lua.DoString("function test() coroutine.manager.sleep(10, 'stopped 1') return 'hi' end " +
                             "function test2() coroutine.manager.sleep(20, 'stopped 2') return 'hello' end ");

                lua.CreateCoroutine("test");
                lua.CreateCoroutine("test2");

                Dictionary<string, LuaCoroutineResult> results = lua.UpdateCoroutines();

                Assert.IsTrue(results["test"].IsValid);
                Assert.IsTrue((string)results["test"].Results[0] == "stopped 1");
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Sleeping);

                Assert.IsTrue(results["test2"].IsValid);
                Assert.IsTrue((string)results["test2"].Results[0] == "stopped 2");
                Assert.IsTrue(lua.StatusCoroutine("test2") == LuaCoroutineStatus.Sleeping);

                results = lua.UpdateCoroutines(10);

                Assert.IsTrue(results["test"].IsValid);
                Assert.IsTrue((string)results["test"].Results[0] == "hi");
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);

                Assert.IsTrue(results["test2"].IsValid);
                Assert.IsTrue(lua.StatusCoroutine("test2") == LuaCoroutineStatus.Sleeping);

                results = lua.UpdateCoroutines(10);

                Assert.IsFalse(results["test"].IsValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);

                Assert.IsTrue(results["test2"].IsValid);
                Assert.IsTrue((string)results["test2"].Results[0] == "hello");
                Assert.IsTrue(lua.StatusCoroutine("test2") == LuaCoroutineStatus.Dead);
            }
        }

        [Test]
        public void GetStatus()
        {
            using (var lua = new NLua.Lua())
            {
                lua.DoCoroutineManager();
                lua.DoString("function test() coroutine.yield() coroutine.yield() coroutine.yield() end");

                lua.CreateCoroutine("test");
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Suspended);

                bool isValid;
                lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Suspended);
                lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Suspended);
                lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Suspended);
                lua.ResumeCoroutine("test", out isValid);
                Assert.IsTrue(lua.StatusCoroutine("test") == LuaCoroutineStatus.Dead);
            }
        }

        [Test]
        public void GetExists()
        {
            using (var lua = new NLua.Lua())
            {
                lua.DoCoroutineManager();
                lua.DoString("function test() coroutine.yield(1) coroutine.yield(2) coroutine.yield(3) end "
                             + "function test2() coroutine.yield(4) coroutine.yield(5) coroutine.yield(6) end");

                lua.CreateCoroutine("test");
                Assert.IsTrue(lua.ExistsCoroutine("test"));
                Assert.IsFalse(lua.ExistsCoroutine("test2"));

                lua.CreateCoroutine("test2");
                Assert.IsTrue(lua.ExistsCoroutine("test"));
                Assert.IsTrue(lua.ExistsCoroutine("test2"));
            }
        }
    }
}