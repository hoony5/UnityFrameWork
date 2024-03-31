using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Diagram
{
    public class ConfluenceConfig : ScriptableObject
    {
        public string url = "https://name.atlassian.net/wiki";
        public string username = "your-username";
        [FormerlySerializedAs("aPIToken")] public string apiToken = "your-api-token";
        public string spaceKey = "your-space-key";
        public string ancestorSplitter = ">"; // i.e. parent>child>child
        public string[] ancestors = Array.Empty<string>();
    }

}