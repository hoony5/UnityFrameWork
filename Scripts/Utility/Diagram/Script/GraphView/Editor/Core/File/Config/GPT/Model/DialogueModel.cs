using System.Collections.Generic;
using GPT;
using UnityEngine.Serialization;

namespace Diagram
{
    [System.Serializable]
    public class DialogueModel
    {
        public string speaker;
        public string content;
        public dynamic raw;

        public DialogueModel()
        {
            
        }
        public DialogueModel(DialogueModel other)
        {
            speaker = other.speaker;
            content = other.content;
            raw = other.raw;
        }
        public override string ToString()
        {
            return $"{speaker} : {content}";
        }
    }
}
