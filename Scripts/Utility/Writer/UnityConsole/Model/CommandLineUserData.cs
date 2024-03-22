using System;

namespace Writer.CommandConsole
{
    public class CommandLineUserData
    {
        private Type Type;
        private object Value;
        
        public readonly bool IsNullOrEmpty;
        
        public CommandLineUserData(Type type, object value)
        {
            if (type == null || value == null)
            {
                IsNullOrEmpty = true;
                return;
            }

            Type = type;
            IsNullOrEmpty = false;
            Value = value;
        }
        
        public T GetValue<T>()
        {
            return (T)Value;
        }
    }

}