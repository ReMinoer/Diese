using System.Linq;

namespace Diese.Lua
{
    public struct LuaCoroutineResult
    {
        public bool IsValid { get; set; }
        public object[] Results { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return IsValid
                ? Results.Any()
                    ? "Results : " + Results.Aggregate((a, b) => a.ToString() + " " + b.ToString())
                    : ""
                : "Error : " + ErrorMessage;
        }
    }
}