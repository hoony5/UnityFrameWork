using System.Reflection;

namespace Writer.CommandConsole
{
    public class CommandMethodInfo
    {
        public string tag;
        public string command;
        public string description;
        public string[] parameterTypeNames;
        public MethodInfo methodInfo;
    }
}