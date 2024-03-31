using System;
using System.Collections.Generic;
using System.Linq;

namespace Diagram
{
    public class DiagramModelModifier
    {
        public DiagramNodeModel NodeModel;
        public DiagramModelModifier(DiagramNodeModel nodeModel)
        {
            this.NodeModel = nodeModel;
        }
        
        public void AddInheritProperty(DiagramNodeModel parent)
        {
            parent.Header.Properties.ForEach(parentProperty =>
            {
                bool exist = NodeModel.Header.Properties.Exists(childProperty =>
                {
                    if(!childProperty.Name.Equals(parentProperty.Name)) return false;
                    childProperty.IsInherited = true;
                    return true;
                });
                if(exist) return;
                NodeModel.Header.Properties.Add(parentProperty);
                NodeModel.Status.IsDirty = true;
            });
        }

        private void RemoveInheritProperty(DiagramNodeModel parent)
        {
            parent.Header.Properties.ForEach(parentProperty =>
            {
                NodeModel.Header.Properties.RemoveAll(childProperty => childProperty.Name.Equals(parentProperty.Name));
                NodeModel.Status.IsDirty = true;
            });
        }

        private void RemoveInjectionProperty(DiagramNodeModel parent)
        {
            NodeModel.Header.Properties.RemoveAll(childProperty => 
                childProperty.Type.Equals(parent.Header.Name) &&
                childProperty.Name.Equals(parent.Header.Name, StringComparison.OrdinalIgnoreCase));
            NodeModel.Status.IsDirty = true;
        }
        public void AddInheritMethod(DiagramNodeModel parent)
        {
            parent.Header.Methods.ForEach(parentMethod =>
            {
                bool exist = NodeModel.Header.Methods.Exists(childMethod =>
                {
                    if(!childMethod.IsMatch(parentMethod)) return false;
                    childMethod.IsInherited = true;
                    return true;
                });
                if(exist) return;
                NodeModel.Header.Methods.Add(parentMethod);
                NodeModel.Status.IsDirty = true;
            });
        }

        private void RemoveInheritMethod(DiagramNodeModel parent)
        {
            parent.Header.Methods.ForEach(parentMethod =>
            {
                NodeModel.Header.Methods.RemoveAll(childMethod => childMethod.IsMatch(parentMethod));
                NodeModel.Status.IsDirty = true;
            });
        }
        public Type[] GetInheritanceTypes()
        {
            return NodeModel.Inheritances.Select(item => item.GetType()).ToArray();
        }

        public string GetNodeViewTypeName<TEnum>() where TEnum : Enum
        {
            if(typeof(TEnum) == typeof(AccessType))
            {
                return GetAccessTypeName();
            }
            if(typeof(TEnum) == typeof(AccessKeyword))
            {
                return GetAccessKeywordName();
            }
            if(typeof(TEnum) == typeof(EventType))
            {
                return GetEventTypeName();
            }
            if(typeof(TEnum) == typeof(ExportFileType))
            {
                return GetExportTypeName();
            }
            if(typeof(TEnum) == typeof(DescriptionType))
            {
                return GetDescriptionTypeName();
            }
            return string.Empty;
        }
        
        private string GetAccessKeywordName()
        {
            return NodeModel.Header.AccessKeyword.ToString().ToLower();
        }
        private string GetAccessTypeName()
        {
            return NodeModel.Header.AccessType.ToString().ToLower().Replace("_", " ");
        }
        private string GetEventTypeName()
        {
            string result = NodeModel.Header.EventType.ToString().ToLower().Replace("_", " ");
            return result.Equals("both") ? "publish / subscribe" : result;
        }
        private string GetDescriptionTypeName()
        {
            return NodeModel.Note.DescriptionType.ToString().ToLower().Replace("_", " ");
        }
        private string GetExportTypeName()
        {
            return NodeModel.Note.ExportFileType.ToString().ToLower().Replace("_", " ");
        }

        public string GetNextCycleType<TEnum>()
        {
            if(typeof(TEnum) == typeof(AccessType))
            {
                return GetNextCycleAccessType();
            }
            if(typeof(TEnum) == typeof(AccessKeyword))
            {
                return GetNextCycleAccessKeywordType();
            }
            if(typeof(TEnum) == typeof(EntityType))
            {
                return GetNextCycleEntityType();
            }
            if(typeof(TEnum) == typeof(EventType))
            {
                return GetNextCycleEventType();
            }
            if(typeof(TEnum) == typeof(ExportFileType))
            {
                return GetNextCycleExportType();
            }
            if(typeof(TEnum) == typeof(DescriptionType))
            {
                return GetNextCycleDescriptionType();
            }
            
            return string.Empty;
        }
        private string GetNextCycleDescriptionType()
        {
            int maxLength = Enum.GetNames(typeof(DescriptionType)).Length;
            NodeModel.Note.DescriptionType = (DescriptionType)(((int)NodeModel.Note.DescriptionType + 1) % maxLength);
            if (NodeModel.Note.DescriptionType == DescriptionType.Not_Used) return GetNextCycleDescriptionType();
            return GetDescriptionTypeName();
        }

        private string GetNextCycleExportType()
        {
            int maxLength = Enum.GetNames(typeof(ExportFileType)).Length;
            NodeModel.Note.ExportFileType = (ExportFileType)(((int)NodeModel.Note.ExportFileType + 1) % maxLength);
            if (NodeModel.Note.ExportFileType == ExportFileType.Not_Used) return GetNextCycleExportType();
            return GetExportTypeName();
        }

        private string GetNextCycleEventType()
        {
            int maxLength = Enum.GetNames(typeof(EventType)).Length;
            NodeModel.Header.EventType = (EventType)(((int)NodeModel.Header.EventType + 1) % maxLength);
            if (NodeModel.Header.EventType == EventType.Not_Used) return GetNextCycleEventType();
            return GetEventTypeName();
        }
        
        private string GetNextCycleAccessType()
        {
            int maxLength = Enum.GetNames(typeof(AccessType)).Length;
            NodeModel.Header.AccessType = (AccessType)(((int)NodeModel.Header.AccessType + 1) % maxLength);
            return GetAccessTypeName();
        }
        
        private string GetNextCycleAccessKeywordType()
        {
            int maxLength = Enum.GetNames(typeof(AccessKeyword)).Length;
            NodeModel.Header.AccessKeyword = (AccessKeyword)(((int)NodeModel.Header.AccessKeyword + 1) % maxLength);
            return GetAccessKeywordName();
        }

        private string GetNextCycleEntityType()
        {
            int maxLength = Enum.GetNames(typeof(EntityType)).Length;
            NodeModel.Header.EntityType = (EntityType)(((int)NodeModel.Header.EntityType + 1) % maxLength);
            return NodeModel.Header.EntityType.ToString();
        }

        public void AddOrUpdateProperty(PropertyFieldInfo fieldInfo)
        {
            NodeModel.Status.IsDirty = true;
            int index = NodeModel.Header.Properties.FindIndex(p => p.ScriptName == fieldInfo.ScriptName && p.Name == fieldInfo.Name);
            if(index != -1)
            {
                NodeModel.Header.Properties[index] = fieldInfo;
                return;
            }
            NodeModel.Header.Properties.Add(fieldInfo);
        }

        public void AddOrUpdateMethod(MethodInfo fieldInfo)
        {
            NodeModel.Status.IsDirty = true;
            int index = NodeModel.Header.Methods.FindIndex(p => p.ScriptName == fieldInfo.ScriptName && p.Name == fieldInfo.Name);
            if(index != -1)
            {
                NodeModel.Header.Methods[index] = fieldInfo;
                return;
            }
            NodeModel.Header.Methods.Add(fieldInfo);
        }

        public void RemoveProperty(PropertyFieldInfo fieldInfo)
        {
            if(!NodeModel.Header.Properties.Contains(fieldInfo)) return;
            NodeModel.Header.Properties.Remove(fieldInfo);
            NodeModel.Status.IsDirty = true;
        }

        public void RemoveMethod(MethodInfo fieldInfo)
        {
            if(!NodeModel.Header.Methods.Contains(fieldInfo)) return;
            NodeModel.Header.Methods.Remove(fieldInfo);
            NodeModel.Status.IsDirty = true;
        }

        private bool TryAddModel(List<DiagramHeaderModel> headerModels, DiagramNodeModel parent)
        {
            if(headerModels.Contains(parent.Header)) return false;
            headerModels.Add(parent.Header);
            NodeModel.Status.IsDirty = true;
            return true;
        }
        public bool TryAddInheritance(DiagramNodeModel parent)
        {
            return  TryAddModel(NodeModel.Inheritances, parent);
        }
        public bool TryAddPublishingEvents(DiagramNodeModel child)
        {
            child.Header.Properties.ForEach(p => p.ScriptName = NodeModel.Header.Name);
            child.Header.Methods.ForEach(m => m.ScriptName = NodeModel.Header.Name);
            child.Status.GraphElementType = GraphElementType.Event;
            return TryAddModel(NodeModel.PublishingEvents, child);
        }
        public bool TryAddSubscribingEvents(DiagramNodeModel parent)
        {
            parent.Header.Properties.ForEach(p => p.ScriptName = parent.Header.Name);
            parent.Header.Methods.ForEach(m => m.ScriptName = parent.Header.Name);
            parent.Status.GraphElementType = GraphElementType.Event;
            return TryAddModel(NodeModel.SubscribingEvents, parent);
        }
        public bool TryAddNoteNode(DiagramNodeModel parent)
        {
            return TryAddModel(NodeModel.NoteNodes, parent);
        }
        public bool TryAddInjection(DiagramNodeModel parent)
        {
            return TryAddModel(NodeModel.Injections, parent);
        }

        private bool TryRemoveModel(List<DiagramHeaderModel> Models, DiagramNodeModel parent)
        {
            if(Models.Count == 0) return false;
            if(!Models.Contains(parent.Header)) return false;
            Models.RemoveAll(item => item.ID.Equals(parent.Header.ID));
            NodeModel.Status.IsDirty = true;
            return true;
        }

        private bool TryRemovePublishers(DiagramNodeModel parent)
        {
            return TryRemoveModel(NodeModel.PublishingEvents, parent);
        }

        private bool TryRemoveSubscribers(DiagramNodeModel parent)
        {
            return TryRemoveModel(NodeModel.SubscribingEvents, parent);
        }

        private bool TryRemoveInjection(DiagramNodeModel parent)
        {
            return TryRemoveModel(NodeModel.Injections, parent);
        }

        private bool TryRemoveNoteNode(DiagramNodeModel parent)
        {
            return TryRemoveModel(NodeModel.NoteNodes, parent);
        }

        private bool TryRemoveInheritance(DiagramNodeModel parent)
        {
            if(NodeModel.Inheritances.Count == 0) return false;
            NodeModel.Inheritances.RemoveAll(item => item.ID.Equals(parent.Header.ID));
            NodeModel.Status.IsDirty = true;
            return true;
        }

        public void RemoveConnectionInfo(DiagramNodeModel parentNodeModel)
        {
            if (TryRemoveInheritance(parentNodeModel))
            {
                RemoveInheritMethod(parentNodeModel);
                RemoveInheritProperty(parentNodeModel);
            }

            if (TryRemoveInjection(parentNodeModel))
            {
                RemoveInjectionProperty(parentNodeModel);
            }
            
            _ = TryRemovePublishers(parentNodeModel);
            _ = TryRemoveSubscribers(parentNodeModel);
            _ = TryRemoveNoteNode(parentNodeModel);
        }
    }

}