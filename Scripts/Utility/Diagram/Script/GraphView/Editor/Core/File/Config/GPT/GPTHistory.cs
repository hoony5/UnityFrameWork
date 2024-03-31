using System.Collections.Generic;
using UnityEngine;

namespace Diagram
{
    public class GPTHistory : ScriptableObject
    {
        public string title;
        public string importance;
        public List<DialogueModel> history = new List<DialogueModel>();
        public string summary;
        public int validationMaxToken;
        public List<FileModel> attachments = new List<FileModel>();
    }
}