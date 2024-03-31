using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    // TODO :: move to css and uxml
    public class ConfluenceConfigView
    {
        public VisualElement mainContainer;
        
        public VisualElement baseUrlContainer;
        public Label confluenceLinkLabel;
        public Button confluenceLinkButton;
        public TextField confluenceLinkField;
        
        public VisualElement authTokenContainer;
        public Label confluenceBaseInfoLabel;
        public Button confluenceAPITokenLinkButton;
        public TextField confluenceAPITokenField;
        public TextField confluenceUserNameField;
        
        public VisualElement spaceKeyContainer;
        public Label confluenceSpaceKeyLinkLabel;
        public Button confluenceSpaceKeyLinkButton;
        public TextField confluenceSpaceKeyField;
        
        public VisualElement confluencePageTitleContainer;
        public Label confluencePageTitleLabel;
        public TextField confluencePageTitleField;
        public Button confluencePageIDLoadButton;
        public Button confluenceOpenPageButton;
        
        public VisualElement confluenceCustomTitleContainer;
        public Label confluenceCurrentTitleLabel;
        public Toggle confluenceCurrentPageTitleToggle;
        
        public ConfluenceConfigView()
        {
            baseUrlContainer = new VisualElement();
            confluenceLinkLabel = CreateLabel("Confluence Link : ");
            confluenceLinkField = CreateTextField("https://confluence.");
            confluenceLinkButton = CreateButton("...");
            authTokenContainer = new VisualElement();
            confluenceBaseInfoLabel = CreateLabel("User Name / API Token : ");
            confluenceUserNameField = CreateTextField("your-domain-name");
            confluenceAPITokenField = CreateTextField("your-api-token");
            confluenceAPITokenLinkButton = CreateButton("...");

            spaceKeyContainer = new VisualElement();
            confluenceSpaceKeyLinkLabel = CreateLabel("Space Key : ");
            confluenceSpaceKeyField = CreateTextField("");
            confluenceSpaceKeyLinkButton = CreateButton("...");

            confluencePageTitleContainer = new VisualElement();
            confluencePageTitleLabel = CreateLabel("Page Title : ");
            confluencePageTitleField = CreateTextField("");
            confluencePageIDLoadButton = CreateButton("Test Connection");
            confluenceOpenPageButton = CreateButton("Open the page");
            
            confluenceCustomTitleContainer = new VisualElement();
            confluenceCurrentTitleLabel = CreateLabel("Page title is current node title ? ");
            confluenceCurrentPageTitleToggle = CreateToggle(true);
            //VisualElement confluencePageIDContainer = new VisualElement();
            //Label confluencePageIDLabel = CreateLabel("Page ID (ReadOnly) : ");
            //confluencePageIDField = CreateTextField("");
            
            SetUpHierarchy();
            
            SetUpStyle();
        }

        private void SetUpHierarchy()
        {
            mainContainer = new VisualElement();
            mainContainer.Add(baseUrlContainer);
            baseUrlContainer.Add(confluenceLinkLabel);
            baseUrlContainer.Add(confluenceLinkField);
            baseUrlContainer.Add(confluenceLinkButton);
            
            mainContainer.Add(authTokenContainer);
            authTokenContainer.Add(confluenceBaseInfoLabel);
            authTokenContainer.Add(confluenceUserNameField);
            authTokenContainer.Add(confluenceAPITokenField);
            authTokenContainer.Add(confluenceAPITokenLinkButton);
            
            mainContainer.Add(spaceKeyContainer);
            spaceKeyContainer.Add(confluenceSpaceKeyLinkLabel);
            spaceKeyContainer.Add(confluenceSpaceKeyField);
            spaceKeyContainer.Add(confluenceSpaceKeyLinkButton);
            
            mainContainer.Add(confluencePageTitleContainer);
            confluencePageTitleContainer.Add(confluencePageTitleLabel);
            confluencePageTitleContainer.Add(confluencePageTitleField);
            confluencePageTitleContainer.Add(confluencePageIDLoadButton);
            confluencePageTitleContainer.Add(confluenceOpenPageButton);
            
            mainContainer.Add(confluenceCustomTitleContainer);
            confluenceCustomTitleContainer.Add(confluenceCurrentTitleLabel);
            confluenceCustomTitleContainer.Add(confluenceCurrentPageTitleToggle);
        }

        private void SetUpStyle()
        {
            mainContainer.SetFlexDirection(FlexDirection.Column);
            
            baseUrlContainer.SetFlexDirection(FlexDirection.Row);
            authTokenContainer.SetFlexDirection(FlexDirection.Row);
            spaceKeyContainer.SetFlexDirection(FlexDirection.Row);
            confluenceCustomTitleContainer.SetFlexDirection(FlexDirection.Row);
            confluencePageTitleContainer.SetFlexDirection(FlexDirection.Row);
            
            baseUrlContainer.SetFlexShrink(1);
            authTokenContainer.SetFlexShrink(1);
            spaceKeyContainer.SetFlexShrink(1);
            confluenceCustomTitleContainer.SetFlexShrink(1);
            confluencePageTitleContainer.SetFlexShrink(1);
            
            confluenceLinkLabel.SetFlexGrow(0);
            confluenceLinkLabel.SetWidth(150);
            confluenceLinkLabel.SetHeight(25);
            confluenceLinkLabel.SetFontSize(12);
            confluenceLinkLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            confluenceLinkField.SetFlexGrow(0);
            confluenceLinkField.SetWidth(200);
            confluenceLinkField.SetHeight(25);
            
            confluenceLinkButton.SetFlexGrow(0);
            confluenceLinkButton.SetWidth(30);
            confluenceLinkButton.SetHeight(25);
            
            confluenceBaseInfoLabel.SetFlexGrow(0);
            confluenceBaseInfoLabel.SetWidth(150);
            confluenceBaseInfoLabel.SetHeight(25);
            confluenceBaseInfoLabel.SetFontSize(12);
            confluenceBaseInfoLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            confluenceUserNameField.SetFlexGrow(0);
            confluenceUserNameField.SetWidth(150);
            confluenceUserNameField.SetHeight(25);
            
            confluenceAPITokenField.SetFlexGrow(0);
            confluenceAPITokenField.SetWidth(150);
            confluenceAPITokenField.SetHeight(25);
            confluenceAPITokenField.textEdition.isPassword = true;
            
            confluenceAPITokenLinkButton.SetFlexGrow(0);
            confluenceAPITokenLinkButton.SetWidth(30);
            confluenceAPITokenLinkButton.SetHeight(25);
            
            confluenceSpaceKeyLinkLabel.SetFlexGrow(0);
            confluenceSpaceKeyLinkLabel.SetWidth(120);
            confluenceSpaceKeyLinkLabel.SetHeight(25);
            confluenceSpaceKeyLinkLabel.SetFontSize(12);
            confluenceSpaceKeyLinkLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            confluencePageTitleLabel.SetFlexGrow(0);
            confluencePageTitleLabel.SetWidth(120);
            confluencePageTitleLabel.SetHeight(25);
            confluencePageTitleLabel.SetFontSize(12);
            confluencePageTitleLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            confluencePageTitleField.SetFlexGrow(0);
            confluencePageTitleField.SetWidth(150);
            confluencePageTitleField.SetHeight(25);
            
            confluenceCustomTitleContainer.SetBackgroundColor(new Color(0.12f, 0.12f, 0.12f, 1));
            confluenceCurrentTitleLabel.SetFlexGrow(0);
            confluenceCurrentTitleLabel.SetWidth(200);
            confluenceCurrentTitleLabel.SetHeight(25);
            confluenceCurrentTitleLabel.SetFontSize(12);
            confluenceCurrentTitleLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            confluenceCurrentPageTitleToggle.SetFlexGrow(0);
            confluenceCurrentPageTitleToggle.SetWidth(150);
            confluenceCurrentPageTitleToggle.SetHeight(25);
            
            confluencePageIDLoadButton.SetFlexGrow(0);
            confluencePageIDLoadButton.SetWidth(120);
            confluencePageIDLoadButton.SetHeight(25);
            
            confluenceOpenPageButton.SetFlexGrow(0);
            confluenceOpenPageButton.SetWidth(120);
            confluenceOpenPageButton.SetHeight(25);
            
            confluenceSpaceKeyField.SetFlexGrow(0);
            confluenceSpaceKeyField.SetWidth(200);
            confluenceSpaceKeyField.SetHeight(25);
            
            confluenceSpaceKeyLinkButton.SetFlexGrow(0);
            confluenceSpaceKeyLinkButton.SetWidth(30);
            confluenceSpaceKeyLinkButton.SetHeight(25);
        }

        public void Clear()
        {
            confluenceLinkField.value = "https://confluence.";
            confluenceUserNameField.value = string.Empty;
            confluenceAPITokenField.value = string.Empty;
            confluenceSpaceKeyField.value = string.Empty;
        }
    }

}