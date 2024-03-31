using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

namespace GPT
{
    public class BetaPayloadBuilder
    {
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private string model;
            private string name;
            private string type;
            private string description;
            private string instruction;
            private string additional_instruction;
            private object fileIDs;
            private string assistantID;
            private string toolCallId;
            private object output;
            private string role;
            private string content;
            private SerializedDictionary<string, string> metadata;
            private List<object> tools;
            private List<MessagePayload> messages;
            private ThreadPayload thread;
            private ToolOutputPayload toolsOutputs;

            public Builder()
            {
                tools = new List<object>();
                messages = new List<MessagePayload>();
                metadata = new SerializedDictionary<string, string>();
            }

            public Builder SetModel(string model)
            {
                this.model = model;
                return this;
            }

            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }

            public Builder SetDescription(string description)
            {
                this.description = description;
                return this;
            }

            public Builder SetInstruction(string instruction)
            {
                this.instruction = instruction;
                return this;
            }
            
            public Builder SetAdditionalInstruction(string additionalInstruction)
            {
                this.additional_instruction = additionalInstruction;
                return this;
            }

            public Builder SetFileIDs(List<string> fileIDs)
            {
                this.fileIDs = fileIDs;
                return this;
            }

            public Builder SetMetadata(object metadata)
            {
                this.metadata = metadata as SerializedDictionary<string, string>;
                return this;
            }
            
            public Builder SetMetadata(params (string key, string value)[] metadatas)
            {
                if(metadatas.Length > 16) throw new Exception("Metadata can only have 16 key-value pairs");
                
                foreach (var (key, value) in metadatas)
                {
                    this.metadata.Add(key, value);
                }
                return this;
            }
            
            public Builder SetToolCallId(string toolCallId)
            {
                this.toolCallId = toolCallId;
                return this;
            }

            public Builder SetAssistantID(string assistantID)
            {
                this.assistantID = assistantID;
                return this;
            }

            public Builder SetCodeInterpreterTool()
            {
                this.tools.Add(new CodeInterpreterToolPayload() { Type = "code_interpreter" });
                return this;
            }
            public Builder SetRetrievalTool()
            {
                this.tools.Add(new RetrievalToolPayload() { Type = "retrieval" });
                return this;
            }
            public Builder SetFunctionTool(params (string name, string description, ParameterPayload parameters)[] functions)
            {
                if (functions.Length == 0) return this;
                
                foreach ((string name, string description, ParameterPayload parameters) in functions)
                {
                    tools.Add(new FunctionToolPayload
                    {
                        Type = "function",
                        Function = new FunctionPayload
                        {
                            Name = name,
                            Description = description,
                            Parameters = parameters
                        }
                    });
                }
                return this;
            }

            public Builder SetRole(string role)
            {
                this.role = role;
                return this;
            }

            public Builder SetContent(string content)
            {
                this.content = content;
                return this;
            }
            public Builder SetThread(ThreadPayload thread)
            {
                this.thread = thread;
                return this;
            }
            
            public Builder SetMessages(IEnumerable<MessagePayload> messages)
            {
                this.messages = new List<MessagePayload>(messages);
                return this;
            }

            public Builder SetToolsOutputs(ToolOutputPayload toolsOutputs)
            {
                this.toolsOutputs = toolsOutputs;
                return this;
            }
            
            public Builder SetOutput(object output)
            {
                this.output = output;
                return this;
            }
            
            public ThreadPayload BuildThreadPayload()
            {
                return new ThreadPayload
                {
                    Messages = new List<MessagePayload>(messages).ToArray(),
                    MetaData = metadata
                };
            }

            public ToolOutputPayload BuildToolOutputPayload()
            {
                return new ToolOutputPayload
                {
                    ToolCallId = toolCallId,
                    Output = output
                };
            }
            
            public MessagePayload BuildMessagePayload()
            {
                return new MessagePayload
                {
                    Role = role,
                    Content = content,
                    FileIds = fileIDs,
                    MetaData = metadata
                };
            }

            public CreateAssistantPayload BuildCreateAssistantPayload()
            {
                return new CreateAssistantPayload
                {
                    Name = name,
                    Description = description,
                    Instructions = instruction,
                    Tools = tools,
                    FileIds = fileIDs,
                    MetaData = metadata
                };
            }
            
            public CreateAssistantFilePayload BuildCreateAssistantFilePayload()
            {
                return new CreateAssistantFilePayload
                {
                    FileID = fileIDs.ToString()
                };
            }
            
            public CreateMessagePayload BuildCreateMessagePayload()
            {
                return new CreateMessagePayload
                {
                    Role = role,
                    Content = content,
                    FileIds = fileIDs,
                    MetaData = metadata
                };
            }
            
            public CreateThreadOptionalPayload BuildThreadOptionalPayload()
            {
                return new CreateThreadOptionalPayload
                {
                    Messages = messages,
                    MetaData = metadata
                };
            }
            
            public CreateRunPayload BuildCreateRunPlayload()
            {
                return new CreateRunPayload
                {
                    AssistantID = assistantID,
                    Model = model,
                    Instructions = instruction,
                    AdditionalInstructions = additional_instruction,
                    Tools = tools,
                    MetaData = metadata
                };
            }
            
            public CreateThreadAndRunPayload BuildCreateThreadAndRunPayload()
            {
                return new CreateThreadAndRunPayload
                {
                    AssistantID = assistantID,
                    Model = model,
                    Thread = thread,
                    Instructions = instruction,
                    Tools = tools,
                    MetaData = metadata
                };
            }
            
            public ModifyAssistantPayload BuildModifyAssistantPayload()
            {
                return new ModifyAssistantPayload
                {
                    Name = name,
                    Description = description,
                    Instructions = instruction,
                    Tools = tools,
                    FileIds = fileIDs,
                    MetaData = metadata
                };
            }
            
            public ModifyMessagePayload BuildModifyMessagePayload()
            {
                return new ModifyMessagePayload
                {
                    MetaData = metadata
                };
            }
            
            public ModifyRunPayload BuildModifyRunPayload()
            {
                return new ModifyRunPayload
                {
                    MetaData = metadata
                };
            }
            
            public ModifyThreadPayload BuildModifyThreadPayload()
            {
                return new ModifyThreadPayload
                {
                    MetaData = metadata
                };
            }
            
            public SubmitToolOutputsToRunPayload BuildSubmitToolOutputsToRunPayload()
            {
                return new SubmitToolOutputsToRunPayload
                {
                    ToolOutputs = toolsOutputs
                };
            }
        }
    }

}