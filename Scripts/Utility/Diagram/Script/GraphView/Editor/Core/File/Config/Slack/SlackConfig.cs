using UnityEngine;

namespace Diagram
{
    public class SlackConfig : ScriptableObject
    {
        public string webhookUrl = "https://hooks.slack.com/services/your-webhook";
        public string channel = "your-channel";
        public string username = "your-username";
        public string iconUrl = "https://www.flaticon.com/svg/static/icons/svg/3523/3523063.svg"; // or string i.e -> ":ghost:"
        public string blockType = "section";
        public string textType = "mrkdwn";
    }
}