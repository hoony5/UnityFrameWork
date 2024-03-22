using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using NaughtyAttributes;
#endif

[Serializable, CreateAssetMenu (fileName = "newSOCreator", menuName = "ScriptableObject/Model/Repository/SOCreator")]
public class SOCreator : ScriptableObject
{
#if UNITY_EDITOR
    [field:SerializeField] private UnityEngine.Object Folder { get; set; }
    [field:SerializeField, ReadOnly] private string SavePath { get; set; }
    [field:SerializeField,Space(10)] private MonoScript[] CreatorTargetScripts { get; set; }
    [field:SerializeField, ReadOnly] private string[] RepositoryTypes { get; set; }
#endif

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Folder == null) return;
        // only allow folders
        SavePath = AssetDatabase.GetAssetPath(Folder);
        if (!AssetDatabase.IsValidFolder(SavePath))
        {
            Folder = null;
            SavePath = "";
        }
        if(CreatorTargetScripts.Length == 0) return;
        
        RepositoryTypes = new string[CreatorTargetScripts.Length];
        for (int i = 0; i < CreatorTargetScripts.Length; i++)
        {
            if (CreatorTargetScripts[i] is null) continue;
            RepositoryTypes[i] = CreatorTargetScripts[i].GetClass().Name;
        }
#endif
    }

#if UNITY_EDITOR
    [Button]
#endif
    private void CreateRepositories()
    {
#if UNITY_EDITOR
        if(RepositoryTypes.Length == 0) return;

        foreach (string TypeName in RepositoryTypes)
        {
            ScriptableObject repository = CreateInstance(TypeName);
            AssetDatabase.CreateAsset(repository, $"{SavePath}/{TypeName}.asset");
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        }
#endif
    }
}