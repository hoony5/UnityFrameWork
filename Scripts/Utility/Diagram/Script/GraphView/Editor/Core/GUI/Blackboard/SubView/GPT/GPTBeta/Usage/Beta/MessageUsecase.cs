using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEngine;

namespace GPT
{
    public class MessageUsecase : UsecaseBase, IMessages
    {
        public MessageUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<Message> CreateMessage(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.postCreateMessageURL(prompt.ThreadID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetRole("user")
                .SetContent(prompt.Content)
                .SetFileIDs(prompt.FileIDs)
                .SetMetadata(prompt.Metadata)
                .BuildCreateMessagePayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Message>(response);
        }

        public async Task<dynamic> ListMessages(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getListMessagesURL(prompt.ThreadID);
            string response = await GetAsync(url);
            Debug.Log(response);
            return JsonConvert.DeserializeObject<dynamic>(response);
        }

        public async Task<IEnumerable<MessageFile>> ListMessageFiles(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getListMessageFilesURL(prompt.ThreadID, prompt.MessageID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<MessageFile>>(response);
        }

        public async Task<Message> RetrieveMessage(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getRetrieveMessageURL(prompt.ThreadID, prompt.MessageID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Message>(response);
        }

        public async Task<MessageFile> RetrieveMessageFile(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getRetrieveMessageFileURL(prompt.ThreadID, prompt.MessageID, prompt.FileIDs[0]);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<MessageFile>(response);
        }

        public async Task<Message> ModifyMessage(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.postModifyMessageURL(prompt.ThreadID, prompt.MessageID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetMetadata(prompt.Metadata)
                .BuildModifyMessagePayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Message>(response);
        }
    }
}