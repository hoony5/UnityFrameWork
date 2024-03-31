using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class SlackConfigView
    {
        public VisualElement mainContainer;
        
        public VisualElement slackLinkContainer;
        private Label slackLinkLabel;
        public TextField slackLinkField;
        public Button slackLinkButton;
        
        public VisualElement slackChannelContainer;
        private Label slackChannelLabel;
        public TextField slackChannelField;
        
        public VisualElement slackUserNameContainer;
        private Label slackUserNameLabel;
        public TextField slackUserNameField;
        
        public VisualElement slackIconURLContainer;
        private Label slackIconURLLabel;
        public TextField slackIconURLField;
        public Button slackIconURLButton;
        
        public VisualElement slackContentContainer;
        public TextField slackEpicField;
        public TextField slackStoryField;
        public TextField slackIssueField;
        public TextField slackMessageField;
        
        public SlackConfigView()
        {
            mainContainer = new VisualElement();
            
            slackLinkContainer = new VisualElement();
            slackLinkLabel = CreateLabel("Slack WebHook URL : ");
            slackLinkField = CreateTextField("https://slack.");
            slackLinkButton = CreateButton("...");
            
            slackChannelContainer = new VisualElement();
            slackChannelLabel = CreateLabel("Channel : ");
            slackChannelField = CreateTextField("i.e #receiveChannel or @receiveUsername");
            
            slackUserNameContainer = new VisualElement();
            slackUserNameLabel = CreateLabel("User Name : ");
            slackUserNameField = CreateTextField("i.e bot");
            
            slackIconURLContainer = new VisualElement();
            slackIconURLLabel = CreateLabel("Icon URL(or shortCodes) : ");
            slackIconURLField = CreateTextField("::ghost::");
            slackIconURLButton = CreateButton("more...");
            
            slackContentContainer = new VisualElement();
            slackEpicField = CreateTextField("Theme");
            slackEpicField.label = "Epic";
            slackStoryField = CreateTextField("Story");
            slackStoryField.label = "Story";
            slackIssueField = CreateTextField("Bug / Issue");
            slackIssueField.label = "Issue";
            slackMessageField = CreateTextField("Message");
            slackMessageField.label = "Message";
            
            SetUpHierarchy();
            
            SetUpStyle();
        }

        private void SetUpHierarchy()
        {
            mainContainer.Add(slackLinkContainer);
            mainContainer.Add(slackChannelContainer);
            mainContainer.Add(slackUserNameContainer);
            mainContainer.Add(slackIconURLContainer);
            mainContainer.CreateVerticalSpace(10);
            mainContainer.Add(slackContentContainer);
            
            slackLinkContainer.Add(slackLinkLabel);
            slackLinkContainer.Add(slackLinkField);
            slackLinkContainer.Add(slackLinkButton);
            
            slackChannelContainer.Add(slackChannelLabel);
            slackChannelContainer.Add(slackChannelField);
            
            slackUserNameContainer.Add(slackUserNameLabel);
            slackUserNameContainer.Add(slackUserNameField);
            
            slackIconURLContainer.Add(slackIconURLLabel);
            slackIconURLContainer.Add(slackIconURLField);
            slackIconURLContainer.Add(slackIconURLButton);
            
            slackContentContainer.Add(slackEpicField);
            slackContentContainer.Add(slackStoryField);
            slackContentContainer.Add(slackIssueField);
            slackContentContainer.Add(slackMessageField);
        }

        private void SetUpStyle()
        {
            mainContainer.SetFlexDirection(FlexDirection.Column);
            
            slackLinkContainer.SetFlexDirection(FlexDirection.Row);
            slackLinkContainer.SetFlexShrink(1);
            slackChannelContainer.SetFlexDirection(FlexDirection.Row);
            slackChannelContainer.SetFlexShrink(1);
            slackUserNameContainer.SetFlexDirection(FlexDirection.Row);
            slackUserNameContainer.SetFlexShrink(1);
            slackIconURLContainer.SetFlexDirection(FlexDirection.Row);
            slackIconURLContainer.SetFlexShrink(1);
            slackContentContainer.SetFlexDirection(FlexDirection.Column);
            slackContentContainer.SetFlexShrink(1);
            slackContentContainer.SetFlexGrow(1);
            
            slackLinkLabel.SetFlexGrow(0);
            slackLinkLabel.SetWidth(150);
            slackLinkLabel.SetHeight(25);
            slackLinkLabel.SetFontSize(12);
            slackLinkLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            slackLinkField.SetFlexGrow(0);
            slackLinkField.SetWidth(300);
            slackLinkField.SetHeight(25);
            
            slackLinkButton.SetFlexGrow(0);
            slackLinkButton.SetWidth(30);
            slackLinkButton.SetHeight(25);
            
            slackChannelLabel.SetFlexGrow(0);
            slackChannelLabel.SetWidth(150);
            slackChannelLabel.SetHeight(25);
            slackChannelLabel.SetFontSize(12);
            slackChannelLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            slackChannelField.SetFlexGrow(0);
            slackChannelField.SetWidth(300);
            slackChannelField.SetHeight(25);
            
            slackUserNameLabel.SetFlexGrow(0);
            slackUserNameLabel.SetWidth(150);
            slackUserNameLabel.SetHeight(25);
            slackUserNameLabel.SetFontSize(12);
            slackUserNameLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            slackUserNameField.SetFlexGrow(0);
            slackUserNameField.SetWidth(150);
            slackUserNameField.SetHeight(25);
            
            slackIconURLLabel.SetFlexGrow(0);
            slackIconURLLabel.SetWidth(150);
            slackIconURLLabel.SetHeight(25);
            slackIconURLLabel.SetFontSize(12);
            slackIconURLLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            slackIconURLField.SetFlexGrow(0);
            slackIconURLField.SetWidth(300);
            slackIconURLField.SetHeight(25);
            
            slackIconURLButton.SetFlexGrow(0);
            slackIconURLButton.SetWidth(60);
            slackIconURLButton.SetHeight(25);
            
            slackEpicField.SetFlexGrow(0);
            slackEpicField.SetWidth(400);
            slackEpicField.SetHeight(25);
            
            slackStoryField.SetFlexGrow(0);
            slackStoryField.SetWidth(400);
            slackStoryField.SetHeight(25);
            
            slackIssueField.SetFlexGrow(0);
            slackIssueField.SetWidth(400);
            slackIssueField.SetHeight(100);
            slackIssueField.multiline = true;
            
            slackMessageField.SetFlexGrow(0);
            slackMessageField.SetWidth(400);
            slackMessageField.SetHeight(100);
            slackMessageField.multiline = true;
        }

        public void Clear()
        {
            slackLinkField.value = "https://slack.";
        }
    }

}