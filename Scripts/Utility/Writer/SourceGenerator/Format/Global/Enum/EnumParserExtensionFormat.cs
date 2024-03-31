using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} Name
    /// {1} string To Enum Content
    /// {2} Enum To string Content
    /// {3} Enum To Number Content
    /// {4} Number To Enum Content
    /// </summary>
    public class EnumParserExtensionFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $@"public static class {{0}}Extensions
{{
    public static bool TryParse(this string value, out {{0}} result)
    {{
        result = default;
        if(string.IsNullOrEmpty(value))
        {{
            return false;
        }}

        result = value switch
        {{
{{1}}
        }};
        return true;          
    }}

    public static string GetString(this {{0}} value)
    {{
        return value switch
        {{
{{2}}
        }};
    }}

    public static int NumberOf(this {{0}} value)
    {{
        return value switch
        {{
{{3}}
        }};
    }}
    
    public static {{0}} ToEnum(this int value)
    {{
        return value switch
        {{
{{4}} 
        }};   
    }}
}}";
        }
    }

}