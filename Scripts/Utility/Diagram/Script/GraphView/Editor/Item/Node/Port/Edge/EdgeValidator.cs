using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace Diagram
{
    public static class EdgeValidator
    {
        
        #region Validated Port Conditions

        private static bool IsPairPort(this Edge edge, string inputPortName, string outputPortName)
        {
            return edge.input.portName.Equals(inputPortName) && edge.output.portName.Equals(outputPortName);
        }

        public static bool IsInheritancePort(this Edge edge)
        {
            // edge.input.Node = "parent"
            return edge.IsPairPort("parent", "child");
        }

        public static bool IsNotePort(this Edge edge)
        {
            return edge.input.node is DiagramNoteNode
                ? edge.IsPairPort("baseNote", "note")
                : edge.IsPairPort("note", "note");
        }

        public static bool IsInjectionPort(this Edge edge)
        {
            return edge.IsPairPort("inject", "provide");
        }
        public static bool IsEventPort(this Edge edge)
        {
            return edge.IsPairPort("subscribe", "publish");
        }

        private static bool OutputIsOf<TNode>(this Edge edge) where TNode : Node
        {
            return edge.output.node is TNode;
        }
        private static bool InputIsOf<TNode>(this Edge edge) where TNode : Node
        {
            return edge.input.node is TNode;
        }

        private static bool IsSelfInheritance(this Edge edge)
        {
            // self inheritance is not allowed.
            // output is parent, input is child
            return edge.input.node.name.Equals(edge.output.node.name);
        }

        private static bool HasValidatedParentWhenNormalNode(this Edge edge)
        {
            if (edge.IsSelfInheritance()) // self inheritance is not allowed
                return false;

            if (edge.OutputIsOf<DiagramNormalNode>())
            {
                // skip event subscribe and publish port
                if (edge.IsEventPort()) return false;
                
                if (edge.IsInheritancePort())
                    return edge.IsValidatedScriptInheritance();
                
                return IsInjectionPort(edge) || edge.IsNotePort();
            }

            if (edge.OutputIsOf<DiagramEventNode>())
            {
                return edge.IsEventPort();
            }

            return edge.OutputIsOf<DiagramNoteNode>() &&
                   IsNotePort(edge);
        }

        private static bool HasValidatedParentWhenEventNode(this Edge edge)
        {
            if (edge.OutputIsOf<DiagramNormalNode>())
            {
                if (edge.IsEventPort())
                    return true;
            }

            if (edge.OutputIsOf<DiagramEventNode>())
            {
                return false;
            }

            return edge.OutputIsOf<DiagramNoteNode>() && edge.IsNotePort();
        }

        private static bool HasValidatedParentWhenNoteNode(this Edge edge)
        {
            if (edge.OutputIsOf<DiagramNormalNode>())
            {
                return false;
            }

            if (edge.OutputIsOf<DiagramEventNode>())
            {
                return false;
            }

            return edge.OutputIsOf<DiagramNoteNode>() && edge.IsNotePort();
        }

        public static bool IsValidatedPairOn(this Edge edge)
        {
            if (edge.InputIsOf<DiagramNormalNode>())
                return edge.HasValidatedParentWhenNormalNode();
            if (edge.InputIsOf<DiagramEventNode>())
                return edge.HasValidatedParentWhenEventNode();

            return edge.InputIsOf<DiagramNoteNode>() && edge.HasValidatedParentWhenNoteNode();
        }

        public static bool IsDisconnectedEdge(this Port port, Edge edge)
        {
            foreach (Edge connection in port.connections)
            {
                if (connection.input.node.name.Equals(edge.input.node.name) &&
                    connection.output.node.name.Equals(edge.output.node.name))
                    return false;

                return true;
            }
            
            return true;
        }

        private static bool IsValidatedClassInheritance(DiagramNormalNode child)
        {
            bool isNormalAccessType = child.NodeModel.Header.AccessType is AccessType.Internal or AccessType.Public;
            bool isAbstract = child.NodeModel.Header.AccessType is AccessType.Internal_Abstract or AccessType.Public_Abstract;
            
            if (child.NodeModel.Inheritances.Any(connection =>
                    connection.AccessKeyword is AccessKeyword.Struct 
                        or AccessKeyword.Record 
                        or AccessKeyword.Enum))
            {
                return false;
            }

            if (isNormalAccessType)
            {
                if (child.NodeModel.Inheritances.Count(
                        connection => connection.AccessKeyword is AccessKeyword.Class) <= 1)
                    return true;
            }

            if (isAbstract)
            {
                return child.NodeModel.Inheritances
                           .Count(connection => connection.AccessKeyword is AccessKeyword.Class &&
                                                connection.AccessType is AccessType.Internal_Abstract
                                                    or AccessType.Public_Abstract) <= 1;
            }

            return false;
        }

        private static bool IsValidatedRecordInheritance(DiagramNormalNode child)
        {
            bool isNormalAccessType = child.NodeModel.Header.AccessType is AccessType.Internal or AccessType.Public;
            if (!isNormalAccessType) 
                return false;
            
            if(child.NodeModel.Inheritances.Any(connection => connection.AccessKeyword 
                   is AccessKeyword.Class 
                   or AccessKeyword.Struct 
                   or AccessKeyword.Enum))
            {
                return false;
            }
                
            return child.NodeModel.Inheritances.Count(connection => connection.AccessKeyword is AccessKeyword.Record) <= 1;
        }

        private static bool IsValidatedStructInheritance(DiagramNormalNode child)
        {
            return child.NodeModel.Inheritances.Count(connection =>
                connection.AccessKeyword is AccessKeyword.Interface) >= 0;
        }
        private static bool IsValidatedInterfaceInheritance(DiagramNormalNode child)
        {
            return child.NodeModel.Header.AccessKeyword == AccessKeyword.Interface && 
                   child.NodeModel.Inheritances.Count(connection =>
                connection.AccessKeyword is AccessKeyword.Interface) >= 0;
        }

        private static bool IsValidatedScriptInheritance(this Edge edge)
        {
            if (edge.input.node is not DiagramNormalNode child) return false;
            if (edge.output.node is not DiagramNormalNode) return false;

            if (child.NodeModel.Inheritances.Count == 0) return true;

            return child.NodeModel.Header.AccessKeyword switch
            {
                AccessKeyword.Class => IsValidatedClassInheritance(child),
                AccessKeyword.Record => IsValidatedRecordInheritance(child),
                AccessKeyword.Struct => IsValidatedStructInheritance(child),
                AccessKeyword.Interface => IsValidatedInterfaceInheritance(child),
                AccessKeyword.Enum => false,
                _ => false
            };
        }

        public static bool IsValidatedConnectionCount(this Edge edge)
        {
            return edge.input.connections.Count(connection
                => connection.input.Equals(edge.input) &&
                   connection.output.Equals(edge.output)) <= 1;
        }

        public static bool NeedImplementClassPort(this Edge edge)
        {
            if (edge.output.node is not DiagramNormalNode parent)
                return false;

            return parent.NodeModel.Header.AccessKeyword == AccessKeyword.Interface ||
                   parent.NodeModel.Header.AccessType 
                       is AccessType.Internal_Abstract
                       or AccessType.Public_Abstract;
        }

        #endregion

    }
}