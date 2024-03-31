using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : Type
    /// {1} : VariableName
    /// {2} : EnumToString
    /// {3} : StringToEnum
    /// </summary>
    public class EnumJsonConverterFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $@"using System;
using Newtonsoft.Json;

public class {{0}}JsonConverter : JsonConverter
{{
 public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {{
        {{0}} {{1}} = value is {{0}} type ? type : default;
        writer.WriteValue({{1}} switch
        {{
{{2}}
        }});
    }}

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {{
        string {{1}}String = reader.Value as string;
        {{0}} result = {{1}}String switch
        {{
{{3}}
        }};
        return result;
    }}

    public override bool CanConvert(Type objectType)
    {{
        return objectType == typeof({{0}});
    }}
}}

public class {{0}}JsonConverterException : Exception
{{
    public {{0}}JsonConverterException(string message) : base(message)
    {{
    }}
}}";
        }
    }

}