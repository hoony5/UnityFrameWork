using System;
using System.Collections.Generic;
using UnityEngine;

namespace Share
{
    public static class ParseEx
    {
        private static char[] separators = { ',', '(', ')','[',']' };
        private static Dictionary<Type, Func<string, object>> parsers = new Dictionary<Type, Func<string, object>>
        {/*
            
            case "double":
            case "char":
            case "sbyte":
            case "datetime":
            case "object":*/
            { typeof(int), s => int.TryParse(s.Trim(separators), out int result) ? result : 0 },
            { typeof(float), s => float.TryParse(s.Trim(separators), out float result) ? result : 0 },
            { typeof(string), s => s.Trim(separators) },
            { typeof(bool), s => bool.TryParse(s.Trim(separators), out bool result) && result },
            { typeof(double), s => Boolean.TryParse(s.Trim(separators), out Boolean result) && result},
            { typeof(long), s => long.TryParse(s.Trim(separators), out long result) ? result : 0},
            { typeof(short), s => short.TryParse(s.Trim(separators), out short result) ? result : 0},
            { typeof(uint), s => uint.TryParse(s.Trim(separators), out uint result) ? result : 0},
            { typeof(ushort), s => ushort.TryParse(s.Trim(separators), out ushort result) ? result : 0},
            { typeof(ulong), s => ulong.TryParse(s.Trim(separators), out ulong result) ? result : 0},
            { typeof(byte), s => byte.TryParse(s.Trim(separators), out byte result) ? result : 0},
            { typeof(char), s => char.TryParse(s.Trim(separators), out char result) ? result : 0},
            { typeof(sbyte), s => sbyte.TryParse(s.Trim(separators), out sbyte result) ? result : 0},
            { typeof(decimal), s => decimal.TryParse(s.Trim(separators), out decimal result) ? result : 0},
            { typeof(Vector2), s =>
            {
                string[] values = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 2)
                {
                    if (float.TryParse(values[0], out float x) && float.TryParse(values[1], out float y))
                    {
                        return new Vector2(x, y);
                    }
                }
                return null;
            } },
            { typeof(Vector3), s =>
            {
                string[] values = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 3)
                {
                    if (float.TryParse(values[0], out float x) && float.TryParse(values[1], out float y) &&
                        float.TryParse(values[2], out float z))
                    {
                        return new Vector3(x, y, z);
                    }
                }
                return null;
            } },
            { typeof(Vector4), s =>
            {
                string[] values = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 4)
                {
                    if (float.TryParse(values[0], out float x) && float.TryParse(values[1], out float y) &&
                        float.TryParse(values[2], out float z) && float.TryParse(values[3], out float w))
                    {
                        return new Vector4(x, y, z, w);
                    }
                }
                return null;
            } },
            { typeof(Color), s =>
            {
                string[] values = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 4)
                {
                    if (float.TryParse(values[0], out float r) && float.TryParse(values[1], out float g) &&
                        float.TryParse(values[2], out float b) && float.TryParse(values[3], out float a))
                    {
                        return new Color(r, g, b, a);
                    }
                }

                if (!s.StartsWith('#')) s = $"#{s}";
                if (ColorUtility.TryParseHtmlString(s, out Color result))
                {
                    return result;
                }
                return null;
            } }
        };
        
        public static object Parse(this Type type, string value)
        {
            if (parsers.TryGetValue(type, out var parser))
            {
                return parser.Invoke(value);
            }

            throw new NotSupportedException($"Parsing not supported for type: {type}");
        }
    }
}