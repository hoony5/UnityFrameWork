using Newtonsoft.Json;

[JsonConverter(typeof(SearchTypeConverter))]
public enum SearchType
{
    None,
    Status,
    State
}