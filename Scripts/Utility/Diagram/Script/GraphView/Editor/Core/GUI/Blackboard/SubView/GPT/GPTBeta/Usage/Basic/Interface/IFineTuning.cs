using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IFineTuning
    {
        Task<FineTuningJob> CreateFineTuningJob(Prompt prompt, string jobName, string trainingFileID, int batchSize, int learningRateMultiplier, int nEpochs, string validationFileID = "");
        Task<IEnumerable<FineTuningJob>> ListFineTuningJobs(Prompt prompt);
        Task<IEnumerable<FineTuningJobEvent>> ListFineTuningEvents(Prompt prompt);
        Task<FineTuningJob> RetrieveFineTuningJob(Prompt prompt);
        Task<FineTuningJob> CancelFineTuningJob(Prompt prompt);
    }
}