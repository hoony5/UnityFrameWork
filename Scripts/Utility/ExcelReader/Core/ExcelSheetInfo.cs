using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [Serializable]
    public class ExcelSheetInfo
    {
        private const int FixedCapacity = 32;
        
        // Excel Sheet Name
        [field:SerializeField] public string MiddleCategory { get; set; }
        
        [field: SerializeField, SerializedDictionary("Header", "Excel Column Data")]
        public SerializedDictionary<string, ColumnData> ColumnDataDict { get; private set; } = new SerializedDictionary<string, ColumnData>(FixedCapacity);

        [field: SerializeField, SerializedDictionary("Name", "Excel Row Data")]
        public SerializedDictionary<string, RowData> RowDataDict { get; private set; } = new SerializedDictionary<string, RowData>(FixedCapacity);

    }
}