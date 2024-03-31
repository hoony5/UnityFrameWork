using UnityEditor;
using UnityEngine;

namespace Diagram.Config
{
    [System.Serializable]
    public class DiagramConfig : ScriptableObject
    {
        [Header("Confluence")]
        public ConfluenceConfig confluence;
        [Header("Slack")]
        public SlackConfig slack;
        [Header("GPT")]
        // model : https://platform.openai.com/docs/models/continuous-model-upgrades
        public GPTConfig gpt;

        private void OnValidate()
        {
            confluence = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Diagram/Config/Editor/ConfluenceConfig.asset", typeof(ConfluenceConfig)) as ConfluenceConfig;
            slack = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Diagram/Config/Editor/SlackConfig.asset", typeof(SlackConfig)) as SlackConfig;
            gpt = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Diagram/Config/Editor/GPTConfig.asset", typeof(GPTConfig)) as GPTConfig;
            if (!gpt) return;
            gpt.promptContent = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Diagram/Config/Editor/GPTPromptConfig.asset", typeof(GPTPromptConfig)) as GPTPromptConfig;
        }
    }
}