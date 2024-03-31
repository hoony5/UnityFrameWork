using UnityEngine;

namespace Diagram.Config
{
    [System.Serializable]
    public class DiagramSourceGeneratorConfig : ScriptableObject
    {
        [Header("Code style")]
        public string onlyPropertyStyle = "public ##type## ##name## { get; ##set##; }"; //i.e. public string Name { get; init; }
        
        public string propertyStyle = "public ##type## ##name## { get => ##field##; ##set## => ##field##; }"; //i.e. public string Name { get; init; }
        public string fieldStyle = "public ##type## ##name##;"; //i.e. public string name; 
        public string serializeFieldStyle = "[field:SerializeField]"; //i.e. [field:SerializeField]
        public string logFormat = "##type## ##name##"; //i.e. public string Name;
        
        public string toStringFormat = $@"[##type##]
-    ##name##"; //i.e. [SerializeField] private string name;

        public bool generateField = false;
        public bool useJsonAttribute = true;
        public bool useSerializeFieldAttribute = true;
        public bool useDisposeMethod = true;
    }
}