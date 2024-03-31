using Share;
using static Share.VisualElementEx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardEventNodeView : VisualElement
    {
        public VisualElement container;
        public VisualElement headerContainer;
        public Label messageTypeLabel;          
        public Label messageTypeField;          
        public BlackboardRow publishRow;
        public TextField publishListField;          // publish list
        public BlackboardRow subscribeRow;
        public TextField subscribeListField;        // subscribe list
        
        public BlackboardEventNodeView(string messageType, string publishList, string subscribeList)
        {
            container = new VisualElement();
            headerContainer = new VisualElement();
            messageTypeLabel = CreateLabel("Message Type");
            messageTypeField = CreateLabel(messageType.IsNullOrEmptyThen("message type..."));
            publishListField = CreateTextField(publishList.IsNullOrEmptyThen("publish callbacks..."));
            subscribeListField = CreateTextField(subscribeList.IsNullOrEmptyThen("Subscribe callbacks..."));
            publishRow = new BlackboardRow(new Label("Publisher list"), publishListField);
            subscribeRow = new BlackboardRow(new Label("Subscriber List"), subscribeListField); 
            
            headerContainer.Add(messageTypeLabel);
            container.Add(headerContainer);
            container.Add(messageTypeField);
            container.CreateVerticalSpace(15);
            container.CreateHorizontalBar(5, Color.yellow);
            container.CreateVerticalSpace(15);
            container.Add(publishRow);
            container.CreateVerticalSpace(30);
            container.Add(subscribeRow);
            
            container.SetFlexDirection(FlexDirection.Column);
            headerContainer.SetFlexDirection(FlexDirection.Row);
            publishRow.SetFlexDirection(FlexDirection.Column);
            subscribeRow.SetFlexDirection(FlexDirection.Column);
            
            messageTypeLabel.SetFlexGrow(1);
            messageTypeLabel.SetHeight(25);
            messageTypeLabel.SetFontSize(20);
            messageTypeLabel.SetFontStyle(FontStyle.Bold);
            messageTypeLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            messageTypeField.SetFlexGrow(1);
            messageTypeField.SetHeight(25);
            messageTypeField.SetFontSize(12);
            messageTypeField.SetFontStyle(FontStyle.Bold);
            messageTypeField.SetTextAlign(TextAnchor.MiddleLeft);
            
            publishListField.SetFlexGrow(0);
            publishListField.SetHeight(250);
            publishListField.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
            publishListField.SetWhiteSpace(WhiteSpace.Normal);
            publishListField.multiline = true;
            publishListField.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
             
            subscribeListField.SetFlexGrow(0);
            subscribeListField.SetHeight(250);
            subscribeListField.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
            subscribeListField.SetWhiteSpace(WhiteSpace.Normal);
            subscribeListField.multiline = true;
            subscribeListField.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
            
            Add(container);
            
            publishRow.expanded = true;
            subscribeRow.expanded = true;
        }
        public void ClearValue()
        {
            messageTypeField.text = "";
            publishListField.value = "";
            subscribeListField.value = "";
        }
    }
}