using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Diagram
{
    public class SearchWindow : ScriptableObject, ISearchWindowProvider, IDisposable
    {
        private DiagramGUI diagramGUI;
        private Texture2D indentationIcon;
        private Vector2 localMousePosition;
        public void Init (DiagramGUI view)
        {
            diagramGUI = view;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree (SearchWindowContext context)
        {
            localMousePosition = diagramGUI.GetGraphMousePosition(context.screenMousePosition, true);
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry> 
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new SearchTreeEntry(new GUIContent("Normal Node",indentationIcon))
                {
                    level = 1,
                    userData = typeof(DiagramNormalNode)
                },
                new SearchTreeEntry(new GUIContent("Event Node",indentationIcon)) 
                {
                    level = 1,
                    userData = typeof(DiagramEventNode)
                },
                new SearchTreeEntry(new GUIContent("Note Node",indentationIcon)) 
                {
                    level = 1,
                    userData = typeof(DiagramNoteNode)
                },
                new SearchTreeEntry(new GUIContent("Memo",indentationIcon)) 
                {
                    level = 1,
                    userData = typeof(DiagramMemo)
                }
            };
            

            return searchTreeEntries;
        }
        public bool OnSelectEntry (SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            string typeName = ((Type)searchTreeEntry.userData).Name;
            return typeName switch
            {
                nameof(DiagramNormalNode) => CreateNode<DiagramNormalNode>(localMousePosition),
                nameof(DiagramEventNode) => CreateNode<DiagramEventNode>(localMousePosition),
                nameof(DiagramNoteNode) => CreateNode<DiagramNoteNode>(localMousePosition),
                nameof(DiagramGroup) => CreateGroup<DiagramGroup>(localMousePosition),
                nameof(DiagramMemo) => CreateMemo(localMousePosition),
                _ => false
            };
        }

        private bool CreateNode<TNode> (Vector2 mousePosition) where TNode : Node
        {
            IDiagramNode node = diagramGUI.CreateNode(typeof(TNode), $"new {typeof(TNode).Name}", mousePosition);
            diagramGUI.AddElement((GraphElement)node);
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
            return true;
        }
        
        private bool CreateGroup<TGroup> (Vector2 mousePosition) where TGroup : Group
        {
            DiagramGroup group = diagramGUI.CreateGroup("Group", mousePosition);
            diagramGUI.AddElement(group);
            return true;
        }
        private bool CreateMemo (Vector2 mousePosition)
        {
            DiagramMemo memo = diagramGUI.CreateMemo("Memo", mousePosition);
            diagramGUI.AddElement(memo);
            return true;
        }

        public void Dispose()
        {
            indentationIcon = null;
        }
    }
}
