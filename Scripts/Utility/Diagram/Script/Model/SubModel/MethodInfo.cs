using System;
using System.Collections.Generic;
using UnityEngine;

namespace Diagram
{
    [Serializable]
    public class MethodInfo
    {
        [field:SerializeField] public string ScriptName { get; set; }
        [field:SerializeField] public string Name { get; set; }
        [field:SerializeField] public bool IsInherited { get; set; }
        [field:SerializeField] public string AccessType { get; set; }
        [field:SerializeField] public DeclarationType DeclarationType { get; set; }
        [field:SerializeField] public string Type { get; set; }
        [field:SerializeField] public string Predicate { get; set; }
        
        public bool IsArray => Type.Contains("[]");
        public List<(string type, string name)> Parameters { get; set; }
        
        public MethodInfo(string scriptName)
        {
            ScriptName = scriptName;
            AccessType = "public";
            Name = "Method";
            DeclarationType = DeclarationType.NonModified;
            Type = "void";
            Predicate = "";
            Parameters = new List<(string type, string name)>(4);
        }
        public bool IsMatch(MethodInfo methodInfo)
        {
            if (!ScriptName.Equals(methodInfo.ScriptName) ||
                !Name.Equals(methodInfo.Name) ||
                AccessType != methodInfo.AccessType ||
                Parameters.Count != methodInfo.Parameters.Count ||
                !Predicate.Equals(methodInfo.Predicate))
            {
                return false;
            }

            for (var i = 0; i < Parameters.Count; i++)
            {
                if (Parameters[i].type != methodInfo.Parameters[i].type ||
                    !Parameters[i].name.Equals(methodInfo.Parameters[i].name))
                {
                    return false;
                }
            }

            return true;
        }
    }
}