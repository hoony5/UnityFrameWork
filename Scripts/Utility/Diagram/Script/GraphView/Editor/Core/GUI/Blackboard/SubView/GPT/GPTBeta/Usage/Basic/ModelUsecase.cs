using System.Collections.Generic;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;

namespace GPT
{
    public class ModelUsecase : UsecaseBase , IModel
    {
        public ModelUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<IEnumerable<Model>> ListModels(Prompt prompt)
        {
            string url = GPTURL.Model.getListModelsURL;
                
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Model>>(response);
        }

        public async Task<Model> RetrieveModel(Prompt prompt)
        {
            string url = GPTURL.Model.getRetrieveModelURL(config.gpt.model);
                
            return await GetAsync(url)
                .ContinueWith(response 
                    => JsonConvert.DeserializeObject<Model>(response.Result));
        }

        public Task<string> DeleteFineTunedModel(Prompt prompt)
        {
            string url = GPTURL.Model.deleteModelURL(config.gpt.model);
            return DeleteAsync(url);
        }
        public Task<string> DeleteFineTunedModel(Prompt prompt, params string[] models)
        {
            if(models.Length == 1)
            {
                string url = GPTURL.Model.deleteModelURL(models[0]);
                return DeleteAsync(url);
            }
                
            List<Task<string>> tasks = new List<Task<string>>();
            foreach (string model in models)
            {
                string url = GPTURL.Model.deleteModelURL(model);
                tasks.Add(DeleteAsync(url));
            }
                
            return Task.WhenAll(tasks)
                .ContinueWith(task 
                    => string.Join(",\n", task.Result));
        }
    }
}