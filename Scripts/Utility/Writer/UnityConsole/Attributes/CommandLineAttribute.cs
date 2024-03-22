using System;

namespace Writer.CommandConsole
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CommandLineAttribute : Attribute
    {
        public string tag;
        public string methodName;
        public string commandFormat;
        public string[] parameterTypeNames;
        
        public CommandLineAttribute(string commandFormat, params string[] parameterTypeNames)
        {
            string[] line = commandFormat.Split('.');
            tag = line[0];
            methodName = line[1];
            this.commandFormat = commandFormat;
            this.parameterTypeNames = parameterTypeNames;
        }
    }
}