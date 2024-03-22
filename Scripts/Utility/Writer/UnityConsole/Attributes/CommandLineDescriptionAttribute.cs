using System;

namespace Writer.CommandConsole
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CommandLineDescriptionAttribute : Attribute
    {
        public string description;
        public CommandLineDescriptionAttribute(string description)
        {
            this.description = description;
        }
    }
}