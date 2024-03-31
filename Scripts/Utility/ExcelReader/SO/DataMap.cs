using AYellowpaper.SerializedCollections;
using Share;
using UnityEngine;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class DataMap<T>
    {
        [field:SerializeField] private SerializedDictionary<string, T> ObjectMap { get; set; } 
            = new SerializedDictionary<string, T>();
        public bool ContainsKey(string objectName) => ObjectMap.ContainsKey(objectName);
        public bool TryGetValue(string objectIndex, out T value)
        {
            value = default;
            if (ObjectMap.TryGetValue(objectIndex, out T original))
            {
                value = original.DeepCopy();
                return true;
            }

            return false;
        }
        
        public DataMap(SerializedDictionary<string, T> objectMap)
        {
            ObjectMap = objectMap;
        }

        // Any other methods you need...
    }
}