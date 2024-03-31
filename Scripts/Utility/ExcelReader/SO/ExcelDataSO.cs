using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utility.ExcelReader
{
    /// <summary>
    /// Using for Editor Only
    /// </summary>
    [CreateAssetMenu(fileName = "newExcelSOData", menuName = "ScriptableObject/ExcelDatabase", order = 0)]
    public class ExcelDataSO : ScriptableObject, ISourceReader
    {
        [field: SerializeField, Header("This repository is used to create these target entity types"), Space(30)]
        public List<string> TargetEntityTypeNames { get; private set; } = new List<string>();
        [field: SerializeField, Space(10)] public Object ExcelData { get; set; }
        [field: SerializeField, Space(10)] public string TopCategory { get; private set; }

        [field: SerializeField, Space(10), SerializedDictionary("Middle Category", "Sheet Infos")]
        public SerializedDictionary<string, ExcelSheetInfo> Database { get; private set; } =
            new SerializedDictionary<string, ExcelSheetInfo>();

#if UNITY_EDITOR
        public virtual void OnValidate()
        {
            Load();
        }
#endif
        public void Load()
        {
#if UNITY_EDITOR
            if (ExcelData is null) return;
            
            string path = AssetDatabase.GetAssetPath(ExcelData);
            if (string.IsNullOrEmpty(path)) return;
            (string topCategory, SerializedDictionary<string, ExcelSheetInfo> map) sheet = ExcelCsvReader.Read(path);
            TopCategory = sheet.topCategory;
            Database = sheet.map;
#endif
        }

        public void GenerateScripts()
        {
#if UNITY_EDITOR
            
#endif
        }

        public (string category, string[] objectNames)[] GetObjectInfos()
        {
#if UNITY_EDITOR
            return Database
                .Select(x
                    => (x.Value.MiddleCategory,
                        x.Value.RowDataDict.Keys.ToArray()))
                .ToArray();
#else
            return null;
#endif
        }

        public void Clear()
        {
            Database.Clear();
        }
    }
}