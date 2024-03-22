using System;
using Share;

namespace Writer.CommandConsole
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CommandTargetAttribute : Attribute
    {
        public string tag;
        public Type type;
        public bool containsAll;
        public CommandTargetAttribute(Type type, bool containsAll = false, string tag = "")
        {
            this.type = type;
            this.tag = tag.IsNullOrEmptyThen(type.Name);
            this.containsAll = containsAll;
        }
    }
}