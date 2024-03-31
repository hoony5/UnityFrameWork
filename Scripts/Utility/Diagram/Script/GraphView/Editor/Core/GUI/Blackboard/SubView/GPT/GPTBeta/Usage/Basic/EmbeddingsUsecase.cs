using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEditor;

namespace GPT
{
    public class EmbeddingsUsecase : UsecaseBase, IEmbedding
    {
        public EmbeddingsUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<Embedding> CreateEmbeddings(Prompt prompt,
            EncodingFormat encodingFormat = EncodingFormat.Base64)
        {
            string url = GPTURL.Embedding.postCreateEmbeddingURL;

            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetInput(prompt.CreateFullPrompt())
                // .SetDimension() // not implemented
                .SetEncodingFormat(encodingFormat)
                .SetUser("Unity-user")
                .BuildCreateEmbeddingsPayload()
                .ToHttpContent();

            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Embedding>(response);
        }
    }
}