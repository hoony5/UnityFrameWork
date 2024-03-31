using System;
using System.Threading;
using System.Threading.Tasks;
using Diagram.Config;
using GPT;
using Share;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Diagram
{
    public class BlackboardGPT : Blackboard, IDisposable
    {/*
        public readonly GPTView view;
        private GPTBinder binder;
        */
        
        public readonly DiagramGUI gui;
        public readonly GPTBetaView betaView;
        public readonly GPTBetaViewModel viewModel;
        
        public readonly DiagramConfig config;
        public readonly NodeAnalyzer analyzer;
        
        public IVisualElementScheduledItem responseUpdater;
        public IVisualElementScheduledItem typingTasker;
        
        public BlackboardGPT(DiagramGUI gui, DiagramConfig config)
        {
            this.gui = gui;
            this.config = config;
            betaView = new GPTBetaView();
            viewModel = new GPTBetaViewModel(this, betaView, config);
            analyzer = new NodeAnalyzer(viewModel);

            if (config == null)
            {
                Debug.LogError("Config is null");
            }
            if (config.gpt == null)
            {
                Debug.LogError("GPT config is null");
            }
            
            if (config.gpt.promptContent == null)
            {
                Debug.LogError("GPT prompt config is null");
            }
            
            if (config.confluence == null)
            {
                Debug.LogError("Confluence config is null");
            }
            
            if(config.slack == null)
            {
                Debug.LogError("Slack config is null");
            }
            
            Add(betaView.element);
            
            this.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
            this.style.width = new StyleLength(new Length(60, LengthUnit.Percent));
            
            SetUp();
            Load();
            viewModel.Bind();
        }

        public void SetUp()
        {
            viewModel.Dispose();
        }
        
        public void SubmitPrompt()
        {
            viewModel.SubmitPrompt();
        }
        public void OnUpdate(ISelectable diagramNode)
        {
            if (diagramNode is not DiagramNodeBase nodeBase)
                return;

            analyzer.SetNodeModel(nodeBase.NodeModel);
        }
        
        public void Load()
        {
            betaView.apiKey.textField.element.value = config.gpt.apiKey;
            betaView.model.dropdown.element.value = config.gpt.model;
            betaView.maxTokens.slider.element.value = config.gpt.maxTokens;
            betaView.topP.slider.element.value = config.gpt.topP;
            betaView.temperature.slider.element.value = config.gpt.temperature;
            betaView.frequencyPenalty.slider.element.value = config.gpt.frequencyPenalty;
            betaView.presencePenalty.slider.element.value = config.gpt.presencePenalty;
            betaView.promptTextField.element.value = config.gpt.prompt;
            betaView.attachmentView.element.contentContainer.Clear();
            config.gpt.promptContent.meaningfulHistoryIndices.Clear();

            betaView.designTitleTextField.element.value = config.gpt.promptContent.designModel.title;
            betaView.designTimingTextField.element.value = config.gpt.promptContent.designModel.timing;
            betaView.designLevelTextField.element.value = config.gpt.promptContent.designModel.level;
            betaView.designSubjectTextField.element.value = config.gpt.promptContent.designModel.subject;
            betaView.designGoalTextField.element.value = config.gpt.promptContent.designModel.goal;
            betaView.designStyleTextField.element.value = config.gpt.promptContent.designModel.style;
            
            for (var index = 0; index < config.gpt.promptContent.history.Count; index++)
            {
                DialogueModel dialogue = config.gpt.promptContent.history[index];
                
                if(dialogue.content.IsNullOrEmpty()) continue;
                DialogueItem viewItem = betaView.AddDialogueItem(dialogue.speaker, dialogue.content, index);
                viewModel.BindDialogue(viewItem.element);
            }

            betaView.fileAttachmentButton.StyleBackgroundColor(new Color(0.25f, 0.5f, 1f, 1));
            betaView.nodeAttachmentButton.StyleBackgroundColor(new Color(0.5f,0.5f,0.5f,1));
            
            betaView.attachmentView.StyleDisplay(DisplayStyle.Flex);
            betaView.nodeTreeView.StyleDisplay(DisplayStyle.None);
            
            foreach (var file in config.gpt.promptContent.attachments)
            {
                IconLineElement attachment = betaView.CreateAttachmentLine("d_Collab.FileAdded", file.path);
                attachment.element.userData = file;
                Button removeButton = attachment.element.Q<Button>();
                Toggle selectToggle = attachment.element.Q<Toggle>();
                VisualElement space = betaView.attachmentView.element.CreateVerticalSpace(10);
            
                removeButton.clicked += () =>
                {
                    viewModel.BindAttachmentItemRemoveButton(space, attachment.element);
                };
            
                selectToggle.RegisterValueChangedCallback(e =>
                {
                    viewModel.BindAttachmentItemSelectToggle(e, attachment.element, file);
                });            
            }
        }
        public void Dispose()
        {
            viewModel.Dispose();
        }
    }

}
