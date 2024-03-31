using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEngine;

namespace GPT
{
    using static GPTURL;
    
    public class AssistantUsecase : UsecaseBase, IAssistants
    {
        public AssistantUsecase(DiagramConfig config) : base(config)
        {
            
        }
        
        public async Task<Assistant> CreateAssistant(Prompt prompt)
        {
            string url = AssistantURL.postCreateAssistantURL;
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetName(config.gpt.model)
                .SetDescription(config.gpt.role) // role
                .SetInstruction(prompt.CreateSystemInstruction())
                .SetCodeInterpreterTool()
                .SetRetrievalTool()
                .SetFunctionTool(prompt.Tools?.ToArray())
                .SetFileIDs(prompt.FileIDs)
                .SetMetadata(prompt.Metadata)
                .BuildCreateAssistantPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Assistant>(response);
        }

        public async Task<AssistantFile> CreateAssistantFile(Prompt prompt)
        {
            string url = AssistantURL.postCreateAssistantFileURL(prompt.AssistantID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetFileIDs(prompt.FileIDs)
                .BuildCreateAssistantFilePayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<AssistantFile>(response);
        }

        public async Task<IEnumerable<Assistant>> ListAssistants()
        {
            string url = AssistantURL.getListAssistantsURL;
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Assistant>>(response);
        }

        public async Task<IEnumerable<AssistantFile>> ListAssistantFiles(Prompt prompt)
        {
            string url = AssistantURL.getListAssistantFilesURL(prompt.AssistantID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<AssistantFile>>(response);
        }

        public async Task<Assistant> RetrieveAssistant(Prompt prompt)
        {
            string url = AssistantURL.getRetrieveAssistantURL(prompt.AssistantID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Assistant>(response);
        }

        public async Task<AssistantFile> RetrieveAssistantFile(Prompt prompt)
        {
            string url = AssistantURL.getRetrieveAssistantFileURL(prompt.AssistantID, prompt.FileIDs[0]);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<AssistantFile>(response);
        }

        public async Task<Assistant> ModifyAssistant(Prompt prompt)
        {
            string url = AssistantURL.postModifyAssistantURL(prompt.AssistantID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetName(config.gpt.model)
                .SetDescription(config.gpt.role) // role
                .SetInstruction(prompt.CreateSystemInstruction())
                .SetCodeInterpreterTool()
                .SetRetrievalTool()
                .SetFunctionTool(prompt.Tools?.ToArray())
                .SetFileIDs(prompt.FileIDs)
                .SetMetadata(prompt.Metadata)
                .BuildModifyAssistantPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Assistant>(response);
        }

        public async Task<string> DeleteAssistant(Prompt prompt)
        {
            string[] assistantIDs = prompt.AssistantID.Split(',');
            
            if (assistantIDs.Length == 1)
            {
                string url = AssistantURL.deleteAssistantURL(assistantIDs[0]);
                string response = await DeleteAsync(url);
                return response;
            }
            
            List<Task<string>> tasks = new List<Task<string>>();
            
            foreach (string assistantID in assistantIDs)
            {
                string url = AssistantURL.deleteAssistantURL(assistantID);
                tasks.Add(DeleteAsync(url));
            }
            
            return await Task.WhenAll(tasks)
                .ContinueWith(responses 
                    => string.Join(",\n", responses));
        }

        public async Task<string> DeleteAssistantFile(Prompt prompt)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            
            foreach (string fileID in prompt.FileIDs)
            {
                string url = AssistantURL.deleteAssistantFileURL(prompt.AssistantID, fileID);
                tasks.Add(DeleteAsync(url));
            }
            
            return await Task.WhenAll(tasks)
                .ContinueWith(responses 
                    => string.Join(",\n", responses));
        }
    }
}