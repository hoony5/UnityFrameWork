using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramNodeBase : CustomNode
    {
        protected LogSection logSection;
        protected DiagramNodeBase(DiagramGUI gui, DiagramNodeModel nodeModel) : base(gui, nodeModel)
        {
            base.SetPosition(new Rect(NodeModel.Header.Position, NodeModel.Header.Size));
        }

        public override void Load()
        {
            SectionFactory.Load();
            RefreshExpandedState();
            
            NodeModel.Status.NeedSavingDiagram = false;
        }
        public override void Dispose()
        {
            ClearPorts(inputContainer);
            ClearPorts(outputContainer);
        }
        public void ClearProperties()
        {
            NodeModel.Header.Properties.Clear();
        }
        public void ClearMethods()
        {
            NodeModel.Header.Methods.Clear();
        }
        private void ClearPorts (VisualElement node)
        {
            foreach(VisualElement visualElement in node.Children())
            {
                Port port = (Port)visualElement;
                if(!port.connected) continue;
                foreach (Edge connection in port.connections)
                {
                    if(!connection.output.node.name.Equals(NodeModel.Header.Name)) continue;
                    if(connection.input.node is not DiagramNormalNode child) continue;
                    
                    Debug.Log($"Disconnecting {NodeModel.Header.Name} from {child.NodeModel.Header.Name}");
                    child.ModelModifier.RemoveConnectionInfo(NodeModel);
                    
                    child.Reload();
                    child.Reload();
                }
                GUI.DeleteElements(port.connections);
            }
        }
    }

}