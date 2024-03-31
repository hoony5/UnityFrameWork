using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace GPT
{
    public class ChatUsecase : UsecaseBase, IChat
    {
        public ChatUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<dynamic> CreateChatCompletion(Prompt prompt, GPTResponseMode mode,
            params CreateChatMessagePayload[] messages)
        {
            string url = GPTURL.Chat.postCreateChatCompletionURL;

            // system, user, assistant, tool
            CreateChatMessagePayload systemMessage = BasicPayloadBuilder
                .NewBuilder()
                .SetContent(prompt.CreateSystemInstruction())
                .SetRole("system")
                .BuildCreateChatMessagePayload();

            messages = messages.Length == 0
                ? new CreateChatMessagePayload[] { systemMessage }
                : messages.Prepend(systemMessage).ToArray();
            bool onStreaming = mode == GPTResponseMode.Streaming;
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetMessage(messages) 
                .SetFrequencyPenalty(config.gpt.frequencyPenalty)
                .SetPresencePenalty(config.gpt.presencePenalty)
                .SetTemperature(config.gpt.temperature)
                .SetTopP(config.gpt.topP)
                .SetMaxTokens(config.gpt.maxTokens)
                .SetN(config.gpt.n)
                .SetStream(onStreaming)
                .SetFunctionTool(prompt.Tools.ToArray())
                .SetUser("Unity-user")
                .BuildCreateChatCompletionPayload()
                .ToHttpContent();
            
            Debug.Log(content.ReadAsStringAsync().Result);

            string response = await PostAsync(url, content);

            if (onStreaming)
            {
                string processed = string.Join(",",
                    response
                        .Replace("data:", "")
                        .Replace("[DONE]", "")
                        .Split("\n", StringSplitOptions.RemoveEmptyEntries));
            
                response = $"[{processed}]";   
            }
            Debug.Log($"response : {response}");
            return JsonConvert.DeserializeObject<dynamic>(response, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }); 
        }
    }
}