using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Unity.Plastic.Newtonsoft.Json;
using static GPT.GPTURL;

namespace GPT
{
    public class FineTuningUsecase : UsecaseBase, IFineTuning
    {
        public FineTuningUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<FineTuningJob> CreateFineTuningJob(Prompt prompt, string jobName, string trainingFileID, int batchSize, int learningRateMultiplier, int nEpochs, string validationFileID = "")
        {
            string url = FineTune.postCreateFineTuningJobURL;

            CreateFineTuningHyperparametersPayload hyperparametersPayload = BasicPayloadBuilder
                .NewBuilder()
                .CreateFineTuningHyperparametersPayload(batchSize, learningRateMultiplier, nEpochs);
            
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetTrainingFile(trainingFileID)
                .SetValidationFile(validationFileID)
                .SetHyperparameters(hyperparametersPayload)
                .SetSuffix(jobName)
                .BuildCreateFineTuningJobPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<FineTuningJob>(response);
        }

        public async Task<IEnumerable<FineTuningJob>> ListFineTuningJobs(Prompt prompt)
        {
             string url = FineTune.getListFineTuningJobsURL;
             
             string response = await GetAsync(url);
             return JsonConvert.DeserializeObject<IEnumerable<FineTuningJob>>(response);
        }

        public async Task<IEnumerable<FineTuningJobEvent>> ListFineTuningEvents(Prompt prompt)
        {
            string url = FineTune.getListFineTuningJobEventsURL(prompt.FineTuningJobID);
            
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<FineTuningJobEvent>>(response);
        }

        public async Task<FineTuningJob> RetrieveFineTuningJob(Prompt prompt)
        {
            string url = FineTune.getRetrieveFineTuningJobURL(prompt.FineTuningJobID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<FineTuningJob>(response);
        }

        public async Task<FineTuningJob> CancelFineTuningJob(Prompt prompt)
        {
            string url = FineTune.postCancelFineTuningJobURL(prompt.FineTuningJobID);
            string response = await PostAsync(url, null);
            return JsonConvert.DeserializeObject<FineTuningJob>(response);
        }
    }
}