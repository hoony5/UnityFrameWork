using System;
using System.Collections.Generic;
using System.Linq;
using Share;
using UnityEditor.Experimental.GraphView;

namespace Diagram
{
    public class PortSectionBinder : IDisposable
    {
        private IDiagramNode node;
        private PortSectionView view;

        public PortSectionBinder(IDiagramNode node, PortSectionView view)
        {
            this.node = node;
            this.view = view;
        }

        public void Bind()
        {
            node.RegisterOnGraphChanged(OnConnectEdge);
        }

        public void Dispose()
        {
            node.UnregisterOnGraphChanged(OnConnectEdge);
        }

        #region Update Model Methods

        private void AddToModelInheritanceNode(Edge edge)
        {
            if (!edge.IsInheritancePort()) return;
            if (edge.output.node is not IDiagramNode parent) return;
            
            if (!node.ModelModifier.TryAddInheritance(parent.NodeModel))
            {
                _ = view.invalidEdges.TryAdd(parent.ID, edge);
                return;
            }

            // addition inheritance relationship
            if (node.GUI.inheritanceRelationships.ContainsKey(parent.NodeModel.Header.Name))
            {
                if (node.GUI.inheritanceRelationships[parent.NodeModel.Header.Name].Contains(node.NodeModel.Header.Name))
                    return;

                node.GUI.inheritanceRelationships[parent.NodeModel.Header.Name].Add(node.NodeModel.Header.Name);

                return;
            }

            node.GUI.inheritanceRelationships.Add(parent.NodeModel.Header.Name, new List<string> { node.NodeModel.Header.Name });
        }

        private void AddToModelInjectionNode(Edge edge)
        {
            if (!edge.IsInjectionPort()) return;
            if(edge.output.node is not IDiagramNode parent) return;
            
            if (node.ModelModifier.TryAddInjection(parent.NodeModel))
                return;
            
            _ = view.invalidEdges.TryAdd(parent.ID, edge);
        }

        private void AddToModelPublishingEventNode(Edge edge)
        {
            if (!edge.IsEventPort()) return;
            if(edge.output.node is not DiagramNormalNode parent) return;
            
            if (parent.ModelModifier.TryAddPublishingEvents(node.NodeModel)) 
                return;
            
            _ = view.invalidEdges.TryAdd(parent.ID, edge);
        }

        private void AddToModelSubscribingEventNode(Edge edge)
        {
            if (!edge.IsEventPort()) return;
            if(edge.output.node is not DiagramEventNode parent) return;
            
            if (node.ModelModifier.TryAddSubscribingEvents(parent.NodeModel)) 
                return;
            
            _ = view.invalidEdges.TryAdd(node.ID, edge);
        }

        private void AddToModelNoteNode(Edge edge)
        {
            if (!edge.IsNotePort()) return;
            if(edge.output.node is not IDiagramNode parent) return;
            
            if (node.ModelModifier.TryAddNoteNode(parent.NodeModel))
                return;
            
            _ = view.invalidEdges.TryAdd(parent.ID, edge);
        }

        private void AddToModelConnectedNodes()
        {
            foreach (Edge edge in view.allParentNodes.Values)
            {
                AddToModelInheritanceNode(edge);
                AddToModelInjectionNode(edge);
                AddToModelPublishingEventNode(edge);
                AddToModelSubscribingEventNode(edge);
                AddToModelNoteNode(edge);
            }
        }

        #endregion
        
        private void AddImplementItems()
        {
            foreach (Edge edge in view.allParentNodes.Values)
            {
                if (edge.output.node is not DiagramNormalNode parent)
                    continue; // only normal Node parent information is saved

                if (edge.IsInheritancePort() &&
                    edge.NeedImplementClassPort())
                {
                    // if the parent is not an interface, abstract class, it is not implemented
                    // add parent member to child
                    node.ModelModifier.AddInheritProperty(parent.NodeModel);
                    node.ModelModifier.AddInheritMethod(parent.NodeModel);
                    node.Reload();
                    continue;
                }

                // add parent to child
                if (edge.IsInjectionPort())
                {
                    ((DiagramNormalNode)node)
                        .propertyFieldSection
                        .AddPropertyField(
                            parent.NodeModel.Header.Name,
                            parent.NodeModel.Header.Name);
                    node.Reload();
                }
            }
        }

        private void UpdateEdges(GraphViewChange change)
        {
            view.allParentNodes.Clear();

            change.edgesToCreate
                .Where(edge => ((IDiagramNode)edge.input.node).ID.Equals(node.ID))
                .ForEach(edge =>
                {
                    // prevent duplicate edges
                    if (edge.output.node is not IDiagramNode parent) return;
                    _ = view.allParentNodes.TryAdd(parent.ID, edge);
                });
        }

        private void RemoveInvalidatedEdges(GraphViewChange change)
        {
            foreach (Edge edge in view.allParentNodes.Values)
            {
                if (edge.IsValidatedPairOn()) continue;
                if (edge.output.node is not IDiagramNode parent) continue;
                
                _ = view.invalidEdges.TryAdd(parent.ID, edge);
            }

            if (view.invalidEdges.Count == 0) return;
            
            foreach (Edge edge in view.invalidEdges.Values)
            {
                edge.input.Disconnect(edge);
                edge.output.Disconnect(edge);
                // delete edge from GraphView
                edge.RemoveFromHierarchy();
            }

            change.edgesToCreate.RemoveAll(edge => view.invalidEdges.ContainsValue(edge));
            view.invalidEdges.ForEach(invalidatedEdge =>
            {
                if (invalidatedEdge.Value.output.node is not DiagramNodeBase parent) return;

                if (parent.NodeModel.Header.Name.Equals(node.NodeModel.Header.Name))
                {
                    view.allParentNodes.SafeRemove(parent.ID);
                    node.ModelModifier.RemoveConnectionInfo(parent.NodeModel);
                }

                if (invalidatedEdge.Value.IsValidatedConnectionCount()) return;
                view.allParentNodes.SafeRemove(parent.ID);
                node.ModelModifier.RemoveConnectionInfo(parent.NodeModel);
            }); 
            view.invalidEdges.Clear();
        }

        private GraphViewChange OnConnectEdge(GraphViewChange change)
        {
            if (change.edgesToCreate == null) return change;
            UpdateEdges(change);
            if (view.allParentNodes.Count == 0) return change;
            AddToModelConnectedNodes();
            RemoveInvalidatedEdges(change);
            AddImplementItems();
            return change;
        }
    }
}