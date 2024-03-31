using Share;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class BlackboardGroupView : VisualElement
    {
        public VisualElement container;
        public VisualElement headerContainer;
        public TextField titleField;
        public BlackboardRow groupListRow;
        public TextField groupListField;
        
        public BlackboardGroupView(string title, string groupList)
        {
            container = new VisualElement();
            headerContainer = new VisualElement();
            Label titleLabel = CreateLabel("Title");
            titleField = CreateTextField(title.IsNullOrEmptyThen("title..."));
            groupListField = CreateTextField(groupList.IsNullOrEmptyThen("group list..."));
            groupListRow = new BlackboardRow(new Label("Group List"), groupListField);
            
            headerContainer.Add(titleField);
            container.Add(titleLabel);
            container.CreateVerticalSpace(15);
            container.Add(headerContainer);
            container.CreateVerticalSpace(15);
            container.CreateHorizontalBar(5, Color.yellow);
            container.CreateVerticalSpace(15);
            container.Add(groupListRow);
            
            container.SetFlexDirection(FlexDirection.Column);
             
            titleLabel.SetFlexGrow(0);
            titleLabel.SetHeight(25);
            titleLabel.SetFontSize(24);
            titleLabel.SetFontStyle(FontStyle.Bold);
            
            titleField.SetFlexGrow(0);
            titleField.SetWidth(250);
            titleField.SetHeight(25);
            titleField.SetFontSize(12);
            titleField.SetBorderLeftWidth(15);
            
            groupListField.SetFlexGrow(0);
            groupListField.SetHeight(200);
            groupListField.SetWidth(200);
            groupListField.SetWhiteSpace(WhiteSpace.Normal);
            
            Add(container);
        }

        public void ClearValue()
        {
            titleField.value = "";
            groupListField.value = "";
        }
    }
}