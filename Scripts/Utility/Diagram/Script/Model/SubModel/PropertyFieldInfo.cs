using System;
using UnityEngine;

namespace Diagram
{
    [Serializable]
    public class PropertyFieldInfo
    {
        [field:SerializeField] public string Name { get; set; }
        [field:SerializeField] public string ScriptName { get; set; }
        [field:SerializeField] public bool IsInherited { get; set; }
        [field:SerializeField] public DeclarationType DeclarationType { get; set; }
        [field:SerializeField] public string Type { get; set; }
        public bool IsArray => Type.Contains("[]");
        
        public PropertyFieldInfo()
        {
            Name = "Property";
            DeclarationType = DeclarationType.NonModified;
            Type = "object";
        }
        public PropertyFieldInfo(string type, string name)
        {
            Name = name;
            DeclarationType = DeclarationType.NonModified;
            Type = type;
        }
    }
}