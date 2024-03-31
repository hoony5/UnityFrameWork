using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public abstract class RepositorySOBase : ScriptableObject
{
    [field:SerializeField] protected string Version { get; set; }
    
#if UNITY_EDITOR
    [field:ReadOnly]
#endif
    [field:SerializeField] public string Name { get; private set; }
    
#if UNITY_EDITOR
    [field:TextArea(3, 10), Space(10)]
#endif
    [field:SerializeField] protected string Description { get; set; }
    protected virtual void OnValidate()
    {
#if UNITY_EDITOR
        Version = string.IsNullOrEmpty(Version) ? "0.0.0" : Version;
        Name = string.IsNullOrEmpty(Name) ? GetType().Name : Name;
#endif
    }
}