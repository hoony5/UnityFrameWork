using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;

namespace GPT
{
    public class ModerationUsecase : UsecaseBase, IModeration
    {
        public ModerationUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<Moderation> CreateModeration(Prompt prompt)
        {
            string url = GPTURL.Moderation.postCreateModerationURL;
                
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetInput(prompt.CreateFullPrompt())
                .BuildCreateModerationPayload()
                .ToHttpContent();
                
            return await PostAsync(url, content)
                .ContinueWith(response 
                    => JsonConvert.DeserializeObject<Moderation>(response.Result));
        }
    }
}