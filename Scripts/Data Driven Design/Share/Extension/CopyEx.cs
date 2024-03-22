using Newtonsoft.Json;

namespace Share
{
    public static class CopyEx
    {
        public static T DeepCopy<T>(this T obj)
        {
            return obj is null ? default : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    
        public static T DeepCopy<T>(this T obj, JsonSerializerSettings settings)
        {
            return obj is null ? default : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, settings));
        }
    }
}
