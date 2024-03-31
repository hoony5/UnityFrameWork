using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Share;
using UnityEditor;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace Diagram
{
    public class BlackboardNoteNodeBinder : IDisposable
    {
        private DiagramBlackboard blackboard;
        public BlackboardNoteNodeView view;
        public DiagramNoteNode node;
        private BlackboardSlackBinder slack;
        private BlackboardConfluenceBinder confluence;
        private bool forceRewrite;
        
        public BlackboardNoteNodeBinder(DiagramBlackboard blackboard, BlackboardNoteNodeView view)
        {
            this.blackboard = blackboard;
            this.view = view;
        }
        
        public void SetUp(DiagramNoteNode selection)
        {
            node = selection;
            slack = new BlackboardSlackBinder(view, node.GUI.Config);
            confluence = new BlackboardConfluenceBinder(view, node.GUI.Config);
            slack.node = selection;
            confluence.node = selection;
        }

        private void SetConfluenceConfig(string baseURL, string userName, string apiToken, string spaceKey)
        {
            node.NodeModel.Blackboard.ConfluenceBaseUrl = baseURL;
            node.NodeModel.Blackboard.ConfluenceUserName = userName;
            node.NodeModel.Blackboard.ConfluenceAPIToken = apiToken;
            node.NodeModel.Blackboard.ConfluenceSpaceKey = spaceKey;
            
            view.confluence.confluenceLinkField.value = baseURL;
            view.confluence.confluenceUserNameField.value = userName;
            view.confluence.confluenceAPITokenField.value = apiToken;
            view.confluence.confluenceSpaceKeyField.value = spaceKey;
        }
        private void SetSlackConfig(string webHookURL, string channel, string userName, string iconURL)
        {
            node.NodeModel.Blackboard.SlackWebhookUrl = webHookURL;
            node.NodeModel.Blackboard.SlackChannel = channel;
            node.NodeModel.Blackboard.SlackUserName = userName;
            node.NodeModel.Blackboard.SlackIconUrl = iconURL;
            
            view.slack.slackLinkField.value = webHookURL;
            view.slack.slackChannelField.value = channel;
            view.slack.slackUserNameField.value = userName;
            view.slack.slackIconURLField.value = iconURL;
        }
        private void BindGlobalConfigToggle(ChangeEvent<bool> evt)
        {
            if (evt.newValue)
            {
                bool isOverwrite = EditorUtility.DisplayDialog("Warning", "It will overwrite the current settings.", "OK", "Cancel");
                if (!isOverwrite)
                {
                    view.useGlobalConfig.SetValueWithoutNotify(false);
                    return;
                }
                SetConfluenceConfig(
                    node.GUI.Config.confluence.url,
                    node.GUI.Config.confluence.username,
                    node.GUI.Config.confluence.apiToken,
                    node.GUI.Config.confluence.spaceKey);
                
                SetSlackConfig(
                    node.GUI.Config.slack.webhookUrl,
                    node.GUI.Config.slack.channel,
                    node.GUI.Config.slack.username,
                    node.GUI.Config.slack.iconUrl);
                
                view.slack.slackIconURLField.Focus();
                view.slack.slackIconURLField.Blur();
                
                view.openConfigButton.SetDisplay(true);
                return;
            }
            view.openConfigButton.SetDisplay(false);
            SetConfluenceConfig(
                node.NodeModel.Blackboard.ConfluenceBaseUrl,
                node.NodeModel.Blackboard.ConfluenceUserName,
                node.NodeModel.Blackboard.ConfluenceAPIToken,
                node.NodeModel.Blackboard.ConfluenceSpaceKey);
            
            SetSlackConfig(
                node.NodeModel.Blackboard.SlackWebhookUrl,
                node.NodeModel.Blackboard.SlackChannel,
                node.NodeModel.Blackboard.SlackUserName,
                node.NodeModel.Blackboard.SlackIconUrl);
        }

        private void BindLoadNodeName(FocusOutEvent evt)
        {
            view.titleField.value = node.NodeModel.Note.Title;
        }
        private void BindSetNodeName(ChangeEvent<string> evt)
        {
            node.NodeModel.Note.Title = evt.newValue;
            if (!node.SectionFactory.TryGetSection(out HeaderSection headerSection)) return;
            headerSection.view.nameTextField.value = evt.newValue;
        }
        private void BindExportType()
        {
            view.exportType.value = node.NodeModel.Note.ExportFileType.ToString();
        }

        private void ResetVisibilities()
        {
            view.filePathContainer.SetDisplay(false);
            view.confluence.mainContainer.SetDisplay(false);
            view.slack.mainContainer.SetDisplay(false);
        }

        private void ToggleVisibility()
        {
            switch (view.exportType.value)
            {
                case "Not_Used":
                    break;
                case "Text":
                case "CSV":
                case "MarkDown":
                    view.filePathContainer.SetDisplay(true);
                    break;
                case "Confluence":
                    view.confluence.mainContainer.SetDisplay(true);
                    break;
                case "Slack":
                    view.slack.mainContainer.SetDisplay(true);
                    break;
            }
        }
        
        private void BindInputFieldByExportType()
        {
            ResetVisibilities();
            ToggleVisibility();
        }

        private void BindExportExample()
        {
            blackboard.exportExampleTask = blackboard.schedule.Execute(() =>
            {
                if(node == null) return;
                if(forceRewrite) return;
                view.exportExampleField.value 
                    = node.noteSection.baseNote.GetDescription(
                        Enum.TryParse(typeof(ExportFileType),
                            view.exportType.value, out object exportFileType) 
                            ? (ExportFileType)exportFileType 
                            : ExportFileType.MarkDown);

                if (node.NodeModel == null) return;
                node.NodeModel.Header.Description = view.exportExampleField.value;

            }).Every(250);
        }
        private void BindRewriteToggle(ChangeEvent<bool> evt)
        {
            if (forceRewrite && !evt.newValue)
            {
                bool isAutomated = EditorUtility.DisplayDialog("Warning",
                    @"This will remove all the content on the note.", "Yes", "No");
                    
                if(isAutomated)
                {
                    forceRewrite = false;
                    return;
                }
            }
            
            view.exportRewriteToggle.SetValueWithoutNotify(true);
            forceRewrite = true;
        }

        private void BindExportExample(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.ExportExampleNote = evt.newValue;
        }
        private void BindExportFilePath()
        {
            switch (node.NodeModel.Note.ExportFileType)
            {
                default:
                case ExportFileType.Text:
                    view.exportPathField.value = EditorUtility.SaveFilePanel("Save File", "", "export", "txt");
                    return;
                case ExportFileType.CSV:
                    view.exportPathField.value = EditorUtility.SaveFilePanel("Save File", "", "export", "csv");
                    return;
                case ExportFileType.MarkDown:
                    view.exportPathField.value = EditorUtility.SaveFilePanel("Save File", "", "export", "md");
                    return;
            }
        }

        private async Task ExportToFile()
        {
            // file path
            string filePath = view.exportPathField.value;
            if (filePath.IsNullOrEmpty()) return;
            
            // file content
            string content = node.NodeModel.Blackboard.ExportExampleNote;
            if (content.IsNullOrEmpty()) return;
            
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            // write file async
            await using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                await file.WriteLineAsync(content);
                tcs.SetResult(true);
            }
            
            tcs.Task.Wait();

            Debug.Log(!tcs.Task.IsCompleted 
                ? $"Failed to export file | {filePath}" 
                : $"Exported to {filePath}");

            if (tcs.Task.IsCompleted)
            {
                 // open folder
                string folderPath = filePath.Substring(0, filePath.LastIndexOf("/", StringComparison.Ordinal));
                Process.Start(folderPath);
            }
        }

        private void BindExportType(ChangeEvent<string> evt)
        {
            node.NodeModel.Note.ExportFileType = Enum.TryParse(typeof(ExportFileType), evt.newValue, out object exportFileType) 
                ? (ExportFileType)exportFileType 
                : ExportFileType.Text;
            
            if(node.SectionFactory.TryGetSection(out HeaderSection headerSection))
                headerSection.view.noteExportFileTypeButton.text = node.ModelModifier.GetNodeViewTypeName<ExportFileType>();
            
            ResetVisibilities();
            ToggleVisibility();
            
            view.useGlobalConfig.SetDisplay(false);

            if (node.NodeModel.Note.ExportFileType is 
                not (ExportFileType.Slack or ExportFileType.Confluence))
                return;
            
            view.useGlobalConfig.SetDisplay(true);
        }
        private void BindExport()
        {
            Debug.Log($"{node.NodeModel.Note.ExportFileType} | Exporting...");
            switch (node.NodeModel.Note.ExportFileType)
            {
                case ExportFileType.Not_Used:
                    return;
                case ExportFileType.Slack:
                    _ = Task.Run(slack.SendMessageToSlack);
                    return;
                case ExportFileType.Confluence:
                    _ = Task.Run(confluence.SendMessageToConfluence);
                    return;
                case ExportFileType.Text:
                case ExportFileType.CSV:
                case ExportFileType.MarkDown:
                    _ = Task.Run(ExportToFile);
                    return;
            }
        }
        public void Bind()
        {
            if (node == null) return;
            
            confluence.BindTitleSettingToggle(blackboard);
            view.openConfigButton.clicked += node.GUI.BindOpenConfigPathPanel;
            view.useGlobalConfig.RegisterValueChangedCallback(BindGlobalConfigToggle);
            
            view.titleField.RegisterValueChangedCallback(BindSetNodeName);
            view.exportType.RegisterValueChangedCallback(BindExportType);
            view.exportExampleField.RegisterValueChangedCallback(BindExportExample);
            view.exportPathButton.clicked += BindExportFilePath;
            view.exportRewriteToggle.RegisterValueChangedCallback(BindRewriteToggle);
            view.exportButton.clicked += BindExport;
            
            slack.Bind();
            confluence.Bind();
            
            BindExportExample();
            if (!node.SectionFactory.TryGetSection(out HeaderSection headerSection)) return;
            headerSection.view.nameTextField.RegisterCallback<FocusOutEvent>(BindLoadNodeName);
            headerSection.view.noteExportFileTypeButton.clicked += BindExportType;
            headerSection.view.noteExportFileTypeButton.clicked += BindInputFieldByExportType;
        }

        public void Dispose()
        {
            if (node == null) return;
            
            if (blackboard.getConfluenceTitleTask != null)
            {
                blackboard.getConfluenceTitleTask.Pause();
                blackboard.getConfluenceTitleTask = null;
            }
            
            view.titleField.UnregisterValueChangedCallback(BindSetNodeName);
            view.useGlobalConfig.UnregisterValueChangedCallback(BindGlobalConfigToggle);
            view.openConfigButton.clicked -= node.GUI.BindOpenConfigPathPanel;
            view.exportType.UnregisterValueChangedCallback(BindExportType);
            view.exportExampleField.UnregisterValueChangedCallback(BindExportExample);
            view.exportPathButton.clicked -= BindExportFilePath;
            view.exportRewriteToggle.UnregisterValueChangedCallback(BindRewriteToggle);
            view.exportButton.clicked -= BindExport;
            
            slack?.Dispose();
            confluence?.Dispose();
            
            if (!node.SectionFactory.TryGetSection(out HeaderSection headerSection)) return;
            headerSection.view.nameTextField.UnregisterCallback<FocusOutEvent>(BindLoadNodeName);
            headerSection.view.noteExportFileTypeButton.clicked -= BindExportType;
            headerSection.view.noteExportFileTypeButton.clicked -= BindInputFieldByExportType;
        }

        public void Load()
        {
            if(blackboard.exportExampleTask != null)
            {
                blackboard.exportExampleTask.Pause();
                blackboard.exportExampleTask = null;
            }
            
            view.titleField.value = node.NodeModel.Note.Title;
            view.exportType.value = node.NodeModel.Note.ExportFileType.ToString();
            view.useGlobalConfig.value = true;
            
            SetConfluenceConfig(
                node.GUI.Config.confluence.url,
                node.GUI.Config.confluence.username,
                node.GUI.Config.confluence.apiToken,
                node.GUI.Config.confluence.spaceKey);
                
            SetSlackConfig(
                node.GUI.Config.slack.webhookUrl,
                node.GUI.Config.slack.channel,
                node.GUI.Config.slack.username,
                node.GUI.Config.slack.iconUrl);
            
            confluence.Load();
            slack.Load();
            BindInputFieldByExportType();
            node.NodeModel.Header.SummarizeDescription = node.GetDescription();
        }
    }
}