using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class RowData
    {
        [field:SerializeField] public bool IsEmpty { get; private set; }
        [field:SerializeField,JsonProperty("TopCategory")] public string TopCategory { get; private set; }
        [field:SerializeField,JsonProperty("MiddleCategory")] public string MiddleCategory { get; private set; }
        [field:SerializeField,JsonProperty("BottomCategory")] public string BottomCategory { get; private set; }
        [field:SerializeField,JsonProperty("ObjectName")] public string ObjectName { get;  private set; }
        [field:SerializeField,JsonProperty("ObjectID")] public int ObjectID { get; private set; }
        [field:SerializeField,JsonProperty("Headers")] public List<string> Headers {get; private set;}
        [field:SerializeField,JsonProperty("Types")] public List<string> Types {get; private set;}
        [field:SerializeField,JsonProperty("Values")] public List<string> Values {get; private set;}

        public RowData(bool isEmpty = true) { IsEmpty = isEmpty; }
        public RowData(string topCategory, string middleCategory, string bottomCategory, string objectName, int objectID,
            int columnCount)
        {
            TopCategory = topCategory;
            MiddleCategory = middleCategory;
            BottomCategory = bottomCategory;
            ObjectName = objectName;
            ObjectID = objectID;
            Headers = new List<string>(columnCount);
            Types = new List<string>(columnCount);
            Values = new List<string>(columnCount);
        }
        [JsonConstructor]
        public RowData(string topCategory, string middleCategory, string bottomCategory, string objectName, int objectID,
            List<string> headers, List<string> types, List<string> values)
        {
            TopCategory = topCategory;
            MiddleCategory = middleCategory;
            BottomCategory = bottomCategory;
            ObjectName = objectName;
            ObjectID = objectID;
            Headers = headers;
            Types = types;
            Values = values;
        }
    }
}