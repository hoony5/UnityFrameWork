using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Share
{
    public enum IntegerToStringFormat
    {
        None,
        WithCommas,
    }
    public static class StringEx
    {
        private static readonly char[] Numbers = new [] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            
        private static readonly char[] Characters = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
            'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
            'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', '!' , '@', '#', '$', '%', '^', '&', '*',
        };
        public static string GetRandomCharacters(int count)
        {
            if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, null);
            Span<char> result = stackalloc char[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Characters[UnityEngine.Random.Range(0, Characters.Length)];
            }
            return result.ToString();
        }

        public static string SplitToOneLine(this string value, params char[] sperators)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (sperators.Length is 0) return value;

            string[] split = value.Split(sperators, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(",", split);
        }

        public static string Join(this string sperator, IEnumerable<string> value)
        {
            return string.Join(sperator, value);
        }
        public static string Join(this char sperator, IEnumerable<string> value)
        {
            return string.Join(sperator, value);
        }

        public static string ToLowerString<T>(this T e) where T : Enum
        {
            return e.ToString().ToLower();
        }
        
        public static string ToUpperString<T>(this T e) where T : Enum
        {
            return e.ToString().ToUpper();
        }
        
        public static string ToLowerAt(this string str, int index)
        {
            if (index < 0 || index >= str.Length) return str;
            return str.Substring(0, index) + char.ToLower(str[index]) + str.Substring(index + 1);
        }
        
        public static string ToUpperAt(this string str, int index)
        {
            if (index < 0 || index >= str.Length) return str;
            return str.Substring(0, index) + char.ToUpper(str[index]) + str.Substring(index + 1);
        }
        
        public static string Replace(this string str, string oldValue, string newValue)
        {
            return str.Replace(oldValue, newValue);
        }
        
        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            return str.Replace(oldValue, newValue, comparison);
        }
        
        public static string Replace(this string str, char oldChar, char newChar)
        {
            return str.Replace(oldChar, newChar);
        }
        
        public static string RemoveSpace(this string str)
        {
            return str.Replace(" ", "");
        }
        
        public static string IsNotNullOrEmptyThen(this string str, string then)
        {
            return !string.IsNullOrEmpty(str) ? then : str;
        }
        
        public static string IsNullOrEmptyThen(this string str, Func<string> then)
        {
            return string.IsNullOrEmpty(str) ? then() : str;
        }
        
        public static string IsNotNullOrEmptyThen(this string str, Func<string> then)
        {
            return !string.IsNullOrEmpty(str) ? then() : str;
        }
        
        public static string IsNullOrEmptyThen(this string str, Func<string, string> then)
        {
            return string.IsNullOrEmpty(str) ? then(str) : str;
        }
        
        public static string IsNotNullOrEmptyThen(this string str, Func<string, string> then)
        {
            return !string.IsNullOrEmpty(str) ? then(str) : str;
        }
        public static string BoolToString(this bool value)
        {
            return value ? "true" : "false";
        }
        public static string IntToStringWithUnit(this int number, string unit, IntegerToStringFormat format = IntegerToStringFormat.None)
        {
            if(string.IsNullOrEmpty(unit) || string.IsNullOrWhiteSpace(unit)) return number.IntToString(format);

            string intToString = number.IntToString(format);
            Span<char> result = stackalloc char[intToString.Length + unit.Length];
            intToString.AsSpan().CopyTo(result);
            unit.AsSpan().CopyTo(result[intToString.Length..]);
            return result.ToString();
        }

        public static string IntToString(this int number, IntegerToStringFormat format = IntegerToStringFormat.None)
        {
            int digitLength = GetIntegerDigitLegnth(number);
            bool isMinus = number < 0;
            int numberLength = format is IntegerToStringFormat.None
                ? digitLength
                : digitLength + CommaCount(digitLength);
            if (isMinus) numberLength++;

            Span<char> result = stackalloc char[numberLength];

            int startPosition = 0;
            CountInteger(number, format, result, startPosition, result.Length);
            return result.ToString();
        }


        #region Count Number Util Methods
        private static void CountInteger(int num, IntegerToStringFormat format, Span<char> result, int currentPosition, int endPosition)
        {
            if (currentPosition == endPosition)
            {
                if(num < 0)
                    result[0] = '-';
                else
                    return;
            }
            int temp = num % 10;
            bool isCommaPosition = currentPosition != 0 &&
                                   format is IntegerToStringFormat.WithCommas &&
                                   (currentPosition + 1) % 4 == 0;
            if (isCommaPosition)
            {
                result[^(currentPosition + 1)] = ',';
                currentPosition++;
            }
            result[^(currentPosition + 1)] = Numbers[temp];
            int next = num - temp;
            if (next >= 10) next /= 10;
            currentPosition++;
            CountInteger(next, format, result, currentPosition, endPosition);
        }
        private static int CommaCount(int digit)
        {
            digit -= 3;
            if (digit <= 0) return 0;
            return 1 + CommaCount(digit);
        }
        private static int GetIntegerDigitLegnth(int num)
        {
            int digit = 0;
            num = Mathf.Abs(num);
            while(num > 0)
            {
                num /= 10;
                digit++;
            }
            return digit is 0 ? 1 : digit;
        }

        #endregion

        public static string Erase(this string value, string part, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            return value.Contains(part, comparisonType) ? value.Replace(part, string.Empty).Trim() : value;
        }
        public static bool ValidateLength(this string value, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            return value.Length >= minLength && value.Length <= maxLength;
        }
        public static bool ValidateLength(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            return value.Length == length;
        }
        public static bool ExistDigit(this string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i])) return true;
            }
            return false;
        }
    
        public static bool ExistAlphabet(this string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetter(value[i])) return true;
            }
            return false;
        }
        public static bool ExistUpper(this string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetter(value[i]) && char.IsUpper(value[i])) return true;
            }
            return false;
        }
        public static bool ExistSpecialCharacter(this string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetterOrDigit(value[i])) return true;
            }
            return false;
        }
        public static bool ExistWhiteSpace(this string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i])) return true;
            }
            return false;
        }
        public static float ToFloat(this string value, IFormatProvider formatProvider = null)
        {
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException($"{nameof(value)} is null or empty");

            return float.TryParse(value,NumberStyles.Float, formatProvider, out float fNumber) ? fNumber : 0;
        }
        public static int ToInt(this string value, IFormatProvider formatProvider = null)
        {
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException($"{nameof(value)} is null or empty");

            return int.TryParse(value,NumberStyles.Integer, formatProvider, out int iNumber) ? iNumber : 0;
        }
        public static bool ToBool(this string value)
        {
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException($"{nameof(value)} is null or empty");

            return (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value is "1") && !string.IsNullOrEmpty(value);
        }
        public static object ToEnum(this string value, Type type)
        {
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException($"{nameof(value)} is null or empty");

            return Enum.TryParse(type, value, out object result) ? result : null;
        }
    
        public static byte[] FromBase64ToByteArray(this string value)
        {
            return Convert.FromBase64String(value);
        }
        public static string ToBase64(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }
        public static byte[] UTF8ToByteArray(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
        public static string ToUTF8String(this byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }
        public static string IsNullOrEmptyThen(this string value, string alternative)
        {
            return string.IsNullOrEmpty(value) ? alternative : value;
        }
        public static string IsNullOrWhiteSpaceThen(this string value, string alternative)
        {
            return string.IsNullOrWhiteSpace(value) ? alternative : value;
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static string Remove(this string value, string remove)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (string.IsNullOrEmpty(remove)) return value;

            if (value.Length < remove.Length || !value.Contains(remove)) return value;

            ReadOnlySpan<char> original = value.AsSpan();
            ReadOnlySpan<char> removeSpan = remove.AsSpan();

            int removeStartIndex = original.IndexOf(removeSpan);
            int removeEndIndex = removeSpan.Length - 1;

            Span<char> result = stackalloc char[value.Length - removeSpan.Length];
            original[..removeStartIndex].CopyTo(result);
            original[(removeStartIndex + removeEndIndex + 1)..].CopyTo(result[removeStartIndex..]);
            return result.ToString();
        }
        public static string AddPrefix(this string value, string prefix)
        {
            Span<char> result = stackalloc char[value.Length + prefix.Length];
            prefix.AsSpan().CopyTo(result);
            value.AsSpan().CopyTo(result[prefix.Length..]);
            return result.ToString();
        }
        public static string Append(this string value, string other)
        {
            Span<char> result = stackalloc char[value.Length + other.Length];
            value.AsSpan().CopyTo(result);
            other.AsSpan().CopyTo(result[value.Length..]);
            return result.ToString();
        }

        public static void Build(this string value, out string target)
        {
            target = value;
        }
        public static string AppendLine(this string value, string other)
        {
            Span<char> result = stackalloc char[value.Length + other.Length + 1];
            value.AsSpan().CopyTo(result);
            other.AsSpan().CopyTo(result[value.Length..]);
            result[value.Length + other.Length] = '\n';
            return result.ToString();
        }
        public static string AppendLine(this string value)
        {
            Span<char> result = stackalloc char[value.Length + 1];
            value.AsSpan().CopyTo(result);
            result[value.Length] = '\n';
            return result.ToString();
        }

        public static string Append(this string value, string other, string separator = "")
        {
            Span<char> result = stackalloc char[value.Length + other.Length + separator.Length];
            value.AsSpan().CopyTo(result);
            int next = value.Length;
            if (!separator.IsNullOrEmpty())
            {
                separator.AsSpan().CopyTo(result[next..]);
                next += separator.Length;
            }
            other.AsSpan().CopyTo(result[next..]);
            return result.ToString();
        }

        public static bool SequenceEqualAsSpan(this string value, string other)
        {
            ReadOnlySpan<char> vSpan = value.AsSpan();
            ReadOnlySpan<char> oSpan = other.AsSpan();
            for(int i = 0; i < vSpan.Length; i++)
            {
                if (vSpan[i] != oSpan[i]) return false;
            }
            return true;
        }
        public static string TrimStart(this string value, string startStr)
        {
            ReadOnlySpan<char> ros = value.AsSpan();
            if(!startStr.SequenceEqualAsSpan(ros[..startStr.Length].ToString())) return value;

            Span<char> result = stackalloc char[value.Length - startStr.Length];
            ros[startStr.Length..].CopyTo(result);
            return result.ToString();
        }

        public static string TrimEnd(this string value, string endStr)
        {
            ReadOnlySpan<char> ros = value.AsSpan();
            if(!endStr.SequenceEqualAsSpan(ros[^endStr.Length..].ToString())) return value;

            Span<char> result = stackalloc char[value.Length - endStr.Length];
            ros[..^endStr.Length].CopyTo(result);
            return result.ToString();
        }
        public static bool Contains(this string value, string value2,
            StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(value))  return false;
            if (string.IsNullOrEmpty(value2)) return false;

            return value.Contains(value2, comparisonType);
        }
        public static string ReplacePosition(this string value, char replace, int position)
        {
            if (string.IsNullOrEmpty(value)) return value;

            if (position < 0 || position >= value.Length) throw new ArgumentOutOfRangeException(nameof(position), position, null);
            Span<char> result = value.ToCharArray();
            result[position] = replace;
            return result.ToString();
        }

        public static string PlaceBetween(this string value, string prefix, string suffix)
        {
            Span<char> result = stackalloc char[value.Length + prefix.Length + suffix.Length];
            prefix.AsSpan().CopyTo(result);
            value.AsSpan().CopyTo(result[prefix.Length..]);
            suffix.AsSpan().CopyTo(result[(prefix.Length + value.Length)..]);
            return result.ToString();
        }
        public static int GetHashCodeEx(this string value)
        {
            return HashCode.Combine(value);
        }

    }

    public static class StringToFormatEx
    {
        public static string ToCSVLine(params string[] row)
        {
            return row.Length is 0 ? string.Empty : string.Join(",", row).Append("\n");
        }

        // MarkDown Command
        public static string CreateTable(params string[] row)
        {
            return row.Length is 0 ? string.Empty : string.Join("|", row).Append("\n");
        }

        public static string LeftAlign(this string value)
        {
            return value.PlaceBetween(":---", "---:");
        }

        public static string CenterAlign(this string value)
        {
            return value.PlaceBetween(":---:", "---:");
        }

        public static string RightAlign(this string value)
        {
            return value.PlaceBetween("---:", ":---");
        }

        public static string ToBold(this string value)
        {
            return value.PlaceBetween("**", "**");
        }

        public static string ToItalic(this string value)
        {
            return value.PlaceBetween("*", "*");
        }

        public static string ToStrike(this string value)
        {
            return value.PlaceBetween("~~", "~~");
        }

        public static string ToUnderline(this string value)
        {
            return value.PlaceBetween("__", "__");
        }

        public static string ToCode(this string value)
        {
            return value.PlaceBetween("`", "`");
        }

        public static string ToCodeBlock(this string value, string language = "C#")
        {
            return value.PlaceBetween($"```{language}", "```");
        }

        public static string ToLink(this string value, string link)
        {
            return value.PlaceBetween("[", "]").PlaceBetween("(", ")").Append(link);
        }

        public static string ToImage(this string value, string link)
        {
            return value.PlaceBetween("![", "]").PlaceBetween("(", ")").Append(link);
        }

        public static string ToHeader(this string value, int level)
        {
            if (level < 1 || level > 6) throw new ArgumentOutOfRangeException(nameof(level), level, null);
            return value.PlaceBetween(new string('#', level), new string('#', level));
        }

        public static string ToQuote(this string value)
        {
            return value.PlaceBetween(">", "\n");
        }

        public static string ToHorizontalRule(this string value)
        {
            return value.PlaceBetween("---", "\n");
        }

        public static string ToUnorderedList(this string value)
        {
            return value.PlaceBetween("-", "\n");
        }

        public static string ToOrderedList(this string value, int number,
            IntegerToStringFormat format = IntegerToStringFormat.None)
        {
            return value.PlaceBetween($"{number.IntToString(format)}.", "\n");
        }

        public static string ToTaskList(this string value, bool isChecked)
        {
            return value.PlaceBetween(isChecked ? "- [x]" : "- [ ]", "\n");
        }
    }
}