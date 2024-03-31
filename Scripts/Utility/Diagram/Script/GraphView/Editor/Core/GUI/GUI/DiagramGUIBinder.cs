using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Share;

namespace Diagram
{
    public class DiagramGUIBinder : IDisposable
    {
        private DiagramGUI gui;
        private DiagramGUIView view;
        private DiagramInputBinding inputBinding;
        
        private ContentDragger contentDragger;
        private SelectionDragger selectionDragger;
        private RectangleSelector rectangleSelector;
        
        private StringBuilder stringBuilder = new StringBuilder();

        public DiagramGUIBinder(DiagramGUI gui, DiagramGUIView view)
        {
            this.gui = gui;
            this.view = view;
            inputBinding = new DiagramInputBinding(this.gui);
            contentDragger  = new ContentDragger();
            selectionDragger  = new SelectionDragger();
            rectangleSelector  = new RectangleSelector();
        }
        private IManipulator CreateGroupContextMenu()
        {
            ContextualMenuManipulator cmm = new ContextualMenuManipulator(menuEvent =>
                menuEvent.menu.InsertAction(1, "Create Group", actionEvent => gui.CreateGroup("New Group", gui.GetGraphMousePosition(actionEvent.eventInfo.localMousePosition)))
            );

            return cmm;
        }

        private void BindGroupElementFixed()
        {
            gui.graphViewChanged -= OnPositionChanged;
            gui.graphViewChanged += OnPositionChanged;
        }
        private void DisposeGroupElementFixed()
        {
            gui.graphViewChanged -= OnPositionChanged;
        }
        private GraphViewChange OnPositionChanged(GraphViewChange change)
        {
            if( change.movedElements == null) return change;
            if (change.movedElements.Count == 0) return change;

            change.movedElements.ForEach(movedElement =>
            {
                if (movedElement is not DiagramGroup group) return;
                Vector2 newPosition = group.GetPosition().position;
                Vector2 newSize = group.GetPosition().size;
                if (!group.NodeModel.Header.Position.Approximately(newPosition))
                {
                    group.NodeModel.Header.Position = newPosition;
                    gui.serialization.pasteCount = 0;   
                }
            
                if (!group.NodeModel.Header.Size.Approximately(newSize))
                {
                    group.NodeModel.Header.Size = newSize;
                } 
            });
            return change;
        }
        private void BindGroupElementsAdded ()
        {
            gui.elementsAddedToGroup = OnGUIElementsAddedToGroup;
        }
        private void DisposeGroupElementsAdded()
        {
            gui.elementsAddedToGroup = null;
        }

        private void OnGUIElementsAddedToGroup(Group group, IEnumerable<GraphElement> elements)
        {
            foreach (GraphElement element in elements)
            {
                if (element is not CustomNode diagramNode) continue;
                if (group is not DiagramGroup diagramGroup) continue;

                diagramNode.Group = diagramGroup;
                diagramNode.NodeModel.Header.Group = diagramGroup.NodeModel.Header.ID;
                diagramNode.NodeModel.Header.Namespace = diagramGroup.NodeModel.Note.Title;
                diagramNode.NodeModel.Status.IsDirty = true;

                if (diagramGroup.NodeModel.Members.Contains(diagramNode.NodeModel.Header)) continue;
                diagramGroup.NodeModel.Members.Add(diagramNode.NodeModel.Header);
            }
        }

        private void BindGroupRenamed()
        {
            gui.groupTitleChanged = OnGUIGroupTitleChanged;
        }
        private void DisposeGroupRenamed()
        {
            gui.groupTitleChanged = null;
        }

        private void OnGUIGroupTitleChanged(Group group, string newTitle)
        {
            if (group is not DiagramGroup newGroup) return;
            group.title = newTitle;
            newGroup.NodeModel.Note.Title = newTitle;
            if (newGroup.NodeModel.Members.Count == 0) return;
            newGroup.NodeModel.Members.ForEach(member =>
            {
                member.Namespace = newTitle;
                if(!view.diagramNodes.TryGetValue(member.ID, out DiagramNodeBase node)) return;
                node.NodeModel.Header.Namespace = newTitle;
                node.NodeModel.Status.IsDirty = true;
            });
        }

        private void BindGroupedElementsRemoved()
        {
            gui.elementsRemovedFromGroup = OnGUIElementsRemovedFromGroup;
        }
        private void DisposeGroupedElementsRemoved()
        {
            gui.elementsRemovedFromGroup = null;
        }

        // when load diagram, group is removed on the diagram because of the group is not exist in the diagram
        private void OnGUIElementsRemovedFromGroup(Group group, IEnumerable<GraphElement> elements)
        {
            foreach (GraphElement element in elements)
            {
                if (element is not CustomNode diagramNode) continue;
                if (group is not DiagramGroup diagramGroup) continue;

                diagramNode.Group = null;
                diagramNode.NodeModel.Header.Group = string.Empty;
                diagramNode.NodeModel.Header.Namespace = string.Empty;

                if (!diagramGroup.NodeModel.Members.Exists(member => member.ID.Equals(diagramNode.NodeModel.Header.ID))) continue;
                diagramGroup.NodeModel.Members.Remove(diagramNode.NodeModel.Header);
            }
        }

        private void BindDeleted()
        {
            gui.deleteSelection = OnGUIDeleteSelection;
        }
        private void DisposeDeleted()
        {
            gui.deleteSelection = null;
        }
        private void RemoveEdge(Edge edge, Node node)
        {
            if (node is not CustomNode child) return;
            switch (edge.output.node)
            {
                case DiagramNormalNode outputNormalNode:
                    if(child.NodeModel.Inheritances.Count != 0) 
                        child.NodeModel.Inheritances.RemoveAll(connection => connection.ID.Equals(outputNormalNode.ID));
                    if(child.NodeModel.Injections.Count != 0) 
                        child.NodeModel.Injections.RemoveAll(connection => connection.ID.Equals(outputNormalNode.ID));
                    break;
                case DiagramEventNode outputEventNode:
                    if(child.NodeModel.SubscribingEvents.Count == 0) return;
                    child.NodeModel.SubscribingEvents.RemoveAll(connection => connection.ID.Equals(outputEventNode.ID));
                    break;
                case DiagramNoteNode outputNoteNode:
                    if(child.NodeModel.NoteNodes.Count == 0) return;
                    child.NodeModel.NoteNodes.RemoveAll(connection => connection.ID.Equals(outputNoteNode.ID));
                    break;
            }
            edge.output.Disconnect(edge);
            edge.input.Disconnect(edge);
            gui.RemoveElement(edge);   
            child.NodeModel.Status.IsDirty = true;
        }
        private void OnGUIDeleteSelection(string s, GraphView.AskUser askUser)
        {
            List<DiagramGroup> groupsToRemove = new List<DiagramGroup>();
            List<DiagramNodeBase> nodesToRemove = new List<DiagramNodeBase>();
            List<Edge> edgesToRemove = new List<Edge>();
            List<StickyNote> memoToRemove = new List<StickyNote>();

            gui.selection.ForEach(selectable =>
            {
                if (selectable is not GraphElement element) return;

                switch (element)
                {
                    case DiagramNodeBase node:
                        nodesToRemove.Add(node);
                        return;
                    case Edge edge:
                        edgesToRemove.Add(edge);
                        return;
                    case DiagramGroup group:
                        groupsToRemove.Add(group);
                        return;
                }

                if (element is not StickyNote memo) return;
                memoToRemove.Add(memo);
            });

            foreach (DiagramGroup group in groupsToRemove)
            {
                List<DiagramNodeBase> groupNodes = new List<DiagramNodeBase>();
                foreach (GraphElement element in group.containedElements)
                {
                    if (element is not DiagramNodeBase groupDiagramNode) continue;

                    groupNodes.Add(groupDiagramNode);
                }
                view.groupToRemove.AddOrUpdate(group.ID, group);
                view.groups.Remove(group.ID);
                group.RemoveElements(groupNodes); // Remove Group Nodes
                gui.RemoveElement(group); // Remove Group
                group.RemoveFromHierarchy();
            }

            edgesToRemove.ForEach
                (edge => RemoveEdge(edge, edge.input.node));

            memoToRemove.ForEach
            (memo =>
            {
                ((DiagramMemo)memo).Dispose();
                gui.view.memoToRemove.AddOrUpdate(((DiagramMemo)memo).ID, (DiagramMemo)memo);
                view.memos.Remove(((DiagramMemo)memo).ID);
                memo.RemoveFromHierarchy();
            });

            foreach (DiagramNodeBase node in nodesToRemove) // Remove Nodes
            {
                node.Group?.RemoveElement(node);
                gui.RemoveElement(node);
                node.RemoveFromHierarchy();
                node.Dispose();

                view.diagramNodeToRemove.AddOrUpdate(node.ID, node);
                view.diagramNodes.Remove(node.ID);
            }
            
            gui.blackboard?.OnUpdate(null);
            gui.gptBoard.viewModel.DrawTreeView();
            gui.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }

        // TODO :: name graph view
        public void CreateSimpleHierarchyDiagram()
        {
            if(gui.inheritanceRelationships == null) return;
            if(gui.inheritanceRelationships.Count == 0) return;
        }
        public string GetHierarchyToString()
        {
            stringBuilder.Clear();
            // 루트 노드 찾기 (부모가 없는 노드)
            List<string> rootNodes = FindRootNodes();
            foreach (var rootNode in rootNodes)
            {
                // 루트 노드부터 계층 구조 출력 시작
                NodeToBuildString(rootNode, 0);
            }
            return stringBuilder.ToString();
        }

        private List<string> FindRootNodes()
        {
            // 모든 자식 노드를 담는 리스트 생성
            List<string> allChildren = gui.inheritanceRelationships.SelectMany(pair => pair.Value).Distinct().ToList();
            // 부모 노드 중 자식 노드 리스트에 없는 노드를 루트 노드로 간주
            List<string> rootNodes = gui.inheritanceRelationships.Keys.Where(k => !allChildren.Contains(k)).ToList();
            return rootNodes;
        }

        private void NodeToBuildString(string node, int depth)
        {
            // 현재 노드 출력 (들여쓰기 포함)
            stringBuilder.AppendLine(new string(' ', depth * 2) + "- " + node);

            // 현재 노드가 자식을 가지고 있다면, 각 자식에 대해 재귀적으로 이 함수 호출
            if (!gui.inheritanceRelationships.TryGetValue(node, out List<string> relationship)) return;
            foreach (string child in relationship)
            {
                NodeToBuildString(child, depth + 1);
            }
        }
        public void Bind()
        {
            gui.AddManipulator(contentDragger);
            gui.AddManipulator(selectionDragger);
            gui.AddManipulator(rectangleSelector);
            gui.AddManipulator(CreateGroupContextMenu());
            
            inputBinding.SetUpShotCutCommand();
            BindDeleted();
            BindGroupElementsAdded();
            BindGroupedElementsRemoved();
            BindGroupRenamed();
            BindGroupElementFixed();
        }
        public void Dispose()
        {
            gui.RemoveManipulator(contentDragger);   
            gui.RemoveManipulator(selectionDragger);   
            gui.RemoveManipulator(rectangleSelector);   
            gui.RemoveManipulator(CreateGroupContextMenu());
            
            inputBinding.Dispose();
            
            DisposeDeleted();
            DisposeGroupElementsAdded();
            DisposeGroupedElementsRemoved();
            DisposeGroupRenamed();
            DisposeGroupElementFixed();
            
            string searchPath = AssetDatabase.GetAssetPath(view.searchWindow);
            if(searchPath.IsNullOrEmpty()) return;
            AssetDatabase.DeleteAsset(searchPath);
        }
    }
}