using System;

namespace Share
{
    public static class TypeEx
    {
        public static bool IsBasicType(this string typeName)
        {
            switch (typeName.ToLower())
            {
                case "string":
                case "int":
                case "int32":
                case "long":
                case "int64":
                case "bool":
                case "boolean":
                case "float":
                case "single":
                case "double":
                case "decimal":
                case "char":
                case "byte":
                case "sbyte":
                case "short":
                case "int16":
                case "ushort":
                case "uint16":
                case "uint":
                case "uint32":
                case "ulong":
                case "uint64":
                    return true;
                default:
                    return false;
            }
        }
        public static Type GetBasicType(this string input)
        {
            switch (input.ToLower())
            {
                case "string":
                    return Type.GetType("System.String");
                case "int":
                case "int32":
                    return Type.GetType("System.Int32");
                case "long":
                case "int64":
                    return Type.GetType("System.Int64");
                case "bool":
                case "boolean":
                    return Type.GetType("System.Boolean");
                case "float":
                case "single":
                    return Type.GetType("System.Single");
                case "double":
                    return Type.GetType("System.Double");
                case "decimal":
                    return Type.GetType("System.Decimal");
                case "char":
                    return Type.GetType("System.Char");
                case "byte":
                    return Type.GetType("System.Byte");
                case "sbyte":
                    return Type.GetType("System.SByte");
                case "short":
                case "int16":
                    return Type.GetType("System.Int16");
                case "ushort":
                case "uint16":
                    return Type.GetType("System.UInt16");
                case "uint":
                case "uint32":
                    return Type.GetType("System.UInt32");
                case "ulong":
                case "uint64":
                    return Type.GetType("System.UInt64");
                default:
                    throw new ArgumentException($"Unsupported basic type: {input}");
            }
        }
    }
}