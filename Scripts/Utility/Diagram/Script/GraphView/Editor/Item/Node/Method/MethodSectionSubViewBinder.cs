using System;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class MethodSectionSubViewBinder : IDisposable
    {
        private MethodSectionSubView subView;
        private MethodInfo fieldInfo;
        private IDiagramNode node;

        public MethodSectionSubViewBinder(IDiagramNode node, MethodSectionSubView subView, MethodInfo fieldInfo)
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
        }

        private void BindNameTextField(FocusOutEvent evt)
        {
            if (fieldInfo.Name == subView.nameTextField.text) return;
            fieldInfo.Name = subView.nameTextField.text;
            node.NodeModel.Status.IsDirty = true;
        }


        private void BindEventNodeNameTextField(FocusOutEvent evt)
        {
            if (fieldInfo.Name == subView.nameTextField.text) return;
            fieldInfo.Name = subView.nameTextField.text;
            fieldInfo.Type = node.NodeModel.Header.Properties[0].Type;
            node.NodeModel.Status.IsDirty = true;
        }

        private void BindPredicateTextField(FocusOutEvent evt)
        {
            if (fieldInfo.Predicate == subView.eventPredicateTextField.text) return;
            fieldInfo.Predicate = subView.eventPredicateTextField.text;
            node.NodeModel.Status.IsDirty = true;
        }

        private void BindParameterTextFieldWhenFocusOut(FocusOutEvent evt)
        {
            fieldInfo.Parameters.Clear();
            string[] parameters = subView.parameterTextField.text.Split(',');
            if(parameters.Length == 1 && parameters[0].IsNullOrEmpty())
                return;
            
            for (var index = 0; index < parameters.Length; index++)
            {
                string parameter = parameters[index];
                bool isContainsThis = parameter.Contains("this");
                if (isContainsThis && index != 0) continue;
                
                string[] parameterFormat = parameter.Trim().Split(' ');
                bool isTypeNameFormat = parameterFormat.Length >= 2;

                if (isTypeNameFormat)
                {
                    string parameterType = isContainsThis ? $"this {parameterFormat[1]}" : parameterFormat[0];
                    string parameterName = isContainsThis ? parameterFormat[2] : parameterFormat[1];
                    string normalizedName = $"{char.ToUpper(parameterName[0])}{parameterName[1..]}";
                    fieldInfo.Parameters.Add((parameterType, normalizedName));
                    continue;
                }

                Debug.LogWarning($@"
[Diagram EditorWindow]
- {node.name} 
    Name your parameter, even if it's later");
                fieldInfo.Parameters.Add((parameter, ""));
                node.NodeModel.Status.IsDirty = true;
            }
        }
        private void BindAccessTypeRepeatButton()
        {
             subView.accessTypeRepeatButton.text = node.ModelModifier.GetNextCycleType<AccessType>();
             string currentAccessType = subView.accessTypeRepeatButton.text;
             // TODO :: support not yet
             if(currentAccessType.Contains("readonly", StringComparison.OrdinalIgnoreCase) ||
                currentAccessType.Contains("partial", StringComparison.OrdinalIgnoreCase) ||
                currentAccessType.Contains("sealed", StringComparison.OrdinalIgnoreCase))
             {
                 BindAccessTypeRepeatButton();
             }
             fieldInfo.AccessType = subView.accessTypeRepeatButton.text;
             node.NodeModel.Status.IsDirty = true;
        }
        
        private void ClearDeclarationTypeMethodAccessType()
        {
            fieldInfo.AccessType = fieldInfo.AccessType.Erase("abstract");
            fieldInfo.AccessType = fieldInfo.AccessType.Erase("virtual");
            fieldInfo.AccessType = fieldInfo.AccessType.Erase("static");
        }

        private bool IsPrivate()
        {
            return fieldInfo.AccessType.Equals("private", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAbstractAndVirtual()
        {
            return fieldInfo.DeclarationType == DeclarationType.Abstract &&
                   fieldInfo.DeclarationType == DeclarationType.Virtual;
        }

        private bool IsValidateAbstractClass()
        {
            return node.ModelModifier
                       .GetNodeViewTypeName<AccessType>()
                       .Contains("abstract", StringComparison.OrdinalIgnoreCase) &&
                   fieldInfo.DeclarationType == DeclarationType.Abstract;
        }
        private void SetNextMethodDeclarationType()
        {
            int maxLength = Enum.GetNames(typeof(DeclarationType)).Length;
            fieldInfo.DeclarationType = (DeclarationType)(((int)fieldInfo.DeclarationType + 1) % maxLength);
            subView.declarationTypeRepeatButton.text = fieldInfo.DeclarationType.ToString();

            if (!IsPrivate()) return;
            if (!IsAbstractAndVirtual()) return;
            if (IsValidateAbstractClass()) return;
            
            SetNextMethodDeclarationType();
        }
        private void BindDeclarationTypeRepeatButton()
        {
            ClearDeclarationTypeMethodAccessType();
            SetNextMethodDeclarationType();
            node.NodeModel.Status.IsDirty = true;
        }

        private void BindNormalNode()
        {
            subView.typeTextField.RegisterCallback<FocusOutEvent>(BindTypeTextField);
            subView.nameTextField.RegisterCallback<FocusOutEvent>(BindNameTextField);
            subView.parameterTextField.RegisterCallback<FocusOutEvent>(BindParameterTextFieldWhenFocusOut);
            subView.accessTypeRepeatButton.clicked += BindAccessTypeRepeatButton;
            subView.declarationTypeRepeatButton.clicked += BindDeclarationTypeRepeatButton;
        }

        private void BindEventNode()
        {
            subView.nameTextField.RegisterCallback<FocusOutEvent>(BindEventNodeNameTextField);
            subView.eventPredicateTextField.RegisterCallback<FocusOutEvent>(BindPredicateTextField);
        }

        public void Bind()
        {
             if (node is DiagramNormalNode)
                 BindNormalNode();
             if (node is DiagramEventNode)
                 BindEventNode();
        }
        public void BindRemoveButton(Action action)
        {
            if(subView.removeButton != null)
                subView.removeButton.clicked += action;
        }

        private void DisposeNormalNode()
        {
            subView.typeTextField?.UnregisterCallback<FocusOutEvent>(BindTypeTextField);
            subView.nameTextField?.UnregisterCallback<FocusOutEvent>(BindNameTextField);
            subView.parameterTextField?.UnregisterCallback<FocusOutEvent>(BindParameterTextFieldWhenFocusOut);
            
            if(subView.accessTypeRepeatButton != null)
                subView.accessTypeRepeatButton.clicked -= BindAccessTypeRepeatButton;
            if(subView.declarationTypeRepeatButton != null)
                subView.declarationTypeRepeatButton.clicked -= BindDeclarationTypeRepeatButton;
        }

        private void DisposeEventNode()
        {
            subView.nameTextField?.UnregisterCallback<FocusOutEvent>(BindEventNodeNameTextField);
            subView.eventPredicateTextField?.UnregisterCallback<FocusOutEvent>(BindPredicateTextField);
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
            ClearDeclarationTypeMethodAccessType();
            fieldInfo.DeclarationType = DeclarationType.Static;
            subView.declarationTypeRepeatButton.text = fieldInfo.DeclarationType.ToString();
        }
    }
}