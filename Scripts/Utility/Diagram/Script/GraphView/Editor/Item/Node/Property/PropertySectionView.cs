using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class PropertySectionView : SectionView
    {
        public Button addButton;
        public PropertySectionView(IDiagramNode node, string title)
        { 
            CreateFoldout(title);
            CreatePlain(title);
            
            if(node is DiagramNormalNode)
                addButton = CreateButton("+");
            
            container.Add(addButton);
            foldout.Add(container);
            
            foldout.SetBorderWidth(6);
            container.SetFlexDirection(FlexDirection.Column);
            foldout.tooltip = "Every Property format is same. (ex : [field:SerializeField, JsonProperty(nameof(Name))] public Type Name {get; private/protected set;})";
        }

        public PropertySectionSubView CreateNormalNodeSubview(
            string declarationTypeButtonLabel,
            string typeFieldLabel,
            string nameFieldLabel)
        {
            addButton = CreateButton("+");
            
            PropertySectionSubView sectionSubView = new PropertySectionSubView();
            sectionSubView.CreateNormalNodeSubview(declarationTypeButtonLabel, typeFieldLabel, nameFieldLabel);
            
            container.Add(sectionSubView.group);
            return sectionSubView;
        }
        public PropertySectionSubView CreateEventNodeSubview(string typeFieldLabel)
        {
            PropertySectionSubView sectionSubView = new PropertySectionSubView();
            sectionSubView.CreateEventNodeSubview(typeFieldLabel);
            container.Add(sectionSubView.group);
            plain.Add(container);
            
            plain.SetBorderLeftWidth(6);
            plain.SetBorderRightWidth(6);
            
            return sectionSubView;
        }
    }
}