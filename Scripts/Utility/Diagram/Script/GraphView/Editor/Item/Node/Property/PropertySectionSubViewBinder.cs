using System;
using Share;
using UnityEngine.UIElements;

namespace Diagram
{
    public class PropertySectionSubViewBinder : IDisposable
    {
        private IDiagramNode node;
        public PropertySectionSubView subView;
        public PropertyFieldInfo fieldInfo;

        public PropertySectionSubViewBinder(IDiagramNode node, PropertySectionSubView subView, PropertyFieldInfo fieldInfo)
        {
            this.node = node;
            this.subView = subView;
            this.fieldInfo = fieldInfo;
        }
        private void BindTypeTextField(FocusOutEvent evt)
        {
            if (fieldInfo.Type == subView.typeTextField.text) return;
            fieldInfo.Type = subView.typeTextField.text;
            node.NodeModel.Status.IsDirty = true;
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }

        private void BindNameTextField(FocusOutEvent evt)
        {
            if (fieldInfo.Name == subView.nameTextField.text) return;
            fieldInfo.Name = subView.nameTextField.text;
            node.NodeModel.Status.IsDirty = true;
            
            node.GUI.gptBoard.viewModel.DrawTreeView();
            node.GUI.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }
        private void ClearDeclarationTypeOnPropertyType()
        {
            fieldInfo.Type = fieldInfo.Type.Erase("abstract");
            fieldInfo.Type = fieldInfo.Type.Erase("virtual");
            fieldInfo.Type = fieldInfo.Type.Erase("static");    
        }

        private bool IsValidatedAbstractMember(string scriptAccessKeywordType)
        {
            //when class is abstract, property can any declaration type
            if (scriptAccessKeywordType.Contains("abstract", StringComparison.OrdinalIgnoreCase)) return true;
            // but when class is not abstract, property can't be abstract
            return fieldInfo.DeclarationType != DeclarationType.Abstract;
        }

        private bool IsValidatedStaticMember(string scriptAccessKeywordType)
        {
            // when class is not static, property can any declaration type
            if(!scriptAccessKeywordType.Contains("static", StringComparison.OrdinalIgnoreCase)) return true;
            // but when class is static, property can't be static
            return fieldInfo.DeclarationType == DeclarationType.Static;
        }
        private void SetNextPropertyDeclarationType()
        {
            int maxLength = Enum.GetNames(typeof(DeclarationType)).Length;
            fieldInfo.DeclarationType = (DeclarationType)(((int)fieldInfo.DeclarationType + 1) % maxLength);
            subView.declarationTypeRepeatButton.text = fieldInfo.DeclarationType.ToString();

            string nodeAccessType = node.ModelModifier.GetNodeViewTypeName<AccessType>();
            
            if(!IsValidatedAbstractMember(nodeAccessType))
                SetNextPropertyDeclarationType();
            
            if(!IsValidatedStaticMember(nodeAccessType))
                SetNextPropertyDeclarationType();
        }

        private void BindDeclarationTypeCycle()
        {
            ClearDeclarationTypeOnPropertyType();
            SetNextPropertyDeclarationType();
            node.NodeModel.Status.IsDirty = true;
        }

        private void BindNormalNode()
        {
            subView.typeTextField.RegisterCallback<FocusOutEvent>(BindTypeTextField);
            subView.nameTextField.RegisterCallback<FocusOutEvent>(BindNameTextField);
            subView.declarationTypeRepeatButton.clicked += BindDeclarationTypeCycle;
        }

        private void BindEventNode()
        {
            subView.typeTextField.RegisterCallback<FocusOutEvent>(BindTypeTextField);
        }
        public void Bind()
        {
            // when created without PropertyFieldInfo (ex. addButton) then return
            if (fieldInfo == null) return;
            
            if (node is DiagramNormalNode)
                BindNormalNode();
            
            if (node is DiagramEventNode)
                BindEventNode();
        }
        public void BindRemoveButton(Action action)
        {
            if(subView.removeButton == null) return;
            subView.removeButton.clicked += action;
        }

        private void DisposeNormalNode()
        {
            subView.typeTextField?.UnregisterCallback<FocusOutEvent>(BindTypeTextField);
            subView.nameTextField?.UnregisterCallback<FocusOutEvent>(BindNameTextField);
            if (subView.declarationTypeRepeatButton == null) return;
            subView.declarationTypeRepeatButton.clicked -= BindDeclarationTypeCycle;
        }

        private void DisposeEventNode()
        {
            subView.typeTextField?.UnregisterCallback<FocusOutEvent>(BindTypeTextField);
        }
        public void Dispose()
        {
            if (node is DiagramNormalNode)
                DisposeNormalNode();
            
            if (node is DiagramEventNode)
                DisposeEventNode();
            
            subView.group.RemoveFromHierarchy();
        }
        
        public void ConvertToStatic()
        {
            ClearDeclarationTypeOnPropertyType();
            fieldInfo.DeclarationType = DeclarationType.Static;
            subView.declarationTypeRepeatButton.text = fieldInfo.DeclarationType.ToString();
            node.NodeModel.Status.IsDirty = true;
        }

        // already implemented in the PropertySection
        public void ConvertToEnumSection(bool visible)
        {
            subView.declarationTypeRepeatButton?.SetDisplay(visible);
        }

    }
}