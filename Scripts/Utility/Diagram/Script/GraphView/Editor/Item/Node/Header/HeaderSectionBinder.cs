using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using Share;

namespace Diagram
{
    public class HeaderSectionBinder : IDisposable
    {
        private HeaderSectionView view;
        private IDiagramNode node;

        public HeaderSectionBinder(IDiagramNode node, HeaderSectionView view)
        {
            this.node = node;
            this.view = view;
        }

        private bool IsAbstract()
        {
            return node.NodeModel.Header.AccessType is AccessType.Internal_Abstract or AccessType.Public_Abstract;
        }

        private bool IsStatic()
        {
            return node.NodeModel.Header.AccessType is AccessType.Internal_Static or AccessType.Public_Static;
        }

        private void BindAccessTypeCycle()
        {
            string currentAccessType = node.ModelModifier.GetNextCycleType<AccessType>();
            view.accessTypeButton.text = currentAccessType;

            if (node.NodeModel.Header.AccessKeyword == AccessKeyword.Enum)
            {
                if (node.NodeModel.Header.AccessType != AccessType.Public &&
                    node.NodeModel.Header.AccessType != AccessType.Internal)
                    BindAccessTypeCycle();
                return;
            }

            if (!IsStatic() &&
                !IsAbstract())
                return;

            if (node is DiagramNormalNode normalNode)
            {
                if (IsStatic())
                {
                    normalNode.ConvertPropertyToStatic();
                    normalNode.ConvertMethodToStatic();
                }
            }

            // abstract 또는 static일때 class가 아니면 조정
            node.NodeModel.Header.AccessKeyword = AccessKeyword.Class;
            view.dataAccessKeywordButton.text = "class";
            view.accessTypeButton.text = currentAccessType;
        }

        private void BindDataAccessKeywordCycle()
        {
            string next = node.ModelModifier.GetNextCycleType<AccessKeyword>();
            view.dataAccessKeywordButton.text = next;

            if (node.NodeModel.Header.AccessKeyword == AccessKeyword.Enum)
            {
                if (node is DiagramNormalNode enumNode)
                {
                    enumNode.ClearProperties();
                    enumNode.ClearMethods();
                    enumNode.SetVisibleEnumSection(true);
                }

                return;
            }

            if (node is DiagramNormalNode normalNode)
            {
                normalNode.SetVisibleEnumSection(false);
            }

            if (!IsStatic() &&
                !IsAbstract())
                return;

            // abstract 또는 static일때 class가 아니면 조정
            view.dataAccessKeywordButton.text = "class";
            node.NodeModel.Header.AccessKeyword = AccessKeyword.Class;
        }

        private void BindEntityTypeCycle()
        {
            string next = node.ModelModifier.GetNextCycleType<EntityType>();
            view.entityTypeButton.text = next;
            
            if (node.NodeModel.Header.EntityType == EntityType.Aggregate) return;
            if(node.NodeModel.Inheritances.Count == 0) return;
                                                                                                                                                                                                                                                                                                    
            if(node.NodeModel.Inheritances.Any(item => item.EntityType != EntityType.ValueObject))
               node.NodeModel.Header.EntityType = EntityType.Aggregate;
        }

        private void BindEventTypeCycle()
        {
            if (!node.SectionFactory.TryGetSection(out PortSection portSection)) return;
            string next = node.ModelModifier.GetNextCycleType<EventType>();
            view.eventTypeButton.text = next;
            portSection.RepaintSubviewOnlyEventNode();
        }

        private void BindNoteExportFileTypeCycle()
        {
            string next = node.ModelModifier.GetNextCycleType<ExportFileType>();
            view.noteExportFileTypeButton.text = next;
        }
        private void BindDescriptionTypeCycle()
        {
            bool isChanged = EditorUtility.DisplayDialog("Warning",
                @"This will remove all the content on the note.
- Do you want to continue?", "Yes", "No");
            
            if(!isChanged) return;
            
            string next = node.ModelModifier.GetNextCycleType<DescriptionType>();
            view.descriptionTypeButton.text = next;

            if (!node.SectionFactory.TryGetSection(out NoteSection noteSection)) return;
            if (noteSection == null) return;
            noteSection.baseNote = noteSection.view.CreateNote();
            
        }

        private void BindClear()
        {
             if (!node.SectionFactory.TryGetSection(out NoteSection noteSection)) return;

             noteSection.baseNote.Clear();
        }

        private void BindNormalNodeNameTextField(FocusOutEvent evt)
        {
            view.nameTextField.value = view.nameTextField.value.ToUpperAt(0);
            
            node.name = view.nameTextField.value;
            
            if (node.SectionFactory.TryGetSection(out PortSection section))
                section.ReloadInjections(node.NodeModel.Header.Name, view.nameTextField.value);
            
            node.NodeModel.Header.Name = view.nameTextField.value;
            node.NodeModel.Status.IsDirty = true;
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        
        private void BindEventNodeNameTextField(FocusOutEvent evt)
        {
            view.nameTextField.value = view.nameTextField.value.ToUpperAt(0);
            node.name = view.nameTextField.value;
            node.NodeModel.Header.Name = view.nameTextField.value;
            node.NodeModel.Status.IsDirty = true;
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        
        private void BindNoteNodeNameTextField(FocusOutEvent evt)
        {
            view.nameTextField.value = view.nameTextField.value.ToUpperAt(0);
            node.name = view.nameTextField.value;
            node.NodeModel.Note.Title = view.nameTextField.value;
            node.NodeModel.Header.Name = view.nameTextField.value;
            node.NodeModel.Status.IsDirty = true;
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }

        private void BindNormalNode()
        {
            view.accessTypeButton.clicked += BindAccessTypeCycle;
            view.dataAccessKeywordButton.clicked += BindDataAccessKeywordCycle;
            view.nameTextField.RegisterCallback<FocusOutEvent>(BindNormalNodeNameTextField);
            view.entityTypeButton.clicked += BindEntityTypeCycle;
        }

        private void BindEventNode()
        {
            view.eventTypeButton.clicked += BindEventTypeCycle;
            view.nameTextField.RegisterCallback<FocusOutEvent>(BindEventNodeNameTextField);
        }

        private void BindNoteNode()
        {
            view.noteExportFileTypeButton.clicked += BindNoteExportFileTypeCycle;
            view.descriptionTypeButton.clicked += BindDescriptionTypeCycle;
            view.nameTextField.RegisterCallback<FocusOutEvent>(BindNoteNodeNameTextField);
            view.clearButton.clicked += BindClear;
        }

        public void Bind()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    BindNormalNode();
                    break;
                case DiagramEventNode:
                    BindEventNode();
                    break;
                case DiagramNoteNode:
                    BindNoteNode();
                    break;
            }
        }

        private void DisposeNormalNode()
        {
            if(view.accessTypeButton != null)
                view.accessTypeButton.clicked -= BindAccessTypeCycle;
            
            if(view.dataAccessKeywordButton != null)
                view.dataAccessKeywordButton.clicked -= BindDataAccessKeywordCycle;
            
            if(view.entityTypeButton != null)
                view.entityTypeButton.clicked -= BindEntityTypeCycle;
            
            view.nameTextField?.UnregisterCallback<FocusOutEvent>(BindNormalNodeNameTextField);
        }

        private void DisposeEventNode()
        {
            if(view.eventTypeButton != null)
                view.eventTypeButton.clicked -= BindEventTypeCycle;
                
            view.nameTextField?.UnregisterCallback<FocusOutEvent>(BindEventNodeNameTextField);
        }

        private void DisposeNoteNode()
        {
            if(view.noteExportFileTypeButton != null)
                view.noteExportFileTypeButton.clicked -= BindNoteExportFileTypeCycle;
            
            if(view.descriptionTypeButton != null)
                view.descriptionTypeButton.clicked -= BindDescriptionTypeCycle;
            
            if(view.clearButton != null)
                view.clearButton.clicked -= BindClear;
            
            view.nameTextField?.UnregisterCallback<FocusOutEvent>(BindNoteNodeNameTextField);
        }

        public void Dispose()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    DisposeNormalNode();
                    break;
                case DiagramEventNode:
                    DisposeEventNode();
                    break;
                case DiagramNoteNode:
                    DisposeNoteNode();
                    break;
            }
        }
    }
}