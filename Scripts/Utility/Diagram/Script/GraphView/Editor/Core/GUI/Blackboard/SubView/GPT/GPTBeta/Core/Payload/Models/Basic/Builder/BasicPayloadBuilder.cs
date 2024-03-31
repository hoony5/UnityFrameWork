using System.Collections.Generic;
using UnityEngine;

namespace GPT
{
    public class BasicPayloadBuilder
    {

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private string model;
            private object input;
            private List<CreateChatMessagePayload> messages;
            private Voice voice;
            private AudioResponseFormat audioResponseFormat;
            private int speed;
            private float seed;
            private object file;
            private string language; // iso 639-1 code
            private string prompt;
            private ScriptResponseFormat scriptResponseFormat;
            private float temperature;
            private int maxTokens;
            private string content;
            private string role;
            private dynamic n;
            private dynamic logitBias;
            private string name;
            private float frequencyPenalty;
            private float presencePenalty;
            private float topP;
            private string stop;
            private bool stream;
            private List<object> tools;
            private string user;
            private EncodingFormat encodingFormat;
            private int dimensions;
            private string fileId;
            private object hyperparameters;
            private string suffix;
            private Purpose purpose;
            private string validation_file; // fine-tune , jsonl
            private string quality; // hd - dall-e-3
            private ImageResponseFormat imageResponseFormat;
            private string size; // resolution i.e 1920x1080
            private ImageStyle style;
            private object mask;
            private string training_file;

            public Builder()
            {
                messages = new List<CreateChatMessagePayload>();
                tools = new List<object>();
            }


            public Builder SetModel(string model)
            {
                this.model = model;
                return this;
            }
            
            public Builder SetInput(object input)
            {
                this.input = input;
                return this;
            }
            
            public Builder SetVoice(Voice voice)
            {
                this.voice = voice;
                return this;
            }
            
            public Builder SetAudioResponseFormat(AudioResponseFormat audioResponseFormat)
            {
                this.audioResponseFormat = audioResponseFormat;
                return this;
            }
            
            public Builder SetSeed(float seed)
            {
                this.seed = seed;
                return this;
            }
            
            public Builder SetFile(object file)
            {
                this.file = file;
                return this;
            }
            
            public Builder SetLanguage(string language)
            {
                this.language = language;
                return this;
            }
            
            public Builder SetPrompt(string prompt)
            {
                this.prompt = prompt;
                return this;
            }
            
            public Builder SetScriptResponseFormat(ScriptResponseFormat scriptResponseFormat)
            {
                this.scriptResponseFormat = scriptResponseFormat;
                return this;
            }
            
            public Builder SetTemperature(float temperature)
            {
                this.temperature = temperature;
                return this;
            }
            
            public Builder SetMaxTokens(int maxTokens)
            {
                this.maxTokens = maxTokens;
                return this;
            }
            
            public Builder SetContent(string content)
            {
                this.content = content;
                return this;
            }
            
            public Builder SetRole(string role)
            {
                this.role = role;
                return this;
            }
            
            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }
            
            public Builder SetFrequencyPenalty(float frequencyPenalty)
            {
                this.frequencyPenalty = frequencyPenalty;
                return this;
            }
            
            public Builder SetPresencePenalty(float presencePenalty)
            {
                this.presencePenalty = presencePenalty;
                return this;
            }
            
            public Builder SetTopP(float topP)
            {
                this.topP = topP;
                return this;
            }
            
            public Builder SetStop(string stop)
            {
                this.stop = stop;
                return this;
            }
            
            public Builder SetStream(bool stream)
            {
                this.stream = stream;
                return this;
            }
            public Builder SetFunctionTool(params (string name, string description, ParameterPayload parameters)[] functions)
            {
                if(functions == null) return this;
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
            
            public Builder SetUser(string user)
            {
                this.user = user;
                return this; 
            }

            public Builder SetN(int gptN)
            {
                this.n = gptN;
                return this;
            }
            
            public Builder SetLogitBias(dynamic logitBias)
            {
                this.logitBias = logitBias;
                return this;
            }
            
            public Builder SetEncodingFormat(EncodingFormat encodingFormat)
            {
                this.encodingFormat = encodingFormat;
                return this;
            }
            
            public Builder SetDimension(int dimension)
            {
                this.dimensions = dimension;
                return this;
            }
            
            public Builder SetFileId(string fileId)
            {
                this.fileId = fileId;
                return this;
            }
            
            public Builder SetHyperparameters(object hyperparameters)
            {
                this.hyperparameters = hyperparameters;
                return this;
            }
                
            public Builder SetSuffix(string suffix)
            {
                this.suffix = suffix;
                return this;
            }
            
            public Builder SetPurpose(Purpose purpose)
            {
                this.purpose = purpose;
                return this;
            }
            
            public Builder SetValidationFile(string validation_file)
            {
                this.validation_file = validation_file;
                return this;
            }
            
            public Builder SetQuality(string quality)
            {
                this.quality = quality;
                return this;
            }
            
            public Builder SetImageResponseFormat(ImageResponseFormat imageResponseFormat)
            {
                this.imageResponseFormat = imageResponseFormat;
                return this;
            }
            
            public Builder SetSize(string size)
            {
                this.size = size;
                return this;
            }
            
            public Builder SetStyle(ImageStyle style)
            {
                this.style = style;
                return this;
            }
            
            public Builder SetMask(object mask)
            {
                this.mask = mask;
                return this;
            }
            public Builder SetSpeed(int speed)
            {
                this.speed = speed;
                return this;
            }
            
            public Builder SetTrainingFile(string trainingFileID)
            {
                this.training_file = trainingFileID;
                return this;
            }
            public Builder SetMessage(CreateChatMessagePayload[] messages)
            {
                this.messages.AddRange(messages);
                return this;
            }
            
            public Builder SetMessage(CreateChatMessagePayload message)
            {
                this.messages.Add(message);
                return this;
            }
            
            public CreateFineTuningHyperparametersPayload CreateFineTuningHyperparametersPayload(int batchSize, double learningRateMultiplier, int nEpochs)
            {
                return new CreateFineTuningHyperparametersPayload
                {
                    BatchSize = batchSize,
                    LearningRateMultiplier = learningRateMultiplier,
                    NEpochs = nEpochs
                };
            }
            
            public CreateChatMessagePayload BuildCreateChatMessagePayload()
            {
                return new CreateChatMessagePayload
                {
                    Content = content,
                    Role = role,
                    Name = name
                };
            }

            public CreateSpeechPayload BuildCreateSpeechPayload()
            {
                return new CreateSpeechPayload
                {
                    Model = model,
                    Input = (string)input,
                    Voice = voice.ToString(),
                    ResponseFormat = audioResponseFormat.ToString(),
                    Speed = speed
                };
            }
            public CreateTranslationPayload BuildCreateTranslationPayload()
            {
                return new CreateTranslationPayload
                {
                    Model = model,
                    Files = (Files) file,
                    Prompt = prompt,
                    ResponseFormat = scriptResponseFormat.ToString(),
                    Temperature = temperature
                };
            }
            
            public CreateTranscriptionPayload BuildCreateTranscriptionPayload()
            {
                return new CreateTranscriptionPayload
                {
                    Model = model,
                    Files = (Files) file,
                    Language = language,
                    Prompt = prompt,
                    ResponseFormat = scriptResponseFormat.ToString(),
                    Temperature = temperature
                };
            }
            
            public CreateChatCompletionPayload BuildCreateChatCompletionPayload()
            {
                return new CreateChatCompletionPayload
                {
                    Model = model,
                    Messages = messages,
                    FrequencyPenalty = frequencyPenalty,
                    PresencePenalty = presencePenalty,
                    Stop = stop,
                    Temperature = temperature,
                    MaxTokens = maxTokens,
                    Stream = stream,
                    Seed = (int) seed,
                    TopP = topP,
                    Tools = tools.Count == 0 ? null : tools,
                    User = user
                };
            }
            
            public CreateEmbeddingsPayload BuildCreateEmbeddingsPayload()
            {
                return new CreateEmbeddingsPayload
                {
                    Model = model,
                    Input = (string) input,
                    EncodingFormat = encodingFormat.ToString().ToLower(),
                    Dimensions = dimensions,
                    User = user
                };
            }
            
            public CreateFineTuningJobPayload BuildCreateFineTuningJobPayload()
            {
                return new CreateFineTuningJobPayload
                {
                    Model = model,
                    Suffix = suffix,
                    ValidationFile = validation_file,
                    HyperParameters = hyperparameters,
                    TrainingFile = training_file
                };
            }
            
            public UploadFilePayload BuildUploadFilePayload()
            {
                return new UploadFilePayload
                {
                    Files = (Files)file,
                    Purpose = purpose.ToString()
                };
            }
            
            public CreateImagePayload BuildCreateImagePayload()
            {
                return new CreateImagePayload
                {
                    Model = model,
                    Quality = quality,
                    ResponseFormat = imageResponseFormat.ToString().ToLower(),
                    Size = size,
                    Style = style.ToString(),
                    User = user,
                    Prompt = prompt
                };
            }
            
            public CreateImageEditPayload BuildCreateImageEditPayload()
            {
                return new CreateImageEditPayload
                {
                    Model = model,
                    User = user,
                    Mask = (Files)mask,
                    Prompt = prompt,
                    Image = (Files)file,
                    ResponseFormat = imageResponseFormat.ToString().ToLower(),
                    Size = size
                };
            }
            
            public CreateImageVariationPayload BuildCreateImageVariationPayload()
            {
                return new CreateImageVariationPayload
                {
                    Model = model,
                    User = user,
                    Image = (Files)file,
                    ResponseFormat = imageResponseFormat.ToString().ToLower(),
                    Size = size,
                };
            }
            
            public CreateModerationPayload BuildCreateModerationPayload()
            {
                return new CreateModerationPayload
                {
                    Model = model,
                    Input = (string)input,
                };
            }
        }
    }

}