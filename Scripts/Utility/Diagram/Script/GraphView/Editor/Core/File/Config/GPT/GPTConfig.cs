using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GPT;
using Share;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Diagram
{
    public class GPTConfig : ScriptableObject
    {
        public readonly string historySavePath = "Assets/Scripts/Utility/Diagram/GPT/History";

        public string apiKey;
        public string model; 
        [Range(0f,1f), Tooltip("more lower, more accuracy and more higher ,more creative response.")] public float temperature = 0.7f;
        [Range(64, 4096)]public int maxTokens = 100;
        [Range(64, 4096)]public int retryTokenStep = 250;
        [Range(0f,1f)] public float topP = 0.7f;
        [Range(-2f,2f)] public float frequencyPenalty = 0f;
        [Range(-2f,2f)] public float presencePenalty = 0;
        [Range(1, 10)] public int n = 1;
         public Queue<DialogueModel> responses = new Queue<DialogueModel>();
        public string roleName = "programmer professional assistant.";
        [TextArea] public string role = "You are a helpful Unity and professional programmer assistant.";
        public IsoLanguage language = IsoLanguage.ko;
        [TextArea]public string prompt;
        [FormerlySerializedAs("promptConfig")] public GPTPromptConfig promptContent;
        public (string content, int maxToken) summary;

        public void SaveHistory()
        {
            if(!Directory.Exists(historySavePath))
                Directory.CreateDirectory(historySavePath);

            List<DialogueModel> target;
            target = promptContent.meaningfulHistoryIndices.Count != 0 
                ? promptContent.meaningfulHistoryIndices.Select(index => promptContent.history[index]).ToList() 
                : promptContent.history;
            
            string rawPath = EditorUtility.SaveFilePanel("Save history", "Assets/", "", "asset");
            string savePath = rawPath.Replace(Application.dataPath, "Assets");

            if (string.IsNullOrEmpty(savePath)) return;

            GPTHistory asset = ScriptableObject.CreateInstance(typeof(GPTHistory)) as GPTHistory;
            if(asset == null) return;
            
            asset.title = Path.GetFileNameWithoutExtension(savePath);
            asset.history = new List<DialogueModel>(target);
            asset.importance = "order by number. i.e 1";
            asset.summary = summary.content;
            asset.validationMaxToken = summary.maxToken;
            asset.attachments = promptContent.attachments;
            
            AssetDatabase.CreateAsset(asset, savePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            summary = ("" , 0);
        }

        public void Load()
        {
            string rawPath = EditorUtility.OpenFilePanel("Load history", historySavePath, "asset");
            string loadPath = rawPath.Replace(Application.dataPath, "Assets");
            if (string.IsNullOrEmpty(loadPath)) return;
            GPTHistory asset = AssetDatabase.LoadAssetAtPath<GPTHistory>(loadPath);
            
            if(asset == null) return;
            summary = (asset.summary, asset.validationMaxToken);
            promptContent.history = new List<DialogueModel>(asset.history);
        }
    }
}