using System.Collections.Generic;
using UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;
using Writer.AssetGenerator.UIElement.Usecase;

namespace GPT
{
    public class GPTBetaView : UxmlItem<VisualElement>
    {
        // config controls
        // inherited from Legacy View

        /*      new added View Element
         *      - Image Dialogue View
         *      - Prompt Strategy
         *      - Hierarchical View ( Name of the View )
         *      - Image View
         *      - Manually Retry View
         *      - Prompt View : P class
         */

        public readonly UxmlItem<VisualElement> headerContainer;
        public readonly UxmlItem<VisualElement> configContainer;
        public readonly UxmlItem<VisualElement> responseContainer;
        public readonly UxmlItem<VisualElement> promptContainer;
        public readonly UxmlItem<VisualElement> attachmentContainer;
        public readonly UxmlItem<VisualElement> attachmentButtonsContainer;
        public readonly UxmlItem<VisualElement> designContainer;
        
        public readonly LineElement headerLable;
        
        public readonly PathLineElement apiKey;
        public readonly DropdownLineElement model;
        public readonly SliderLineElement temperature;
        public readonly SliderLineElement topP;
        public readonly SliderLineElement maxTokens;
        public readonly SliderLineElement presencePenalty;
        public readonly SliderLineElement frequencyPenalty;

        public readonly UxmlItem<Label> responseLabel;
        public readonly UxmlItem<Button> selectionCopyButton;
        public readonly UxmlItem<Button> dialogueClearButton;
        public readonly UxmlItem<Button> dialogueSaveButton;
        public readonly UxmlItem<Button> dialogueLoadButton;
        public readonly UxmlItem<Button> dialogueSummaryButton;
        public readonly UxmlItem<Button> designDialogueButton;
        
        public readonly UxmlItem<TextField> designTitleTextField;
        public readonly UxmlItem<TextField> designTimingTextField;
        public readonly UxmlItem<TextField> designLevelTextField;
        public readonly UxmlItem<TextField> designSubjectTextField;
        public readonly UxmlItem<TextField> designGoalTextField;
        public readonly UxmlItem<TextField> designStyleTextField;
        
        public readonly UxmlItem<ScrollView> responseView;
        
        public readonly UxmlItem<Button> fileAttachmentButton;
        public readonly UxmlItem<Button> nodeAttachmentButton;
        
        public readonly UxmlItem<ScrollView> attachmentView;
        public readonly UxmlItem<TreeView> nodeTreeView;
        public readonly UxmlItem<TextField> promptTextField;
        public readonly UxmlItem<Button> submitButton;

        public GPTBetaView()
        {
            apiKey = new PathLineElement("API Key", true);
            model = new DropdownLineElement("Model",
                new List<string> { 
                "gpt-4-0125-preview",
                "gpt-4-turbo-preview",
                "gpt-3.5-turbo-0125",
                "gpt-3.5-turbo" });
            temperature = new SliderLineElement("Temperature", 0.0f, 1.0f, 0.5f);
            topP = new SliderLineElement("Top P", 0.0f, 1.0f, 1.0f);
            maxTokens = new SliderLineElement("Max Tokens", 1, 4096, 1024);
            presencePenalty = new SliderLineElement("Presence Penalty", -2.0f, 2.0f, 0.0f);
            frequencyPenalty = new SliderLineElement("Frequency Penalty", -2.0f, 2.0f, 0.0f);
            headerLable = new LineElement("GPT Beta");
            
            headerLable.label
                .StyleFontSize(20)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleFontStyle(FontStyle.Bold)
                .StyleJustifyContent(Justify.FlexStart);
            
            responseLabel = new Label()
                .SetUp()
                .SetName("response-label")
                .SetText("Response")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleFontSize(12)
                .StyleFontStyle(FontStyle.Bold)
                .StyleMarginBottom(5);
            
            selectionCopyButton = new Button()
                .SetUp()
                .SetName("selection-copy-button")
                .SetText("Copy")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            dialogueClearButton = new Button()
                .SetUp()
                .SetName("dialogue-clear-button")
                .SetText("Clear")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            dialogueSaveButton = new Button()
                .SetUp()
                .SetName("dialogue-save-button")
                .SetText("Save")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            dialogueLoadButton = new Button()
                .SetUp()
                .SetName("dialogue-load-button")
                .SetText("Load")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            dialogueSummaryButton = new Button()
                .SetUp()
                .SetName("dialogue-load-button")
                .SetText("Summary")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            designDialogueButton = new Button()
                .SetUp()
                .SetName("design-dialogue-button")
                .SetText("Design")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            
            designTitleTextField = new TextField()
                .SetUp()
                .SetName("design-title-text-field")
                .SetValue("Title")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designTitleTextField.element.label = "Title";
            
            designTimingTextField = new TextField()
                .SetUp()
                .SetName("design-timing-text-field")
                .SetValue("Timing")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designTimingTextField.element.label = "Timing";
            
            designLevelTextField = new TextField()
                .SetUp()
                .SetName("design-level-text-field")
                .SetValue("Level")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designLevelTextField.element.label = "Level";
            
            designSubjectTextField = new TextField()
                .SetUp()
                .SetName("design-subject-text-field")
                .SetValue("Subject")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designSubjectTextField.element.label = "Subject";
            
            designGoalTextField = new TextField()
                .SetUp()
                .SetName("design-goal-text-field")
                .SetValue("Goal")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designGoalTextField.element.label = "Goal";
            
            designStyleTextField = new TextField()
                .SetUp()
                .SetName("design-style-text-field")
                .SetValue("Style")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(95, LengthUnit.Percent)
                .StyleHeight(25);
            designStyleTextField.element.label = "Style";
            
            responseView = new ScrollView()
                .SetUp()
                .SetShowHorizontalVisibility(ScrollerVisibility.Hidden)
                .SetShowVerticalVisibility(ScrollerVisibility.AlwaysVisible)
                .StyleBorderColor(Color.black)
                .StyleBorderWidth(0.1f)
                .SetName("response-view")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent);

            fileAttachmentButton = new Button()
                .SetUp()
                .SetName("file-attachment-button")
                .SetText("File")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleTextAlign(TextAnchor.MiddleCenter)
                .StyleWidth(100, LengthUnit.Percent);

            nodeAttachmentButton = new Button()
                .SetUp()
                .SetName("node-attachment-button")
                .SetText("Node")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleTextAlign(TextAnchor.MiddleCenter)
                .StyleWidth(100, LengthUnit.Percent);

            nodeTreeView = new TreeView()
                .SetUp() 
                .StyleBorderColor(Color.black)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1))
                .StyleBorderWidth(0.1f)
                .SetName("nodeTree-view")
                .StyleDisplay(DisplayStyle.None)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);

            attachmentView = new ScrollView()
                .SetUp()
                .SetShowHorizontalVisibility(ScrollerVisibility.Hidden)
                .SetShowVerticalVisibility(ScrollerVisibility.AlwaysVisible)
                .StyleBorderColor(Color.black)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1))
                .StyleBorderWidth(0.1f)
                .SetName("attachment-view")
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            promptTextField = new TextField()
                .SetUp()
                .SetMultiline(true)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .SetValue("Enter your prompt here.")
                .SetName("prompt-text-field")
                .StyleWhiteSpace(WhiteSpace.Normal)
                .StyleWidth(100, LengthUnit.Percent);
            
            submitButton = new Button()
                .SetUp()
                .SetName("submit-button")
                .SetText("Submit")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(40);

            this
                .StyleFlexDirection(FlexDirection.Column)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1));

            headerContainer = new VisualElement()
                .SetUp()
                .SetName("header-container")
                .StyleFlexDirection(FlexDirection.Column)
                .StyleFlexGrow(0)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(100)
                .StyleFlexShrink(1);
            
            configContainer = new VisualElement()
                .SetUp()
                .SetName("config-container")
                .StyleFlexDirection(FlexDirection.Column)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBorderColor(Color.black)
                .StyleBorderWidth(0.1f)
                .StyleWidth(500);
            
            designContainer = new VisualElement()
                .SetUp()
                .SetName("design-container")
                .StyleFlexDirection(FlexDirection.Column)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(60, LengthUnit.Percent)
                .StyleDisplay(DisplayStyle.None);
            
            responseContainer = new VisualElement()
                .SetUp()
                .SetName("response-container")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(60, LengthUnit.Percent);
            
            attachmentContainer = new VisualElement()
                .SetUp()
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1))
                .StyleFlexDirection(FlexDirection.Column)
                .StyleWidth(58, LengthUnit.Percent);

            attachmentButtonsContainer = new VisualElement()
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBorderWidth(0)
                .StyleFlexDirection(FlexDirection.Row)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1))
                .StyleHeight(25);
            
            promptContainer = new VisualElement()
                .SetUp()
                .SetName("prompt-container")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleBackgroundColor(new Color(0.16f, 0.16f, 0.16f, 1))
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(20, LengthUnit.Percent);
            
            headerContainer.Add(
                headerLable,
                UxmlGUI.CreateSpace(100, LengthUnit.Percent, 10, LengthUnit.Pixel, Color.yellow));
            
            designContainer.Add(
                UxmlGUI.CreateSpace(100, LengthUnit.Percent, 10, LengthUnit.Pixel, Color.clear),
                designTitleTextField,
                designTimingTextField,
                designLevelTextField,
                designSubjectTextField,
                designGoalTextField,
                designStyleTextField);
            
            configContainer.Add(
                apiKey,
                model,
                temperature,
                topP,
                maxTokens,
                presencePenalty,
                frequencyPenalty,
                responseLabel,
                selectionCopyButton,
                dialogueClearButton,
                dialogueSaveButton,
                dialogueLoadButton,
                dialogueSummaryButton,
                designDialogueButton,
                designContainer);
            
            responseContainer.Add(
                configContainer,
                responseView);
            
            attachmentButtonsContainer.Add(
                fileAttachmentButton,
                nodeAttachmentButton);
            
            attachmentContainer.Add(
                attachmentButtonsContainer,
                attachmentView,
                nodeTreeView);
            
            promptContainer.Add(
                attachmentContainer,
                promptTextField);

            int space = 20;
            this.Add(
                headerContainer,
                UxmlGUI.CreateSpace(100, LengthUnit.Percent, space, LengthUnit.Pixel, Color.clear),
                responseContainer,
                UxmlGUI.CreateSpace(100, LengthUnit.Percent, space, LengthUnit.Pixel, Color.clear),
                promptContainer,
                submitButton);

        }
        
        public DialogueItem AddDialogueItem(string title, string content, int index)
        {
            DialogueItem item = new DialogueItem(title, content);
            responseView.element.contentContainer.Add(item.element);
            responseView.element.contentContainer.Add(UxmlGUI.CreateSpace(100, LengthUnit.Percent, 10f, LengthUnit.Pixel, Color.clear).element);
            item.element.userData = index;
            return item;
        }


        public IconLineElement CreateAttachmentLine(string iconPath, string filePath)
        {
            IconLineElement item = new IconLineElement(iconPath, filePath);
            attachmentView.element.contentContainer.Add(item.element);
            return item;
        }
    }
}