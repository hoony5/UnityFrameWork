using System.Collections.Generic;
using System.Text;
using Writer.SourceGenerator.Format;

namespace Writer.SourceGenerator
{
    public class EnumWriter : ContentWriterBase
    {
        private string enumFormat;
        private string enumJsonConverterFormat;
        private string enumParserFormat;

        private string enumToStringFormat;
        private string stringToEnumFormat;
        private string numberToEnumFormat;
        private string enumToNumberFormat;

        private string enumExceptionFormat;

        private StringBuilder sb;

        public EnumWriter(
            string nameSpace,
            string usingSpace)
        {
            sb = new StringBuilder();

            AddFormat(nameof(enumFormat), new EnumFormat(nameSpace, usingSpace));
            AddFormat(nameof(enumJsonConverterFormat), new EnumJsonConverterFormat());
            AddFormat(nameof(enumParserFormat), new EnumParserExtensionFormat());

            AddFormat(nameof(stringToEnumFormat), new StringToEnumFormat());
            AddFormat(nameof(enumToStringFormat), new EnumToStringFormat());
            AddFormat(nameof(numberToEnumFormat), new NumberToEnumFormat());
            AddFormat(nameof(enumToNumberFormat), new EnumToNumberFormat());

            AddFormat(nameof(enumExceptionFormat), new EnumExceptionFormat());
        }

        public string WriteEnum(string typeName, IList<string> enumValues)
        {
            string format = GetFormat(nameof(enumFormat));
            StringBuilder values = new StringBuilder();
            foreach (string value in enumValues)
            {
                if (string.IsNullOrEmpty(value)) continue;
                values.AppendLine($"\t{value},\n");
            }

            return format
                .Replace("{0}", typeName)
                .Replace("{1}", values.ToString()); 
        }

        public string WriteEnumExtension(string typeName, IList<string> enumValues)
        {
            if (typeName == null)
            {
                return "Enum Name is null";
            }

            if (enumValues.Count == 0)
            {
                return "Enum Values is empty";
            }

            string inputParameterName = "value";
            string stringToEnum = StringToEnumSwitch(typeName, enumValues, inputParameterName);
            string enumToString = EnumSwitchToString(typeName, enumValues, inputParameterName);
            string enumToNumber = EnumToNumberSwitch(typeName, enumValues, inputParameterName);
            string numberToEnum = NumberToEnumSwitch(typeName, enumValues, inputParameterName);

            return GetFormat(nameof(enumParserFormat)
                    .Replace("{0}", stringToEnum)
                    .Replace("{1}", enumToString)
                    .Replace("{2}", enumToNumber)
                    .Replace("{3}", numberToEnum));
        }

        public string WriteJsonConverter(string typeName, IList<string> enumValues)
        {
            string enumVariableName = typeName[..1].ToLower() + typeName[1..];
            string enumToString = EnumSwitchToString(typeName, enumValues, enumVariableName);
            string stringToEnum = StringToEnumSwitch(typeName, enumValues, $"{enumVariableName}String");

            return GetFormat(nameof(enumJsonConverterFormat))
                .Replace("{0}", typeName)
                .Replace("{1}", enumVariableName)
                .Replace("{2}", enumToString)
                .Replace("{3}", stringToEnum);
        }

        private string EnumSwitchToString(string typeName, IList<string> enumValues, string inputValueName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string value in enumValues)
            {
                if (string.IsNullOrEmpty(value)) continue;
                sb.AppendFormat(GetFormat(nameof(enumToStringFormat)), typeName, value);
            }

            string content = GetFormat(nameof(enumExceptionFormat))
                .Replace("{0}", typeName)
                .Replace("{1}", inputValueName);
            
            sb.AppendLine(content);

            return sb.ToString();
        }

        private string StringToEnumSwitch(string typeName, IList<string> enumValues, string inputValue)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string value in enumValues)
            {
                if (string.IsNullOrEmpty(value)) continue;
                sb.AppendFormat(GetFormat(nameof(stringToEnumFormat)), typeName, value);
            }

            string content = GetFormat(nameof(enumExceptionFormat))
                .Replace("{0}", typeName)
                .Replace("{1}", inputValue);
            
            sb.AppendLine(content);
            return sb.ToString();
        }

        private string NumberToEnumSwitch(string typeName, IList<string> enumValues, string inputValue)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < enumValues.Count; ++i)
            {
                string enumContent = GetFormat(nameof(numberToEnumFormat))
                    .Replace("{0}", i.ToString())
                    .Replace("{1}", typeName)
                    .Replace("{2}", enumValues[i]);
                
                sb.AppendLine(enumContent);
            }

            string content = GetFormat(nameof(enumExceptionFormat))
                .Replace("{0}", typeName)
                .Replace("{1}", inputValue);
            
            sb.AppendLine(content);
            return sb.ToString();
        }

        private string EnumToNumberSwitch(string typeName, IList<string> enumValues, string inputValue)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < enumValues.Count; ++i)
            {
                string enumContent = GetFormat(nameof(enumToNumberFormat))
                    .Replace("{0}", typeName)
                    .Replace("{1}", enumValues[i])
                    .Replace("{2}", i.ToString());
                
                sb.AppendLine(enumContent);
            }

            string content = GetFormat(nameof(enumExceptionFormat))
                .Replace("{0}", typeName)
                .Replace("{1}", inputValue);
            sb.AppendLine(content);

            return sb.ToString();
        }
    }
}