using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Diagram
{
    public class DiagramSO : ScriptableObject
    {
        [field:SerializeField] public string ID { get; set; }
        [field:SerializeField] public string OldPath { get; set; }
        [field:SerializeField, SerializedDictionary("GUID", "Node")]
        public SerializedDictionary<string, DiagramNodeModel> Models { get; set; }

        #if UNITY_EDITOR
        [UnityEditor.Callbacks.OnOpenAsset]
        #endif
        public static bool Load(int instanceID, int _)
        {
#if UNITY_EDITOR
            DiagramSO asset = UnityEditor.EditorUtility.InstanceIDToObject(instanceID) as DiagramSO;
            if (asset == null) return false;
        
            if(DiagramWindow.IsOpened)
            {
                DiagramWindow.CloseWindow();
            }
            DiagramWindow.OpenWindowWithoutTempDatabase();
            DiagramWindow.IO.Load(asset);
#endif
            return true;
        }
        public void Init()
        {
            ID = System.Guid.NewGuid().ToString();
            Models = new SerializedDictionary<string, DiagramNodeModel>();
        }
        public void Clear()
        {
            Models.Clear();
        }
    }
}