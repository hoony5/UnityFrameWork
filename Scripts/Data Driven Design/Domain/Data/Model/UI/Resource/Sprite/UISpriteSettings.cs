using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable, CreateAssetMenu (menuName = "ScriptableObject/UI/UISpriteSettings")]
public class UISpriteSettings : ScriptableObject
{
    [field: SerializeField, SerializedDictionary("Purpose", "Components")]
    private SerializedDictionary<string, UISpriteInfo[]> SpriteInfoMap { get; set; }
        = new SerializedDictionary<string, UISpriteInfo[]>();
    
    public bool TryGetMap(string key, out SerializedDictionary<string, UISpriteInfo> value)
    {
        value = null;
        if (!SpriteInfoMap.TryGetValue(key, out UISpriteInfo[] map)) return false;
        value = new SerializedDictionary<string, UISpriteInfo>(
            map.ToDictionary(
            keyIs => keyIs.Description,
            value => value)
            );
        return true;
    }
}

[System.Serializable]
public class UISpriteInfo
{
    [field:SerializeField] public string Description {get; private set;}
    [field:SerializeField] public Sprite Sprite {get; private set;}
}
