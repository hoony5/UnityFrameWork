using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Diagram
{
    public class DiagramSerialization
    {
        private DiagramGUI gui; 
        public int pasteCount;
        // copy
        [SerializedDictionary("OldID", "Node")]
        private SerializedDictionary<string, CustomNode> copyNodes;
        [SerializedDictionary("OldID", "Group")]
        private SerializedDictionary<string, DiagramGroup> copyGroups;
        private HashSet<GraphElement> copyableElements;

        private CopyType CopyType { get; set; }

        public DiagramSerialization(DiagramGUI gui)
        {
            this.gui = gui;
            copyNodes = new SerializedDictionary<string, CustomNode>();
            copyGroups = new SerializedDictionary<string, DiagramGroup>();
            copyableElements = new HashSet<GraphElement>();
        }

        private DiagramMemo CopyMemo(DiagramNodeModel nodeModel)
        {
            DiagramMemo memo = new DiagramMemo(gui, nodeModel);
            memo.SetUp();
            memo.Load();
            
            memo.NodeModel.Header.ID = Guid.NewGuid().ToString();
            memo.ID = memo.NodeModel.Header.ID;
            gui.view.memos.AddOrUpdate(memo.NodeModel.Header.ID, memo);
            return memo;
        }

        private DiagramGroup CopyGroup(DiagramNodeModel nodeModel)
        {
            DiagramGroup diagramGroup = new DiagramGroup(gui, nodeModel);
            diagramGroup.SetUp();
            diagramGroup.NodeModel.Header.ID = Guid.NewGuid().ToString();
            diagramGroup.ID = diagramGroup.NodeModel.Header.ID;
            gui.view.groups.AddOrUpdate(diagramGroup.NodeModel.Header.ID, diagramGroup);
            return diagramGroup;
        }

        private DiagramNodeBase CopyNode(Type type, DiagramNodeModel nodeModel) 
        {
            DiagramNodeBase node;
            if (type == typeof(DiagramNormalNode))
                node = new DiagramNormalNode(gui, nodeModel);
            else if (type == typeof(DiagramEventNode)) 
                node = new DiagramEventNode(gui, nodeModel);
            else if (type == typeof(DiagramNoteNode))
                node = new DiagramNoteNode(gui, nodeModel);
            else
                return default;
            node.SetUp();
            node.NodeModel.Header.ID = Guid.NewGuid().ToString();
            node.ID = node.NodeModel.Header.ID;
            gui.view.diagramNodes.AddOrUpdate(node.NodeModel.Header.ID, node);
            return node;
        }
        
        public void Copy(CopyType copyType)
        {
            if(gui.selection == null) return;
            if(gui.selection.Count == 0) return;

            CopyType = copyType;
            bool isDuplication = copyType == CopyType.Duplicate;
            if(!isDuplication)
                pasteCount = 0;
            
            copyableElements.Clear();
            
            gui.CollectCopyableElements(
                gui.selection.OfType<GraphElement>(),
                copyableElements);
            
            if(copyableElements.Count == 0) return;
            
            if (!isDuplication) return;
            Paste();
        }

        private Rect GetPastePosition(GraphElement node)
        {
            Rect oldRect = node.GetPosition();
            Vector2 position = oldRect.position;
            Rect newRect = CopyType switch
            {
                CopyType.Cut => new Rect(
                    gui.GetGraphMousePosition(Event.current.mousePosition)
                        .Subtract(new Vector2(10, 10)),
                    oldRect.size),
                _ => new Rect(
                    position.Add(new Vector2(50, 50) * (pasteCount + 1)),
                    node.GetPosition().size)
            };
            
            return newRect;
        }

        private void PasteNode(DiagramNodeBase node)
        {
            DiagramNodeBase copied = CopyNode(node.GetType(), node.NodeModel);
            Rect newRect = GetPastePosition(node); 
            copied.SetPosition(newRect);
            copyNodes.AddOrUpdate(copied.NodeModel.Header.OldID, copied);
            gui.AddElement(copied);
        }

        private void PasteGroup(DiagramGroup group)
        {
            DiagramGroup copied = CopyGroup(group.NodeModel);
            Rect newRect = GetPastePosition(group);
            copied.SetPosition(newRect);
            copyGroups.AddOrUpdate(copied.NodeModel.Header.OldID, copied);
            gui.AddElement(copied);
        }

        private void PasteMemo(DiagramMemo memo)
        {
            DiagramMemo copied = CopyMemo(memo.NodeModel);
            Rect newRect = GetPastePosition(memo);
            copied.SetPosition(newRect);
            gui.AddElement(copied);
        }

        private void UpdateNodePartnerID(DiagramHeaderModel old)
        {
            CustomNode copied = 
                gui.view.diagramNodes.Values.FirstOrDefault(original 
                    => !original.NodeModel.Header.OldID.IsNullOrEmpty() &&
                       original.NodeModel.Header.OldID.Equals(old.ID));
            old.ID = copied?.NodeModel.Header.ID;
        }
        private void UpdatePartnerNodeIDs()
        {
            if (copyNodes.Count == 0) return;
            copyNodes.ForEach(copied =>
            {
                copied.Value.NodeModel.Inheritances?.ForEach(UpdateNodePartnerID);
                
                copied.Value.NodeModel.Injections?.ForEach(UpdateNodePartnerID);

                copied.Value.NodeModel.SubscribingEvents?.ForEach(UpdateNodePartnerID);
                
                copied.Value.NodeModel.NoteNodes?.ForEach(UpdateNodePartnerID);
                
                copied.Value.Load();
            });
        }

        private void AddNodeIntoGroup()
        {
            if (copyNodes.Count == 0) return;
            copyNodes.ForEach(copied =>
            {
                if(copied.Value.NodeModel.Header.Group.IsNullOrEmpty()) return;
                if(!copyGroups.ContainsKey(copied.Value.NodeModel.Header.Group)) return;
                DiagramGroup group = copyGroups[copied.Value.NodeModel.Header.Group];
                group.AddElement(copied.Value);
            });
        }
        private void UpdatePartnerGroupMemberIDs()
        {
            if (copyGroups.Count == 0) return;
            
            copyGroups.ForEach(copied =>
            {
                copied.Value.NodeModel.Members?.Clear();
                copied.Value.Load();
            });
        }

        public void Paste()
        {
            if(copyableElements.Count == 0) return;
            
            List<GraphElement> copyableList = copyableElements.ToList();
            foreach (GraphElement target in copyableList)
            {
                switch (target)
                {
                    case DiagramNodeBase node:
                        PasteNode(node);
                        break;
                    case DiagramMemo memo:
                        PasteMemo(memo);
                        break;
                    case DiagramGroup group:
                        PasteGroup(group);
                        break;
                }
            }
            UpdatePartnerNodeIDs();
            UpdatePartnerGroupMemberIDs();
            AddNodeIntoGroup();
            pasteCount++;
            if(CopyType != CopyType.Cut) return;
            pasteCount = 0;
            copyableElements.Clear();
            copyGroups.Clear();
            copyNodes.Clear();
        }

        public void Dispose()
        {
            copyableElements.Clear();
            copyGroups.Clear();
            copyNodes.Clear();
        }
    }

}