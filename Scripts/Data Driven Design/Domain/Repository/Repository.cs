using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Utility.ExcelReader;
using System;

[System.Serializable]
public class Repository<TDataModel> : IRepository<TDataModel> where TDataModel : ObjectCategory
{
    // first Key : Category
    // second Key : ObjectName
    [field:SerializeField] public SerializedDictionary<string, SerializedDictionary<string, TDataModel>> Entities { get; private set; }
    = new SerializedDictionary<string, SerializedDictionary<string, TDataModel>>();

    public Repository()
    {
        Entities = new SerializedDictionary<string, SerializedDictionary<string, TDataModel>>();
    }
    public Repository(ExcelDataSO excelDataSO, params string[] sharedCategories)
    {
        Entities = new SerializedDictionary<string, SerializedDictionary<string, TDataModel>>();

        excelDataSO.Load();
        
        (string category, string[] objectNames)[] infoOf = excelDataSO.GetObjectInfos();
        foreach ((string category, string[] objectNames) info in infoOf)
        {
            if (!sharedCategories.Contains(info.category)) continue;
            if (Entities.ContainsKey(info.category) && Entities[info.category].Count > 0) 
            {
                foreach (string objectName in info.objectNames)
                {
                    Entities[info.category].Add(objectName, default);
                }
            }
            else
            {
                SerializedDictionary<string, TDataModel> objectMap = new SerializedDictionary<string, TDataModel>();
                foreach (string objectName in info.objectNames)
                {
                    objectMap.Add(objectName, default);
                }
                Entities.Add(info.category, objectMap);
            }
        }
    }
    public string[] GetCategories()
    {
#if UNITY_EDITOR
        return Entities.Keys.ToArray();
#else
        return Array.Empty<string>();
#endif
    }
    public void Add(TDataModel entity)
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(entity.MiddleCategory)) return;

        if (Entities.TryGetValue(
                entity.MiddleCategory,
                out SerializedDictionary<string, TDataModel> repository))
        {
            repository[entity.ObjectName] = entity;
        }
        else
        {
            SerializedDictionary<string, TDataModel> newRepository = new SerializedDictionary<string, TDataModel>();
            newRepository.Add(entity.ObjectName, entity);
            Entities.TryAdd(entity.MiddleCategory, newRepository);
        }
#endif
    }
    public void Clear()
    {
#if UNITY_EDITOR
        Entities.Clear();
#endif 
    }
    public bool TryGetData(string category, string objectName, out TDataModel dataModel)
    {
        dataModel = default;
        if (string.IsNullOrEmpty(category)) return false;
        
        return Entities.TryGetValue(
            category,
            out SerializedDictionary<string, TDataModel> repository) && repository.TryGetValue(objectName, out dataModel);
    }
 
    public override string ToString()
    {
#if UNITY_EDITOR
        IEnumerable<TDataModel> dataBases = Entities.Values.SelectMany(x => x.Values);
        string[] keys = string.Join(",", Entities.Keys).Split(",");
        return $@"
[Entity Repository] - Count : {Entities.Count}

    *   Key     : {string.Join(",", Entities.Keys)}

        - Count : {string.Join(",", keys.Select(x => $"{x} : {Entities[x].Count}"))}
                 

    *   Value   : {string.Join(",\n", dataBases.Select(x => x.ToString()))}

";
#else
        return base.ToString();
#endif
    }
}