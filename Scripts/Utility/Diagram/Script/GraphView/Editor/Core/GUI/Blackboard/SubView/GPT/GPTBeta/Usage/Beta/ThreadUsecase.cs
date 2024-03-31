using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;

namespace GPT
{
    public class ThreadUsecase : UsecaseBase, IThreads
    {
        public ThreadUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<Thread> CreateThread(Prompt prompt, params MessagePayload[] messages)
        {
            string url = GPTURL.ThreadURL.postCreateThreadURL;

            MessagePayload @base = BetaPayloadBuilder
                .NewBuilder()
                .SetRole(prompt.Role)
                .SetContent(prompt.Content)
                .SetFileIDs(prompt.FileIDs)
                .SetMetadata(prompt.Metadata)
                .BuildMessagePayload();
            
            messages = messages.Length == 0 
                ? new MessagePayload []{ @base } 
                : messages.Prepend(@base).ToArray();

            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetMessages(messages)
                .SetMetadata()
                .BuildThreadOptionalPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Thread>(response);
        }

        public async Task<Thread> RetrieveThread(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getRetrieveThreadURL(prompt.ThreadID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Thread>(response);
        }

        public async Task<Thread> ModifyThread(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.postModifyThreadURL(prompt.ThreadID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetMetadata(prompt.Metadata)
                .BuildModifyThreadPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Thread>(response);
        }

        public async Task<string> DeleteThread(Prompt prompt)
        {
            string[] threadIDs = prompt.ThreadID.Split(',');

            if (threadIDs.Length == 1)
            {
                string url = GPTURL.ThreadURL.deleteThreadURL(threadIDs[0]);
                string response = await DeleteAsync(url);
                return response;    
            }

            List<Task<string>> tasks = new List<Task<string>>();
            foreach (string threadID in threadIDs)
            {
                string url = GPTURL.ThreadURL.deleteThreadURL(threadID);
                tasks.Add(DeleteAsync(url));
            }
                 
            return await Task.WhenAll(tasks)
                .ContinueWith(responses 
                    => string.Join(",\n", responses));
        }
    }
}