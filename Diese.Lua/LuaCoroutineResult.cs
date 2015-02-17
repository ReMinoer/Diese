namespace Diese.Lua
{
    public struct LuaCoroutineResult
    {
        public bool IsValid { get; set; }
        public object[] Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}