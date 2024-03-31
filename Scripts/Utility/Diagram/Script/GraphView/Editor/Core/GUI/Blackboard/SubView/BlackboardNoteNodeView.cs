using System;
using System.Collections.Generic;
using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class BlackboardNoteNodeView : VisualElement
    {
        public VisualElement container;
        public VisualElement headerContainer;
        public TextField titleField;
        public Toggle useGlobalConfig;
        public Button openConfigButton;
        public DropdownField exportType;
        public Label exportListField;           //  i.e note - baseNote - baseNote ...
        public TextField exportExampleField;
        public Toggle exportRewriteToggle;
        public ConfluenceConfigView confluence;
        public SlackConfigView slack;
        public TextField exportPathField;
        public Button exportPathButton;
        public Button exportButton;
        public VisualElement filePathContainer;
        public VisualElement exportButtonContainer;
        public VisualElement exportSettingContainer;
        public Label TitleLabel;
        public Label exportExampleLabel;
        public VisualElement configContainer;
        public Label exportPathLabel;

        public BlackboardNoteNodeView(string exportList, string exportExample)
        {
            confluence = new ConfluenceConfigView();
            slack = new SlackConfigView();
            
            container = new VisualElement();
            headerContainer = new VisualElement();
            TitleLabel = CreateLabel("Note");
            useGlobalConfig = CreateToggle(false);
            openConfigButton = CreateButton("Open Config");
            titleField = CreateTextField("title...");
            exportType = CreateDropdownField("Export Type", new List<string>(Enum.GetNames(typeof(ExportFileType))), 0);
            exportListField = CreateLabel(exportList);
            exportExampleLabel = CreateLabel("Export Example");
            exportRewriteToggle = CreateToggle(false);
            exportExampleField = CreateTextField(exportExample);
            configContainer = new VisualElement();
            
            filePathContainer = new VisualElement();
            exportButtonContainer = new VisualElement();
            exportSettingContainer = new VisualElement();
            
            exportPathLabel = CreateLabel("Export Path : ");
            exportPathField = CreateTextField("C:/");
            exportPathButton = CreateButton("...");
            exportButton = CreateButton("Export");
            
            SetUpHierarchy();
            
            SetUpStyle();
            
            Add(container);
        }

        private void SetUpHierarchy()
        {
            headerContainer.Add(titleField);
            headerContainer.Add(useGlobalConfig);
            headerContainer.Add(openConfigButton);
            exportSettingContainer.Add(exportType);
            exportSettingContainer.Add(exportListField);
            
            filePathContainer.Add(exportPathLabel);
            filePathContainer.Add(exportPathField);
            filePathContainer.Add(exportPathButton);
            
            configContainer.Add(exportSettingContainer);
            configContainer.CreateVerticalSpace(10);
            configContainer.Add(confluence.mainContainer);
            configContainer.Add(slack.mainContainer);
            
            configContainer.Add(filePathContainer);
            exportButtonContainer.Add(exportButton);
            
            container.Add(TitleLabel);
            container.CreateVerticalSpace(10);
            container.Add(headerContainer);
            container.CreateVerticalSpace(15);
            container.CreateHorizontalBar(5, Color.yellow);
            container.CreateVerticalSpace(15);
            container.Add(configContainer);
            container.CreateVerticalSpace(10);
            container.Add(exportButtonContainer);
            container.CreateVerticalSpace(30);
            container.Add(exportExampleLabel);
            container.Add(exportRewriteToggle);
            container.Add(exportExampleField);
        }
        private void SetUpStyle()
        {
            container.SetFlexDirection(FlexDirection.Column);
            headerContainer.SetFlexDirection(FlexDirection.Row);
            configContainer.SetFlexDirection(FlexDirection.Column);
            filePathContainer.SetFlexDirection(FlexDirection.Row);
            exportSettingContainer.SetFlexDirection(FlexDirection.Row);
            exportButtonContainer.SetFlexDirection(FlexDirection.Column);
            
            TitleLabel.SetFlexGrow(0);
            TitleLabel.SetWidth(100);
            TitleLabel.SetHeight(25);
            TitleLabel.SetFontSize(24);
            TitleLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            titleField.SetFlexGrow(0);
            titleField.SetWidth(150);
            titleField.SetHeight(25);
            titleField.SetFontSize(12);
            titleField.SetTextAlign(TextAnchor.MiddleLeft);
            
            useGlobalConfig.label = "Use Global Config";
            useGlobalConfig.SetFlexGrow(0);
            useGlobalConfig.SetFlexShrink(1);
            useGlobalConfig.SetWidth(150);
            useGlobalConfig.SetHeight(25);
            useGlobalConfig.SetTextAlign(TextAnchor.MiddleLeft);
            useGlobalConfig.SetDisplay(false);
            
            openConfigButton.SetFlexGrow(0);
            openConfigButton.SetWidth(100);
            openConfigButton.SetHeight(25);
            openConfigButton.SetDisplay(false);

            exportType.SetFlexGrow(0);
            exportType.SetWidth(100);
            exportType.SetFlexGrow(1);
            exportType.SetHeight(20);
            
            exportListField.SetFlexGrow(1);
            exportListField.SetHeight(25);
            exportListField.SetFontSize(12);
            
            exportExampleLabel.SetFlexGrow(0);
            exportExampleLabel.SetWidth(100);
            exportExampleLabel.SetHeight(25);
            exportExampleLabel.SetFontSize(12);
            exportExampleLabel.SetTextAlign(TextAnchor.MiddleLeft);
            exportExampleLabel.SetFontStyle(FontStyle.Bold);
            
            exportRewriteToggle.SetFlexGrow(0);
            exportRewriteToggle.SetHeight(20);
            exportRewriteToggle.label = "Customize";
                
            exportExampleField.SetFlexGrow(0);
            exportExampleField.SetHeight(200);
            exportExampleField.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
            exportExampleField.SetWhiteSpace(WhiteSpace.Normal);
            exportExampleField.multiline = true;
            exportExampleField.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
            
            exportExampleField.SetBackgroundColor(new Color(0.12f, 0.12f, 0.12f, 1));
            exportRewriteToggle.SetBackgroundColor(new Color(0.12f, 0.12f, 0.12f, 1));
            
            exportPathLabel.SetFlexGrow(0);
            exportPathLabel.SetWidth(120);
            exportPathLabel.SetHeight(25);
            exportPathLabel.SetFontSize(12);
            exportPathLabel.SetTextAlign(TextAnchor.MiddleLeft);
            
            exportPathField.SetFlexGrow(0);
            exportPathField.SetWidth(200);
            exportPathField.SetHeight(25);
            
            exportButton.SetFlexGrow(0);
            exportButton.SetWidth(70);
            exportButton.SetHeight(20);
            
            exportPathButton.SetFlexGrow(0);
            exportPathButton.SetWidth(30);
            exportPathButton.SetHeight(20);
            
            exportButtonContainer.SetAlignSelf(Align.FlexEnd);
            exportButtonContainer.style.alignItems = Align.FlexEnd;
            exportButtonContainer.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
            exportButtonContainer.SetBorderRightWidth(10);
            
        }
        public void ClearValue()
        {
            titleField.value = string.Empty;
            exportType.value = "Not_Used";
            exportListField.text = string.Empty;
            exportExampleField.value = string.Empty;
            exportPathField.value = "C:/";
        }
    }
}