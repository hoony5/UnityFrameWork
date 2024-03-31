using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEditor.Experimental.GraphView;

namespace Diagram
{
    public class PortSection : ISectionFactory
    {
        private IDiagramNode node;
        private PortSectionView view;
        private PortSectionBinder binder;
        
        [SerializedDictionary("input", "output")]
        public readonly SerializedDictionary<string, string> PortPairs;
        public PortSection(IDiagramNode node, params (string input, string output)[] pairs)
        {
            this.node = node;
            view = new PortSectionView(node);
            PortPairs = new SerializedDictionary<string, string>();
            foreach ((string input, string output) in pairs)
            {
                PortPairs.Add(input, output);
            }
        }
        public void Setup()
        {
            view.CreateSubview(PortPairs);
            binder = new PortSectionBinder(node, view);
            binder.Bind();
        }

        public void RepaintSubviewOnlyEventNode()
        {
            if(node is not DiagramEventNode) return;
            view.Repaint(node.NodeModel.Header.EventType);
        }

        public void Load()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    LoadNormalNodeEdges();
                    break;
                case DiagramEventNode:
                    LoadEventNodeEdges();
                    break;
                case DiagramNoteNode:
                    LoadNoteNodeEdges();
                    break;
            }
        }

        public void ReloadInjections(string old, string @new)
        {
            _ = view.ports.TryGetValue("provide", out Port providePort);
            
            if (providePort == null) return;

            foreach (Edge edge in providePort.connections)
            {
                if(edge.input.node is not DiagramNormalNode child) continue;
                child.propertyFieldSection.ReloadInjections(old, @new);
            }
        }

        public void Reload()
        {
            Dispose();
            Setup();
            Load();
        }

        private void LoadEdge(IDiagramNode target, string parentOutput, string childInput)
        {
            Edge edge = new Edge();
            
            // find input
            if(!view.TryGetPort(childInput, out Port inputPort)) return;
            edge.input = inputPort;
            
            // find output
            PortSection otherPortSection = target.SectionFactory.TryGetSection(out PortSection portSection) ? portSection : null;
            if(otherPortSection == null) return;
            if(!otherPortSection.view.TryGetPort(parentOutput, out Port outputPort)) return;
            
            edge.output = outputPort;

            // connect
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            node.GUI.AddElement(edge);
        }

        private void LoadNormalNodeEdges()
        {
            LoadInheritanceEdges();
            LoadEventEdges();
            LoadNoteEdges();
            LoadInjectionEdges();
        }

        private void LoadEventNodeEdges()
        {
            LoadEventEdges();
            LoadNoteEdges();
        }
        private void LoadNoteNodeEdges()
        {
            LoadNoteEdges();
        }

        private void LoadEdgeInternal(DiagramHeaderModel targetModel, string targetOutput,
            string thisInput, Predicate<CustomNode> predicate = null)
        {
            if (!node.GUI.view.diagramNodes.TryGetValue(targetModel.ID, out DiagramNodeBase connection)) return;
            if(predicate == null)
            {
                LoadEdge(connection, targetOutput, thisInput);
                return;
            }
            if(!predicate(connection)) return;
            LoadEdge(connection, targetOutput, thisInput);
        }
        private void LoadEdgeInternal(ICollection<DiagramHeaderModel> targetModels, string targetOutput, string thisInput, Predicate<CustomNode> predicate = null)
        {
            if(targetModels == null) return;
            if(targetModels.Count == 0) return;
            
            foreach (DiagramHeaderModel targetModel in targetModels)
            {
                if (!node.GUI.view.diagramNodes.TryGetValue(targetModel.ID, out DiagramNodeBase connection)) 
                    continue;
                
                if(predicate == null)
                {
                    LoadEdge(connection, targetOutput, thisInput);
                    continue;
                }
                
                if(!predicate(connection)) continue;
                LoadEdge(connection, targetOutput, thisInput);
            }
        }

        private void LoadInheritanceEdges()
        {
            LoadEdgeInternal(node.NodeModel.Inheritances,
                "child",
                "parent",
                normalNode => normalNode is DiagramNormalNode);
        }

        private void LoadInjectionEdges()
        {
            LoadEdgeInternal(node.NodeModel.Injections,
                "provide",
                "inject",
                normalNode => normalNode is DiagramNormalNode);
        }
        
        private void LoadEventEdges()
        {
            LoadEdgeInternal(node.NodeModel.SubscribingEvents,
                "publish",
                "subscribe");
            if(node.NodeModel.PublishingEvents.Count == 0) return;
            
            PortSection[] targetNodes = node.NodeModel.PublishingEvents
                .Select(targetNode => node.GUI.view.diagramNodes[targetNode.ID])
                .Select(targetNode => targetNode.SectionFactory.TryGetSection(out PortSection portSection) ? portSection : null)
                .Where(portSection => portSection != null)
                .ToArray();
            
            foreach (PortSection portSection in targetNodes)
            {
                portSection.LoadEdgeInternal(node.NodeModel.Header,
                    "publish",
                    "subscribe");
            }
        }
        
        private void LoadNoteEdges()
        {
            LoadEdgeInternal(node.NodeModel.NoteNodes,
                "note",
                node is DiagramNoteNode ? "baseNote" : "note");
        }
        public void Dispose()
        {
            binder.Dispose();
        }
    }
}