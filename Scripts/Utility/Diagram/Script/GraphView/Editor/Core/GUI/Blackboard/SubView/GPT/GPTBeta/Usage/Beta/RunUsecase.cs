using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEngine;

namespace GPT
{
    public class RunUsecase : UsecaseBase, IRuns
    {
        public RunUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<Run> CreateRun(Prompt prompt, string additionalInstruction = "")
        {
            string url = GPTURL.ThreadURL.postCreateRunURL(prompt.ThreadID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetAssistantID(prompt.AssistantID)
                .SetModel(config.gpt.model)
                .SetInstruction(prompt.CreateSystemInstruction())
                .SetAdditionalInstruction(additionalInstruction)
                .SetMetadata(prompt.Metadata)
                .SetCodeInterpreterTool()
                .SetRetrievalTool()
                .SetFunctionTool(prompt.Tools?.ToArray())
                .BuildCreateRunPlayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Run>(response);
        }

        private ThreadPayload CreateThreadPayload(Prompt prompt, params MessagePayload[] messages)
        {
            ThreadPayload thread = BetaPayloadBuilder
                .NewBuilder()
                .SetMessages(messages)
                .SetFileIDs(prompt.FileIDs)
                .SetMetadata()
                .BuildThreadPayload();
            
            return thread;
        }
        
        public async Task<Run> CreateThreadAndRun(Prompt prompt, params MessagePayload[] messages)
        {
            string url = GPTURL.ThreadURL.postCreateThreadAndRunURL;

            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetAssistantID(prompt.AssistantID)
                .SetThread(CreateThreadPayload(prompt, messages))
                .SetModel(config.gpt.model)
                .SetInstruction(prompt.CreateSystemInstruction())
                .SetCodeInterpreterTool()
                .SetRetrievalTool()
                .SetFunctionTool(prompt.Tools?.ToArray())
                .SetMetadata(prompt.Metadata)
                .BuildCreateThreadAndRunPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            Debug.Log(response);
            return JsonConvert.DeserializeObject<Run>(response);
        }

        public async Task<IEnumerable<Run>> ListRuns(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getListRunsURL(prompt.ThreadID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Run>>(response);
        }

        public async Task<IEnumerable<RunStep>> ListRunSteps(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getListRunStepsURL(prompt.ThreadID, prompt.RunID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<RunStep>>(response);
        }

        public async Task<Run> RetrieveRun(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getRetrieveRunURL(prompt.ThreadID, prompt.RunID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<Run>(response);
        }

        public async Task<RunStep> RetrieveRunStep(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.getRetrieveRunStepsURL(prompt.ThreadID, prompt.RunID, prompt.StepID);
            string response = await GetAsync(url);
            return JsonConvert.DeserializeObject<RunStep>(response);
        }

        public async Task<Run> ModifyRun(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.postModifyRunURL(prompt.ThreadID, prompt.RunID);
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetMetadata(prompt.Metadata)
                .BuildModifyRunPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Run>(response);
        }

        public async Task<Run> SubmitToolOutputToRun(Prompt prompt, string toolOutputID = "" ,string output = "")
        {
            string url = GPTURL.ThreadURL.postSubmitToolOutputToRunURL(prompt.ThreadID, prompt.RunID);
            
            ToolOutputPayload toolOutputs = BetaPayloadBuilder
                .NewBuilder()
                .SetToolCallId(toolOutputID)
                .SetOutput(output)
                .BuildToolOutputPayload();
            
            HttpContent content = BetaPayloadBuilder
                .NewBuilder()
                .SetToolsOutputs(toolOutputs)
                .BuildSubmitToolOutputsToRunPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Run>(response);
        }

        public async Task<Run> CancelRun(Prompt prompt)
        {
            string url = GPTURL.ThreadURL.postCancelRunURL(prompt.ThreadID, prompt.RunID);
            string response = await PostAsync(url, null);
            return JsonConvert.DeserializeObject<Run>(response);
        }
    }
}