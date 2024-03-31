using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class MethodSectionView : SectionView
    {
        // List
        public Button addButton;    
        public MethodSectionView(string title)
        {
            CreateFoldout(title);
            
            addButton = CreateButton("+");
            
            container.Add(addButton);
            foldout.Add(container);
            
            foldout.SetBorderWidth(6);
            container.SetFlexDirection(FlexDirection.Column);
        }

        public MethodSectionSubView CreateNormalNodeSubview(
            string accessTypeButtonLabel,
            string declarationTypeButtonLabel,
            string typeFieldLabel,
            string nameFieldLabel,
            string parameterFieldLabel)
        {
            addButton = CreateButton("+");
            MethodSectionSubView subSubView = new MethodSectionSubView();
            subSubView.CreateNormalNodeSubview(
                accessTypeButtonLabel,
                declarationTypeButtonLabel,
                typeFieldLabel,
                nameFieldLabel,
                parameterFieldLabel);
            
            container.Add(subSubView.group);
            return subSubView;
        }

        public MethodSectionSubView CreateEventNodeView(string nameFieldLabel, string predicateFieldLabel)
        {
            addButton = CreateButton("+");
            MethodSectionSubView subSubView = new MethodSectionSubView();
            
            subSubView.CreateEventNodeView(nameFieldLabel, predicateFieldLabel);
            
            container.Add(subSubView.group);
            return subSubView;
        }
    }
}