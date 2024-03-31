using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardSlackBinder : IDisposable
    {
        private DiagramConfig config;
        private BlackboardNoteNodeView view;
        public DiagramNoteNode node;
        
        public BlackboardSlackBinder(BlackboardNoteNodeView view, DiagramConfig config)
        {
            this.view = view;
            this.config = config;
        }
        private void SetSlackHook()
        {
            string url = "https://my.slack.com/services/new/incoming-webhook/";
            Application.OpenURL(url);
        }

        private void BindSlackLink(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackWebhookUrl = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.slack.webhookUrl = evt.newValue;
        }
        private void BindSlackChannel(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackChannel = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.slack.channel = evt.newValue;
        }
        
        private void BindSlackUserName(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackUserName = evt.newValue.IsNullOrEmptyThen("Diagram");
            if (!view.useGlobalConfig.value) return;
            config.slack.username = evt.newValue;
        }
        
        private void BindSlackIconUrl(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackIconUrl = evt.newValue;
            if (!view.useGlobalConfig.value) return;
            config.slack.iconUrl = evt.newValue;
        }
        private void OpenIconURL()
        {
            string url = "https://www.webfx.com/tools/emoji-cheat-sheet/";
            Application.OpenURL(url);
        }
        
        private async Task SendMessageAsync(string title, string message)
        {
            // test
            config.slack.webhookUrl = node.NodeModel.Blackboard.SlackWebhookUrl;

            string _webhookUrl = config.slack.webhookUrl;

            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    text = title,
                    channel = config.slack.channel,
                    username = config.slack.username,
                    icon_emoji = config.slack.iconUrl,
                    // link = "<https://alert-system.com/alerts/1234|여기를 클릭>",
                    blocks = new object[]
                    {
                        new
                        {
                            type = config.slack.blockType,
                            text = new
                            {
                                type = config.slack.textType,
                                text = $"{GetSlackBasicFormat()}\n\n{message}"
                            }
                        }
                    }
                };
                string jsonPayload = JsonConvert.SerializeObject(payload);
                using (StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = await client.PostAsync(_webhookUrl, content);
                    Debug.Log($"Slack Hook Response StatusCode : {response.StatusCode}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to send message to Slack. Status code: {response.StatusCode}");
                    }
                }
            }
        }

        public async Task SendMessageToSlack()
        {
            if (node.NodeModel.Note.ExportFileType != ExportFileType.Slack) return;
            string title = node.NodeModel.Note.Title;
            string message = node.NodeModel.Blackboard.ExportExampleNote;
            await SendMessageAsync(title, message);
        }
        
        private void BindSlackEpic(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackEpicText = evt.newValue;
        }
        private void BindSlackStory(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackStoryText = evt.newValue;
        }
        private void BindSlackIssue(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackIssueText = evt.newValue;
        }
        private void BindSlackMessage(ChangeEvent<string> evt)
        {
            node.NodeModel.Blackboard.SlackMessageText = evt.newValue;
        }

        private string GetSlackBasicFormat()
        {
            string epic = node.NodeModel.Blackboard.SlackEpicText.IsNullOrEmptyThen("Epic");
            string story = node.NodeModel.Blackboard.SlackStoryText.IsNullOrEmptyThen("Story");
            string issue = node.NodeModel.Blackboard.SlackIssueText.IsNullOrEmptyThen("Issue");
            string message = node.NodeModel.Blackboard.SlackMessageText.IsNullOrEmptyThen("Message");

            return $@"• {epic}
• Story : {story}
• Issue : {issue}
• Message : {message}
";
        }
        
        

        public void Load()
        {
            view.slack.slackLinkField.value = node.NodeModel.Blackboard.SlackWebhookUrl;
            view.slack.slackChannelField.value = node.NodeModel.Blackboard.SlackChannel;
            view.slack.slackUserNameField.value = node.NodeModel.Blackboard.SlackUserName.IsNullOrEmptyThen("Diagram");
            view.slack.slackIconURLField.value = node.NodeModel.Blackboard.SlackIconUrl;
        }
        public void Bind()
        {
            view.slack.slackLinkButton.clicked += SetSlackHook;
            view.slack.slackLinkField.RegisterValueChangedCallback(BindSlackLink);
            view.slack.slackChannelField.RegisterValueChangedCallback(BindSlackChannel);
            view.slack.slackUserNameField.RegisterValueChangedCallback(BindSlackUserName);
            view.slack.slackIconURLField.RegisterValueChangedCallback(BindSlackIconUrl);
            view.slack.slackIconURLButton.clicked += OpenIconURL;
            view.slack.slackEpicField.RegisterValueChangedCallback(BindSlackEpic);
            view.slack.slackStoryField.RegisterValueChangedCallback(BindSlackStory);
            view.slack.slackIssueField.RegisterValueChangedCallback(BindSlackIssue);
            view.slack.slackMessageField.RegisterValueChangedCallback(BindSlackMessage);
        }
        public void Dispose()
        {
            view.slack.slackLinkButton.clicked -= SetSlackHook;
            view.slack.slackLinkField.UnregisterValueChangedCallback(BindSlackLink);
            view.slack.slackChannelField.UnregisterValueChangedCallback(BindSlackChannel);
            view.slack.slackUserNameField.UnregisterValueChangedCallback(BindSlackUserName);
            view.slack.slackIconURLField.UnregisterValueChangedCallback(BindSlackIconUrl);
            view.slack.slackIconURLButton.clicked -= OpenIconURL;
            view.slack.slackEpicField.UnregisterValueChangedCallback(BindSlackEpic);
            view.slack.slackStoryField.UnregisterValueChangedCallback(BindSlackStory);
            view.slack.slackIssueField.UnregisterValueChangedCallback(BindSlackIssue);
            view.slack.slackMessageField.UnregisterValueChangedCallback(BindSlackMessage);
        }
    }

}