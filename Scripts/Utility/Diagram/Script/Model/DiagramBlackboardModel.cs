using UnityEngine;

namespace Diagram
{
    [System.Serializable]
    public class DiagramBlackboardModel
    {
        private DiagramStatusModel status;
        [SerializeField] private string note;
        [SerializeField] private string exportExampleCode;
        [SerializeField] private string exportExampleNote;
        [SerializeField] private string exportPath;
        [SerializeField] private string exportType;
        // Slack
        [SerializeField] private string slackSlackWebhookUrl;
        [SerializeField] private string slackChannel;
        [SerializeField] private string slackUserName;
        [SerializeField] private string slackIconUrl;
        [SerializeField] private string slackEpicText;
        [SerializeField] private string slackStoryText;
        [SerializeField] private string slackIssueText;
        [SerializeField] private string slackMessageText;
        // Confluence
        [SerializeField] private string confluenceBaseUrl;
        [SerializeField] private string confluenceUserName;
        [SerializeField] private string confluenceAPIToken;
        [SerializeField] private string confluenceSpaceKey;
        [SerializeField] private string pageTitle;
        [SerializeField] private string pageID;
        [SerializeField] private string pageVersion;
        
        public string Note
        {
            get => note;
            set
            {
                note = value;
                status.IsDirty = true;
            }
        }
        public string ExportExampleNote
        {
            get => exportExampleNote;
            set
            {
                exportExampleNote = value;
                status.IsDirty = true;
            }
        }
        public string ExportExampleCode
        {
            get => exportExampleCode;
            set
            {
                exportExampleCode = value;
                status.IsDirty = true;
            }
        }
        
        public string ExportPath
        {
            get => exportPath;
            set
            {
                exportPath = value;
                status.IsDirty = true;
            }
        }
        
        public string ExportType
        {
            get => exportType;
            set
            {
                exportType = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackWebhookUrl
        {
            get => slackSlackWebhookUrl;
            set
            {
                slackSlackWebhookUrl = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackChannel
        {
            get => slackChannel;
            set
            {
                slackChannel = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackUserName
        {
            get => slackUserName;
            set
            {
                slackUserName = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackIconUrl
        {
            get => slackIconUrl;
            set
            {
                slackIconUrl = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackEpicText
        {
            get => slackEpicText;
            set
            {
                slackEpicText = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackStoryText
        {
            get => slackStoryText;
            set
            {
                slackStoryText = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackIssueText
        {
            get => slackIssueText;
            set
            {
                slackIssueText = value;
                status.IsDirty = true;
            }
        }
        
        public string SlackMessageText
        {
            get => slackMessageText;
            set
            {
                slackMessageText = value;
                status.IsDirty = true;
            }
        }
        
        public string ConfluenceBaseUrl
        {
            get => confluenceBaseUrl;
            set
            {
                confluenceBaseUrl = value;
                status.IsDirty = true;
            }
        }

        
        public string ConfluenceUserName
        {
            get => confluenceUserName;
            set
            {
                confluenceUserName = value;
                status.IsDirty = true;
            }
        }
        public string ConfluenceAPIToken
        {
            get => confluenceAPIToken;
            set
            {
                confluenceAPIToken = value;
                status.IsDirty = true;
            }
        }

        
        public string ConfluenceSpaceKey
        {
            get => confluenceSpaceKey;
            set
            {
                confluenceSpaceKey = value;
                status.IsDirty = true;
            }
        }
        
        public string PageTitle
        {
            get => pageTitle;
            set
            {
                pageTitle = value;
                status.IsDirty = true;
            }
        }
        
        public string PageID
        {
            get => pageID;
            set
            {
                pageID = value;
                status.IsDirty = true;
            }
        }
        
        public string PageVersion
        {
            get => pageVersion;
            set
            {
                pageVersion = value;
                status.IsDirty = true;
            }
        }

        public DiagramBlackboardModel(DiagramStatusModel status)
        {
            this.status = new DiagramStatusModel(status);
            note = string.Empty;
            exportPath = string.Empty;
            exportType = string.Empty;
            exportExampleCode = string.Empty;
            exportExampleNote = string.Empty;
            slackSlackWebhookUrl = string.Empty;
            slackChannel = string.Empty;
            slackUserName = string.Empty;
            slackIconUrl = string.Empty;
            slackEpicText = string.Empty;
            slackStoryText = string.Empty;
            slackIssueText = string.Empty;
            slackMessageText = string.Empty;
            confluenceBaseUrl = string.Empty;
            confluenceUserName = string.Empty;
            confluenceAPIToken = string.Empty;
            confluenceSpaceKey = string.Empty;
            pageTitle = string.Empty;
            pageID = string.Empty;
            pageVersion = string.Empty;
        }
        public DiagramBlackboardModel(DiagramBlackboardModel nodeModel)
        {
            status = nodeModel.status;
            note = nodeModel.note;
            exportPath = nodeModel.exportPath;
            exportType = nodeModel.exportType;
            exportExampleCode = nodeModel.exportExampleCode;
            exportExampleNote = nodeModel.exportExampleNote;
            slackSlackWebhookUrl = nodeModel.slackSlackWebhookUrl;
            slackChannel = nodeModel.slackChannel;
            slackUserName = nodeModel.slackUserName;
            slackIconUrl = nodeModel.slackIconUrl;
            slackEpicText = nodeModel.slackEpicText;
            slackStoryText = nodeModel.slackStoryText;
            slackIssueText = nodeModel.slackIssueText;
            slackMessageText = nodeModel.slackMessageText;
            confluenceBaseUrl = nodeModel.confluenceBaseUrl;
            confluenceUserName = nodeModel.confluenceUserName;
            confluenceAPIToken = nodeModel.confluenceAPIToken;
            confluenceSpaceKey = nodeModel.confluenceSpaceKey;
            pageTitle = nodeModel.pageTitle;
            pageID = nodeModel.pageID;
            pageVersion = nodeModel.pageVersion;
        }
    }
}