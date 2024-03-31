using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Utility.ExcelReader;

[System.Serializable]
public class ObjectDataSO<T> : ScriptableObject
{
    [field: SerializeField] protected ExcelDataSO ExcelData { get; set; }

    [field: SerializeField, SerializedDictionary("Middle Category", "Object Infos")]
    public SerializedDictionary<string, DataMap<T>> DataBase { get; private set; }
        = new SerializedDictionary<string, DataMap<T>>();

    public string[] GetDataRegionIndexDatas => DataBase.Keys.ToArray();
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        foreach (KeyValuePair<string, ExcelSheetInfo> ExcelDataBase in ExcelData.Database)
        {
            string middleCategory = ExcelDataBase.Key;
            ExcelSheetInfo objectExcelSheetInfo = ExcelDataBase.Value;

            DataMap<T> dataMap = new DataMap<T>(objectExcelSheetInfo.ToObjects<T>());
            DataBase.Add(middleCategory, dataMap);
        }
    }
#endif

    public bool TryGetObjectRepository(string middleCategory, out DataMap<T> repository)
    {
        return DataBase.TryGetValue(middleCategory, out repository);
    }

    public bool TryGetObject(string middleCategory, string objectName, out T objectItem)
    {
        objectItem = default;
        return DataBase.TryGetValue(middleCategory, out DataMap<T> dataRegion) &&
               dataRegion.TryGetValue(objectName, out objectItem);
    }
}
