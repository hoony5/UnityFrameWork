using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEditor.Experimental.GraphView;

namespace Diagram
{
    public class PortSectionView : SectionView
    {
        private IDiagramNode node;
        public SerializedDictionary<string, Port> ports;
        [SerializedDictionary("Parent Node Name, Connection")]
        public readonly SerializedDictionary<string, Edge> allParentNodes;
        public readonly SerializedDictionary<string, Edge> invalidEdges;
        
        public PortSectionView(IDiagramNode node)
        {
            this.node = node;
            ports = new SerializedDictionary<string, Port>();
            allParentNodes = new SerializedDictionary<string, Edge>();
            invalidEdges = new SerializedDictionary<string, Edge>();
        }
        
        private Port AddInputPort(string portName,Type type, Port.Capacity capacity)
        {
            Port port = node.InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, type);
            port.portName = portName;
            node.inputContainer.Add(port);
            return port;
        }
        private Port AddOutputPort(string portName,Type type, Port.Capacity capacity)
        {
            Port port = node.InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, type);
            port.portName = portName;
            node.outputContainer.Add(port);
            return port;
        }

        public void CreateSubview(Dictionary<string, string> pairs)
        {
            foreach (KeyValuePair<string, string> pair in pairs)
            {
                string inputPortName = pair.Key;
                string outputPortName = pair.Value;
                if (!inputPortName.IsNullOrEmpty())
                {
                    Port inputPort = AddInputPort(inputPortName, typeof(object), Port.Capacity.Multi);
                    ports.Add(inputPortName, inputPort);
                }

                if (outputPortName.IsNullOrEmpty()) continue;
                Port outputPort = AddOutputPort(outputPortName, typeof(object), Port.Capacity.Multi);
                ports.Add(outputPortName, outputPort);
            }
        }
        public void Repaint(EventType eventType)
        {
            HidePort(eventType);
            ShowPort(eventType);
        }

        private void ShowPort(EventType eventType)
        {
            bool hasPublishPort = ports.TryGetValue("publish", out Port publishPort);
            bool hasSubscribePort = ports.TryGetValue("subscribe", out Port subscribePort);
            
            switch (eventType)
            {
                default:
                case EventType.Not_Used:
                    break;
                case EventType.Publish when hasPublishPort:
                    publishPort.SetDisplay(true);
                    publishPort.connections?.ForEach(edge => edge.SetDisplay(true));
                    break;
                case EventType.Subscribe when hasSubscribePort:
                    subscribePort.SetDisplay(true);
                    subscribePort.connections?.ForEach(edge => edge.SetDisplay(true));
                    break;
                case EventType.Both when hasPublishPort && hasSubscribePort:
                    publishPort.SetDisplay(true);
                    subscribePort.SetDisplay(true);
                    publishPort.connections?.ForEach(edge => edge.SetDisplay(true));
                    subscribePort.connections?.ForEach(edge => edge.SetDisplay(true));
                    break;
            }
        }
        private void HidePort(EventType eventType)
        {
            bool hasPublishPort = ports.TryGetValue("publish", out Port publishPort);
            bool hasSubscribePort = ports.TryGetValue("subscribe", out Port subscribePort);
            
            switch (eventType)
            {
                default:
                case EventType.Both:
                case EventType.Not_Used:
                    break;
                case EventType.Publish when hasSubscribePort:
                    subscribePort.SetDisplay(false);
                    subscribePort.connections?.ForEach(edge => edge.SetDisplay(false));
                    break;
                case EventType.Subscribe when hasPublishPort:
                    publishPort.SetDisplay(false);
                    publishPort.connections?.ForEach(edge => edge.SetDisplay(false));
                    break;
            }
        }
        public bool TryGetPort(string portName, out Port port)
        {
            return ports.TryGetValue(portName, out port);
        }
    }
}