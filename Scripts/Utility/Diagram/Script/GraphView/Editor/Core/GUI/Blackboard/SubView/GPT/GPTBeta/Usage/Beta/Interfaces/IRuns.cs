using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IRuns
    {
        Task<Run> CreateRun(Prompt prompt, string additionalInstruction = "");
        Task<Run> CreateThreadAndRun(Prompt prompt, params MessagePayload[] messages);
        Task<IEnumerable<Run>> ListRuns(Prompt prompt);
        Task<IEnumerable<RunStep>> ListRunSteps(Prompt prompt);
        Task<Run> RetrieveRun(Prompt prompt);
        Task<RunStep> RetrieveRunStep(Prompt prompt);
        Task<Run> ModifyRun(Prompt prompt);
        Task<Run> SubmitToolOutputToRun(Prompt prompt , string toolOutputID = "" ,string output = "");
        Task<Run> CancelRun(Prompt prompt);
    }
}