using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Diagram
{
    public class GPTPromptConfig : ScriptableObject
    {
        // if folder search all *.cs , and if cs get File name
        public List<FileModel> attachments = new List<FileModel>();
        [FormerlySerializedAs("FileAttachmentActiveSelf")] public bool fileAttachmentActiveSelf = true;
        public List<TreeViewItemData<string>> nodeTreeViewItems = new List<TreeViewItemData<string>>();
        
        // input
        // first level : refer note, idea , planning
        // second level : structure layout
        // third level : something specific script role
        // forth level : new apply for the script
        
        // output
        // fist level : tracking time and resources
        // second level : solidify the structure
        // third level : refactoring recommendation
        // forth level : generate test codes
        // fifth level : generate profiling codes
        // sixth level : generate documentation ( check List, summary, review, next to do )
        
        public List<string> promptLevels = new List<string>();
        public List<DialogueModel> history = new List<DialogueModel>();
        public List<int> meaningfulHistoryIndices = new List<int>();
        public DesignModel designModel = new DesignModel();

        // calculate gpt token size
        private int TokenAmountOf(string input)
        {
            return input.Length / 4;
        }
        private int TokenAmountOf(List<string> inputs)
        {
            return inputs.Sum(TokenAmountOf);
        }

        public string ToTreeViewString()
        {
            if (nodeTreeViewItems.Count == 0)
                return "No data.";
            
            StringBuilder sb = new StringBuilder();
            foreach (var item in nodeTreeViewItems)
            {
                sb.AppendLine($"- {item.data}"); // value Object class Name : i.e Attack
                if (!item.hasChildren) continue;
                foreach (TreeViewItemData<string> child in item.children)
                {
                     sb.AppendLine($"\t- {child.data}");
                }
            }
            return sb.ToString();
        }
    }
}