using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Share;
using Diagram;
using Diagram.Config;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Utility.ExcelReader;
using Writer.AssetGenerator.UIElement;
using ColorUtility = UnityEngine.ColorUtility;
using JsonConvert = Unity.Plastic.Newtonsoft.Json.JsonConvert;

namespace GPT
{
    public class GPTBetaViewModel
    {
        private BlackboardGPT gptBoard;
        private GPTBetaView view;
        // basic
        public readonly IAudio audioService;
        public readonly IChat chatService;
        public readonly IEmbedding embeddingService;
        public readonly IFineTuning fineTuningService;
        public readonly IFile fileService;
        public readonly IImage imageService;
        public readonly IModel modelService;
        public readonly IModeration moderationService;
        
        // beta
        public readonly IAssistants assistantService;
        public readonly IThreads chatServiceBeta;
        public readonly IMessages messageService;
        public readonly IRuns runService;
        private readonly string[] timerTexts =
        {
            "Waiting", "Waiting.", "Waiting..", "Waiting...",
            "Waiting....", "Waiting.....", "Waiting......", "Waiting.......",
            
        };
        private bool isPending;
        private Dictionary<DialogueItem, Queue<string>> responseMap;
        public GPTBetaViewModel(BlackboardGPT gpt, GPTBetaView view, DiagramConfig config)
        {
            gptBoard = gpt;
            this.view = view;
            responseMap = new Dictionary<DialogueItem, Queue<string>>();
            
            audioService = new AudioUsecase(config);
            chatService = new ChatUsecase(config);
            embeddingService = new EmbeddingsUsecase(config);
            fineTuningService = new FineTuningUsecase(config);
            fileService = new FileUsecase(config);
            imageService = new ImageUsecase(config);
            modelService = new ModelUsecase(config);
            moderationService = new ModerationUsecase(config);
            
            assistantService = new AssistantUsecase(config);
            chatServiceBeta = new ThreadUsecase(config);
            messageService = new MessageUsecase(config);
            runService = new RunUsecase(config);
        }
        
        private void WaitingResponseTask()
        {
            gptBoard.responseUpdater = gptBoard.schedule.Execute(() =>
            {
                view.submitButton.element.text = isPending ? timerTexts[(int)Time.time % timerTexts.Length] : "Submit";
                view.submitButton.element.SetEnabled(!isPending);
                
                if(gptBoard.config.gpt.responses.Count == 0) return;

                // add
                DialogueModel response = gptBoard.config.gpt.responses.Dequeue();
                int dialogueIndex = gptBoard.config.gpt.promptContent.history.Count;
                DialogueItem dialog = view.AddDialogueItem(response.speaker, response.content, dialogueIndex);
            
                BindDialogue(dialog.element);
            
                responseMap.Add(dialog, new Queue<string>());
            
                // add words
                foreach (dynamic data in response.raw)
                {
                    foreach (dynamic choice in data.choices)
                    {
                        if (choice.delta.content == null) continue;
                    
                        string content = choice.delta.content;
                        responseMap[dialog].Enqueue(content);
                    }
                }
                gptBoard.config.gpt.promptContent.history.Add(new DialogueModel(response));
            }).Every(5);
        }

        private void TypingResponseTask()
        {
            gptBoard.typingTasker = gptBoard.schedule.Execute(() =>
            {
                if (responseMap.Count == 0) return;
                view.responseView.element.verticalScroller.value = 
                    Mathf.Lerp(view.responseView.element.verticalScroller.value,
                        view.responseView.element.verticalScroller.highValue,
                        Time.time);
                
                KeyValuePair<DialogueItem, Queue<string>> queue = responseMap.FirstOrDefault();

                DialogueItem dialogView = queue.Key;
                dialogView.contentLabel.element.text += queue.Value.Dequeue();

                if (queue.Value.Count != 0) return;
                gptBoard.config.gpt.promptContent.history[(int)dialogView.element.userData].content = dialogView.contentLabel.element.text;
                responseMap.Remove(dialogView);
                isPending = false;

            }).Every(10);
        }
        private void OpenAPIKey()
        {
            string url = "https://platform.openai.com/api-keys";
            Application.OpenURL(url);
        }
        private void BindAPIKey(FocusOutEvent evt)
        {
            gptBoard.config.gpt.apiKey = view.apiKey.textField.element.value;
        }

        private void BindModel(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.model = view.model.dropdown.element.value;
        }
        private void BindMaxTokens(ChangeEvent<float> evt)
        {
            gptBoard.config.gpt.maxTokens = (int)evt.newValue;
        }
        private void BindTopP(ChangeEvent<float> evt)
        {
            gptBoard.config.gpt.topP = evt.newValue;
        }
        
        private void BindTemperature(ChangeEvent<float> evt)
        {
            gptBoard.config.gpt.temperature =evt.newValue;
        }
        
        private void BindFrequencyPenalty(ChangeEvent<float> evt)
        {
            gptBoard.config.gpt.frequencyPenalty = evt.newValue;
        }
        
        private void BindPresencePenalty(ChangeEvent<float> evt)
        {
            gptBoard.config.gpt.presencePenalty = evt.newValue;
        }
        private void BindPrompt(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.prompt = evt.newValue;
        }
        private async Task<string> ReadTextFileAsync(string path)
        {
            StringBuilder sb = new StringBuilder();
            string text = "";
            if(System.IO.File.Exists(path))
            {
                if (Path.GetExtension(path) == ".xlsx")
                {
                    (string topCategory, SerializedDictionary<string, ExcelSheetInfo> map) = ExcelCsvReader.Read(path);
                    text = JsonConvert.SerializeObject(map);
                }
                else if (Path.GetExtension(path) == ".csv")
                {
                    text = ExcelCsvReader.ReadCSV(path).Select(x => $"{x.Key} : {string.Join("\n", x.Value)}").Aggregate((current, next) => $"{current}\n{next}");
                }
                else
                    text = await System.IO.File.ReadAllTextAsync(path);
            }
            int expectedTokens = text.Length / 4;
            int limitTokens = 128_000;
            
            if (expectedTokens > limitTokens)
            {
                text = text[..limitTokens];
            }
            sb.AppendLine(text);
            Debug.Log($"ReadTextFileAsync : {text}");
            return sb.ToString();
        }

        private async Task<string> ReadTextInnerFilesAsync(string folderPath)
        {
            StringBuilder sb = new StringBuilder();

            string[] innerFiles = System.IO.Directory.GetFiles(folderPath);
            if (innerFiles.Length == 0) return string.Empty;
            foreach (string file in innerFiles)
            {
                sb.AppendLine(await System.IO.File.ReadAllTextAsync(file));
            }
            
            return sb.ToString();
        }

        private async Task<string> AttachFileTexts(IEnumerable<string> filePaths)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder PathSb = new StringBuilder();
            foreach (string path in filePaths)
            {
                // file
                sb.AppendLine(await ReadTextFileAsync(path));
                PathSb.AppendLine($"*\t{path}");
                if (!Directory.Exists(path)) continue;
                
                // directory
                sb.AppendLine(await ReadTextInnerFilesAsync(path));
                string[] innerFolders = System.IO.Directory.GetDirectories(path);
                if (innerFolders.Length == 0) continue;
                sb.AppendLine(await AttachFileTexts(innerFolders));
            }
            return sb.ToString();
        }

        private string GetDesignFormat()
        {
            return $@"{gptBoard.config.gpt.promptContent.designModel.title}
    - Target Timing: {gptBoard.config.gpt.promptContent.designModel.timing}
    - Target Level: {gptBoard.config.gpt.promptContent.designModel.level}
    - Subject: {gptBoard.config.gpt.promptContent.designModel.subject}
    - Goal: {gptBoard.config.gpt.promptContent.designModel.goal}
    - Style: {gptBoard.config.gpt.promptContent.designModel.style}";
        }
        
        public async void SubmitPrompt()
        {
            if(isPending) return;
            VisualElement question = view.AddDialogueItem(
                "You",
                gptBoard.config.gpt.prompt,
                gptBoard.config.gpt.promptContent.history.Count)
                .element;
            gptBoard.config.gpt.promptContent.history.Add(
                new DialogueModel
                {
                    speaker = "You",
                    content = gptBoard.config.gpt.prompt
                });
            view.responseView.element.verticalScroller.value = view.responseView.element.verticalScroller.highValue;
            BindDialogue(question);

            string context = string.Empty;
            if(gptBoard.config.gpt.promptContent.fileAttachmentActiveSelf)
            {
                if (gptBoard.config.gpt.promptContent.attachments.Count > 0)
                {
                    IEnumerable<string> filePaths =
                        gptBoard.config.gpt.promptContent.attachments.Select(file => file.path);
                    string payload = await AttachFileTexts(filePaths);
                    context =
                        $@"{gptBoard.config.gpt.prompt}.and Refer by these following files contents : ""{payload}""";
                }
            }
            else
            {
                context =
                    $@"{gptBoard.config.gpt.prompt}.and Refer by these following files contents : ""{gptBoard.config.gpt.promptContent.ToTreeViewString()}""";
            }

            bool hasDesign = !string.IsNullOrEmpty(gptBoard.config.gpt.promptContent.designModel.title);
            
            await GetSendChatResponse($"Context : {context}.{(hasDesign ? $" Design : {GetDesignFormat()}. " : string.Empty)}Now : {gptBoard.config.gpt.prompt}");
        }

        private async Task GetSendChatResponse(string input)
        {
            isPending = true;
            dynamic response = await chatService.CreateChatCompletion(
                new Prompt()
                {
                    Reward = GPTSystemContent.reward,
                    Instruction = GPTSystemContent.instruction,
                    Language = gptBoard.config.gpt.language.ToString(),
                    MaxTokens = gptBoard.config.gpt.maxTokens,
                    Role = gptBoard.config.gpt.role,
                    Tone = "I want to know about",
                    History = string.Join(", ", gptBoard.config.gpt.promptContent.history),
                }, 
                GPTResponseMode.Streaming,
                new CreateChatMessagePayload
                {
                    Role = "user",
                    Content = $"{input}"
                });
            
            DialogueModel model = new DialogueModel
            {
                speaker = $"{gptBoard.config.gpt.model}",
                content = "",
                raw = response
            };
            
            gptBoard.config.gpt.responses.Clear();
            gptBoard.config.gpt.responses.Enqueue(model);
        }
        public async Task GetSummaryResponse(string input)
        {
            isPending = true;
            dynamic response = await chatService.CreateChatCompletion(
                new Prompt()
                {
                    Reward = GPTSystemContent.reward,
                    Instruction = GPTSystemContent.instruction,
                    Language = gptBoard.config.gpt.language.ToString(),
                    MaxTokens = gptBoard.config.gpt.maxTokens,
                    Role = GPTSystemContent.summarizerRole,
                    Tone = "I want to know about",
                    Content = input,
                },
                GPTResponseMode.Block,
                new CreateChatMessagePayload()
                {
                    Role = "user",
                    Content = input

                });
            
            string finish_reason = response.choices[0].finish_reason;
                
            if(finish_reason is "length" or "max_tokens")
            {
                gptBoard.config.gpt.maxTokens += gptBoard.config.gpt.retryTokenStep;
                await GetSendChatResponse(input);
                return;
            }
            
            dynamic responseMessage = response.choices[0].message.content;
            int completionTokens  = response.usage?.completion_tokens;
            
            gptBoard.config.gpt.summary = (responseMessage, completionTokens);
            isPending = false;
        }
        
        private void SaveDialogue()
        {
            gptBoard.config.gpt.SaveHistory();
            ClearDirty();
        }

        private void LoadDialogue()
        {
            gptBoard.config.gpt.Load();
            
            view.responseView.element.contentContainer.Clear();
            gptBoard.config.gpt.promptContent.history.For((index,history) =>
            {
                VisualElement dialogue = view.AddDialogueItem(
                    history.speaker,
                    history.content,
                    index).element;
                BindDialogue(dialogue);
            });
        }
        private void ClearDialogues()
        {
            bool isYes = EditorUtility.DisplayDialog("Warning", "Are you sure to clear all dialogues?", "Yes", "No");
            if (!isYes) return;
            
            view.responseView.element.contentContainer.Clear();
            gptBoard.config.gpt.promptContent.history.Clear();
            gptBoard.config.gpt.promptContent.meaningfulHistoryIndices.Clear();
            gptBoard.config.gpt.responses.Clear(); 
            gptBoard.config.gpt.promptContent.attachments.Clear();
            gptBoard.config.gpt.summary = (string.Empty, 0);
        }

        public void BindDialogue(VisualElement dialogue)
        {
            if (dialogue.userData == null) return;
            
            Toggle toggle = dialogue.Q<Toggle>();
            toggle.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue)
                {
                    gptBoard.config.gpt.promptContent.meaningfulHistoryIndices.Add((int)dialogue.userData);
                }
                else
                {
                    gptBoard.config.gpt.promptContent.meaningfulHistoryIndices.Remove((int)dialogue.userData);
                }
            });
        }

        private void SetDirty()
        {
            view.dialogueSaveButton.element.text = "*Save";
        }

        private void ClearDirty()
        {
            view.dialogueSaveButton.element.text = "Save";
        }

        private string GetHistory()
        {
            string summary;
            if (gptBoard.config.gpt.promptContent.meaningfulHistoryIndices.Count == 0)
            {
                summary = string.Join('\n', gptBoard.config.gpt.promptContent.history);
            }
            else
            {
                summary = string.Join('\n', gptBoard.config.gpt.promptContent.meaningfulHistoryIndices
                    .Select(index => $"{gptBoard.config.gpt.promptContent.history[index]}"));
            }    
            return summary;
        }

        private void SummaryDialogue()
        {
            if(isPending) 
                return;
            
            if(gptBoard.config.gpt.promptContent.history.Count == 0)
                return;

            string summary = GetHistory();
            
            if(summary.IsNullOrEmpty())
            {
                view.responseView.element.contentContainer
                    .Add(view.AddDialogueItem("System", "There is no history to summarize.", -1).element);
                return;
            }

            _ = Task.Run(() => GetSummaryResponse(summary));
            SetDirty();
            view.responseView.element.contentContainer
                .Add(view.AddDialogueItem("System", "Summary is being generated.", -1).element);
        }

        private void CopyDialogue()
        {
            if (gptBoard.config.gpt.promptContent.meaningfulHistoryIndices.Count == 0) return;
            view.promptTextField.element.value = gptBoard.config.gpt.promptContent.meaningfulHistoryIndices
                .Select(index => gptBoard.config.gpt.promptContent.history[index].content)
                .Aggregate((current, next) => $"{current}\n{next}");
        }
        
        private void OnSelectFileAttachment()
        {
            view.fileAttachmentButton.StyleBackgroundColor(new Color(0.25f,0.5f,1f,1));
            view.nodeAttachmentButton.StyleBackgroundColor(new Color(0.5f,0.5f,0.5f,1));

            view.attachmentView.StyleDisplay(DisplayStyle.Flex);
            view.nodeTreeView.StyleDisplay(DisplayStyle.None);
            gptBoard.config.gpt.promptContent.fileAttachmentActiveSelf = true;
        }
        private void OnSelectNodeAttachment()
        {
            view.fileAttachmentButton.StyleBackgroundColor(new Color(0.5f,0.5f,0.5f,1));
            view.nodeAttachmentButton.StyleBackgroundColor(new Color(0.25f,0.5f,1f,1));

            view.attachmentView.StyleDisplay(DisplayStyle.None);
            view.nodeTreeView.StyleDisplay(DisplayStyle.Flex);
            gptBoard.config.gpt.promptContent.fileAttachmentActiveSelf = false;
            DrawTreeView();
        }

        public void DrawTreeView()
        {
            DiagramNodeBase[] nodes = gptBoard.gui.view.diagramNodes.Values.ToArray();
            List<TreeViewItemData<string>> treeViewItems = new List<TreeViewItemData<string>>();

            Color typeColor = new Color(0, 0.5f, 0.75f, 1);
            string typeColorHtmlRGB = ColorUtility.ToHtmlStringRGB(typeColor);
            int treeViewItemIndex = 0;
            for (var index = 0; index < nodes.Length; index++)
            {
                DiagramNodeBase node = nodes[index];
                if(node.NodeModel.Status.GraphElementType 
                   is GraphElementType.Memo 
                   or GraphElementType.Group 
                   or GraphElementType.Note) 
                    continue;
                
                TreeViewItemData<string> nodeItem = default;
                if (node.NodeModel.Header.Properties.Count == 0)
                {
                    nodeItem = new TreeViewItemData<string>(treeViewItemIndex++, node.NodeModel.Header.Name);
                }
                else
                {
                    List<TreeViewItemData<string>> propertyItems = new List<TreeViewItemData<string>>();
                    for(var propertyIndex = 0; propertyIndex < node.NodeModel.Header.Properties.Count; propertyIndex++)
                    {
                        PropertyFieldInfo property = node.NodeModel.Header.Properties[propertyIndex];
                        propertyItems.Add(new TreeViewItemData<string>(treeViewItemIndex++, $"{property.Name} : <color=#{typeColorHtmlRGB}>{property.Type}</color>"));
                    }
                    nodeItem = new TreeViewItemData<string>(treeViewItemIndex++, node.NodeModel.Header.Name, propertyItems);
                }
                treeViewItems.Add(nodeItem);
            }
            view.nodeTreeView.element.SetRootItems(treeViewItems);            
        }

        private void BindDragAndDropFilesCursor(MouseEnterEvent evt)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }
        
        private void BindDragAndDropFiles(DragUpdatedEvent evt)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }

        public void BindAttachmentItemRemoveButton(params VisualElement[] elements)
        {
            if(elements.Length == 0) return;
            foreach (VisualElement element in elements)
            {
                view.attachmentView.element.Remove(element);
            }
        }

        public void BindAttachmentItemSelectToggle(ChangeEvent<bool> e, VisualElement item, FileModel file)
        {
            if (e.newValue)
            {
                if (gptBoard.config.gpt.promptContent.attachments.Contains(file)) 
                    return;
                    
                item.style.backgroundColor = new StyleColor(new Color(0.25f,0.5f,1f,1));
                gptBoard.config.gpt.promptContent.attachments.Add(file);
            }
            else
            {
                if (!gptBoard.config.gpt.promptContent.attachments.Contains(file)) 
                    return;
                    
                item.style.backgroundColor = new StyleColor(Color.clear);
                gptBoard.config.gpt.promptContent.attachments.Remove(file);
            }
        }

        private void AddFileAttachmentList(string path)
        {
            string fileName = Path.GetFileName(path);
            
            FileModel file = new FileModel
            {
                path = path,
                name = fileName
            };
            
            VisualElement item = view.CreateAttachmentLine("d_Collab.FileAdded", fileName).element;
            item.userData = file;
            Button removeButton = item.Q<Button>();
            Toggle selectToggle = item.Q<Toggle>();
            VisualElement space = view.attachmentView.element.CreateVerticalSpace(10);
            
            removeButton.clicked += () =>
            {
                BindAttachmentItemRemoveButton(space, item);
            };
            
            selectToggle.RegisterValueChangedCallback(e =>
            {
                BindAttachmentItemSelectToggle(e, item, file);
            });
            view.attachmentView.element.Add(item);
        }
        
        private void BindDragAndDropFiles(DragPerformEvent evt)
        {
            DragAndDrop.AcceptDrag();
            string[] files = DragAndDrop.paths;
            if (files.Length == 0) return;
            Debug.Log($"files : {string.Join(",", files)}");
            string path = files.FirstOrDefault();
            if (path.IsNullOrEmpty()) return;
            AddFileAttachmentList(path);
        }

        private void OnDesignFields()
        {
            view.designContainer.element.style.display 
                = view.designContainer.element.style.display == DisplayStyle.Flex 
                ? DisplayStyle.None 
                : DisplayStyle.Flex;
        }
        
        private void BindDesignTitleField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.title = evt.newValue;
        }
        
        private void BindDesignTimingField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.timing = evt.newValue;
        }
        
        private void BindDesignLevelField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.level = evt.newValue;
        }
        
        private void BindDesignSubjectField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.subject = evt.newValue;
        }
        
        private void BindDesignGoalField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.goal = evt.newValue;
        }
        
        private void BindDesignStyleField(ChangeEvent<string> evt)
        {
            gptBoard.config.gpt.promptContent.designModel.style = evt.newValue;
        }
        
        public void Bind()
        {
            view.apiKey.textField.element.RegisterCallback<FocusOutEvent>(BindAPIKey);
            view.apiKey.button.element.clicked += OpenAPIKey; 
            view.model.dropdown.element.RegisterValueChangedCallback(BindModel);
            view.maxTokens.slider.element.RegisterValueChangedCallback(BindMaxTokens);
            view.topP.slider.element.RegisterValueChangedCallback(BindTopP);
            view.temperature.slider.element.RegisterValueChangedCallback(BindTemperature);
            view.frequencyPenalty.slider.element.RegisterValueChangedCallback(BindFrequencyPenalty);
            view.presencePenalty.slider.element.RegisterValueChangedCallback(BindPresencePenalty);
            view.promptTextField.element.RegisterValueChangedCallback(BindPrompt);

            view.fileAttachmentButton.element.clickable.clicked += OnSelectFileAttachment;
            view.nodeAttachmentButton.element.clickable.clicked += OnSelectNodeAttachment;
            
            view.attachmentView.element.RegisterCallback<MouseEnterEvent>(BindDragAndDropFilesCursor);
            view.attachmentView.element.RegisterCallback<DragUpdatedEvent>(BindDragAndDropFiles);
            view.attachmentView.element.RegisterCallback<DragPerformEvent>(BindDragAndDropFiles);
            
            view.submitButton.element.clicked += SubmitPrompt;
            view.dialogueSummaryButton.element.clicked += SummaryDialogue;
            view.designDialogueButton.element.clicked += OnDesignFields;
            
            view.selectionCopyButton.element.clicked += CopyDialogue;
            view.dialogueSaveButton.element.clicked += SaveDialogue;
            view.dialogueLoadButton.element.clicked += LoadDialogue;
            view.dialogueClearButton.element.clicked += ClearDialogues;
            
            view.designTitleTextField.element.RegisterValueChangedCallback(BindDesignTitleField);
            view.designTimingTextField.element.RegisterValueChangedCallback(BindDesignTimingField);
            view.designLevelTextField.element.RegisterValueChangedCallback(BindDesignLevelField);
            view.designSubjectTextField.element.RegisterValueChangedCallback(BindDesignSubjectField);
            view.designGoalTextField.element.RegisterValueChangedCallback(BindDesignGoalField);
            view.designStyleTextField.element.RegisterValueChangedCallback(BindDesignStyleField);
            
            WaitingResponseTask();
            TypingResponseTask();
        }

        public void Dispose()
        {
            view.apiKey.element.UnregisterCallback<FocusOutEvent>(BindAPIKey);
            view.apiKey.button.element.clicked -= OpenAPIKey;
            view.model.dropdown.element.UnregisterValueChangedCallback(BindModel);
            view.maxTokens.slider.element.UnregisterValueChangedCallback(BindMaxTokens);
            view.topP.slider.element.UnregisterValueChangedCallback(BindTopP);
            view.temperature.slider.element.UnregisterValueChangedCallback(BindTemperature);
            view.frequencyPenalty.slider.element.UnregisterValueChangedCallback(BindFrequencyPenalty);
            view.presencePenalty.slider.element.UnregisterValueChangedCallback(BindPresencePenalty);
            view.promptTextField.element.UnregisterValueChangedCallback(BindPrompt);
            
            view.fileAttachmentButton.element.clickable.clicked -= OnSelectFileAttachment;
            view.nodeAttachmentButton.element.clickable.clicked -= OnSelectNodeAttachment;
            
            view.attachmentView.element.UnregisterCallback<MouseEnterEvent>(BindDragAndDropFilesCursor);
            view.attachmentView.element.UnregisterCallback<DragUpdatedEvent>(BindDragAndDropFiles);
            view.attachmentView.element.UnregisterCallback<DragPerformEvent>(BindDragAndDropFiles);
            
            view.submitButton.element.clicked -= SubmitPrompt;
            view.dialogueSummaryButton.element.clicked -= SummaryDialogue;
            view.designDialogueButton.element.clicked -= OnDesignFields;
            
            view.selectionCopyButton.element.clicked -= CopyDialogue;
            view.dialogueSaveButton.element.clicked -= SaveDialogue;
            view.dialogueLoadButton.element.clicked -= LoadDialogue;
            view.dialogueClearButton.element.clicked -= ClearDialogues;

            view.designTitleTextField.element.UnregisterValueChangedCallback(BindDesignTitleField);
            view.designTimingTextField.element.UnregisterValueChangedCallback(BindDesignTimingField);
            view.designLevelTextField.element.UnregisterValueChangedCallback(BindDesignLevelField);
            view.designSubjectTextField.element.UnregisterValueChangedCallback(BindDesignSubjectField);
            view.designGoalTextField.element.UnregisterValueChangedCallback(BindDesignGoalField);
            view.designStyleTextField.element.UnregisterValueChangedCallback(BindDesignStyleField);
            
            gptBoard.responseUpdater?.Pause();
            gptBoard.responseUpdater = null;
            gptBoard.typingTasker?.Pause();
            gptBoard.typingTasker = null;
        }
    }
}