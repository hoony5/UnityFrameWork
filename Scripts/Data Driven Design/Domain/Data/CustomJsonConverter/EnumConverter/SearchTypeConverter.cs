using System;
using Newtonsoft.Json;

public class SearchTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        SearchType searchType = value is SearchType type ? type : SearchType.None;
        writer.WriteValue(searchType switch
        {
            SearchType.None => "None",
            SearchType.Status => "Status",
            SearchType.State => "State",
            _ => throw new SearchTypeConverterException( $"Not supported SearchType: {searchType}")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string searchTypeString = reader.Value as string;
        SearchType result = searchTypeString switch
        {
            "None" => SearchType.None,
            "Status" => SearchType.Status,
            "State" => SearchType.State,
            _ => throw new SearchTypeConverterException($"Not supported SearchType: {searchTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(SearchType);
    }
}

public class SearchTypeConverterException : Exception
{
    public SearchTypeConverterException(string message) : base(message)
    {
    }
}
