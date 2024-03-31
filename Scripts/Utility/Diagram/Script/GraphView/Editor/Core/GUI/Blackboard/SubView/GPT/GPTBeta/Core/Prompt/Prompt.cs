using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

namespace GPT
{
    public class Prompt
    {
        public string Instruction {get;set;}    
        public string Language {get;set;}   
        public string Role {get;set;}   
        public string Tone {get;set;}   
        public string History {get;set;}   
        public string Reward {get;set;} 
        public string Penalty {get;set;}    
        /// <summary>
        /// user input
        /// </summary>
        public string Content {get;set;}    
        public int MaxTokens {get;set;}    
        public string AssistantID {get;set;}
        public string ThreadID {get;set;}
        public string MessageID {get;set;}
        public string RunID {get;set;}
        public string StepID {get;set;}
        public List<string> FileIDs {get;set;}
        public string FineTuningJobID {get;set;}
        public SerializedDictionary<string, string> Metadata {get; set;}
        public List<(string name, string description, ParameterPayload parameters)> Tools {get;set;}
        
        public Prompt()
        {
            Metadata = new SerializedDictionary<string, string>();
            FileIDs = new List<string>();
            Tools = new List<(string name, string description, ParameterPayload parameters)>();
        }
    }

}