using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;

namespace GPT
{
    public class FileUsecase : UsecaseBase, IFile
    {
        public FileUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<File> UploadFile(string path, string purpose = "assistants")
        {
            string url = GPTURL.File.postUploadFileURL;
            
            MultipartFormDataContent content = new MultipartFormDataContent
            {
                { new StringContent(purpose), "purpose" },
                { new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(path)), "file", Path.GetFileName(path) }
            };
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<File>(response);
        }

        public async Task<Files> ListFiles()
        {
            string url = GPTURL.File.getListFilesURL;
            
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Files>(response);
        }

        public async Task<Files> RetrieveFile(Prompt prompt)
        {
            string url = GPTURL.File.getRetrieveFileURL(prompt.FileIDs[0]);
            
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Files>(response);
        }

        public async Task<string> DeleteFile(Prompt prompt)
        {
            if (prompt.FileIDs == null) return "No file to delete";
            if(prompt.FileIDs.Count == 0) return "No file to delete";
            
            
            List<Task<string>> tasks = new List<Task<string>>();
                
            foreach (string fileID in prompt.FileIDs)
            {
                string url = GPTURL.File.deleteFileURL(fileID);
                tasks.Add(DeleteAsync(url));
            }
                
            return await Task.WhenAll(tasks)
                .ContinueWith(responses 
                    => string.Join(",\n", responses));
        }

        public Task<string> RetrieveFileContent(Prompt prompt)
        {
            string url = GPTURL.File.getRetrieveFileContentURL(prompt.FileIDs[0]);
            return GetAsync(url);
        }
    }
}