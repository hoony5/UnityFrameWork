using System.Collections.Generic;
using UnityEngine;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class ColumnData
    {
        [field:SerializeField] public string TableName {get; set;}
        [field:SerializeField] public string Header {get; set;}
        [field:SerializeField] public string Type {get; set;}
        [field:SerializeField] public List<string> Values {get; set;}
    }
}