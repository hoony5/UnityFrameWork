using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;

namespace GPT
{
    public class PromptBuilder
    {
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private string instruction;
            private string language;
            private string role;
            private string tone;
            private string reward;
            private string penalty;
            private string content;
            private string assistantID;
            private string threadID;
            private string messageID;
            private string runID;
            private List<string> fileIDs;
            private string stepID;
            private string fineTuningJobID;
            private int maxTokens;
            private SerializedDictionary<string, string> metadata;
            private List<(string name, string description, ParameterPayload parameters)> tools;

            public Builder()
            {
                metadata = new SerializedDictionary<string, string>();
                fileIDs = new List<string>();
            }
            

            public Builder SetInstruction(string instruction)
            {
                this.instruction = instruction;
                return this;
            }

            public Builder SetLanguage(string language)
            {
                this.language = language;
                return this;
            }

            public Builder SetRole(string role)
            {
                this.role = role;
                return this;
            }
            
            public Builder SetFineTuningJobID(string fineTuningJobID)
            {
                this.fineTuningJobID = fineTuningJobID;
                return this;
            }

            public Builder SetTone(string tone)
            {
                this.tone = tone;
                return this;
            }

            public Builder SetReward(string reward)
            {
                this.reward = reward;
                return this;
            }

            public Builder SetPenalty(string penalty)
            {
                this.penalty = penalty;
                return this;
            }

            public Builder SetContent(string content)
            {
                this.content = content;
                return this;
            }
            
            public Builder SetStepID(string stepID)
            {
                this.stepID = stepID;
                return this;
            }

            public Builder SetMaxTokens(int maxTokens)
            {
                this.maxTokens = maxTokens;
                return this;
            }
            
            public Builder SetFileIDs(params string[] info)
            {
                this.fileIDs = info.ToList();
                return this;
            }
            
            public Builder SetMetadata(SerializedDictionary<string, string> metadata)
            {
                this.metadata = metadata;
                return this;
            }
            
            public Builder SetMetaData(params (string key, string value)[] metadata)
            {
                if(metadata.Length > 16) throw new System.Exception("metadata length should be less than 16");
                
                foreach (var (key, value) in metadata)
                {
                    this.metadata.Add(key, value);
                }
                return this;
            }
            
            public Builder SetTools(List<(string name, string description, ParameterPayload parameters)> tools)
            {
                this.tools = tools;
                return this;
            }
            
            public Builder SetTools(params (string name, string description, ParameterPayload parameters)[] tools)
            {
                if(tools.Length == 0) throw new System.Exception("tools should not be empty");
                foreach (var (name, description, parameters) in tools)
                {
                    this.tools.Add((name, description, parameters));
                }
                return this;
            }
            
            public Builder SetAssistantID(string assistantID)
            {
                this.assistantID = assistantID;
                return this;
            }
            
            public Builder SetThreadID(string threadID)
            {
                this.threadID = threadID;
                return this;
            }
            
            public Builder SetMessageID(string messageID)
            {
                this.messageID = messageID;
                return this;
            }
            
            public Builder SetRunID(string runID)
            {
                this.runID = runID;
                return this;
            }

            public Prompt Build()
            {
                return new Prompt
                {
                    Instruction = instruction,
                    Language = language,
                    Role = role,
                    Tone = tone,
                    Reward = reward,
                    Penalty = penalty,
                    Content = content,
                    MaxTokens = maxTokens,
                    FileIDs = fileIDs,
                    Metadata = metadata,
                    Tools = tools,
                    AssistantID = assistantID,
                    ThreadID = threadID,
                    MessageID = messageID,
                    RunID = runID,
                    StepID = stepID,
                    FineTuningJobID = fineTuningJobID
                };
            }

        }
    }

}