using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using UnityEditor;

namespace GPT
{
    public class ImageUsecase : UsecaseBase, IImage
    {
        public ImageUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<IEnumerable<Image>> CreateImage(Prompt prompt, bool isHD = true, ImageStyle style = ImageStyle.natural, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url)
        {
            string url = GPTURL.Image.postCreateImageURL;
            
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel("dall-e-3") // or "dall-e-2"
                .SetQuality(isHD ? "hd" : "")
                .SetImageResponseFormat(responseFormat)
                .SetSize(size)
                .SetStyle(style)
                .SetN(config.gpt.n)
                .SetUser("Unity-user")
                .SetPrompt(prompt.CreateFullPrompt())
                .BuildCreateImagePayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<IEnumerable<Image>>(response);
        }

        public async Task<IEnumerable<Image>> CreateImageEdit(Prompt prompt, Files image, Files mask = default, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url)
        {
            string url = GPTURL.Image.postCreateImageEditURL;
            
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel("dall-e-3") // or "dall-e-2"
                .SetImageResponseFormat(responseFormat)
                .SetN(config.gpt.n)
                .SetSize(size)
                .SetUser("Unity-user")
                .SetPrompt(prompt.CreateFullPrompt())
                .SetFile(image)
                .SetMask(mask)
                .BuildCreateImageEditPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<IEnumerable<Image>>(response);
        }

        public async Task<IEnumerable<Image>> CreateImageVariation(Prompt prompt, Files image, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url)
        {
            string url = GPTURL.Image.postCreateImageVariationURL;
            
            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel("dall-e-3") // or "dall-e-2"
                .SetImageResponseFormat(responseFormat)
                .SetN(config.gpt.n)
                .SetUser("Unity-user")
                .SetFile(image)
                .SetSize(size)
                .BuildCreateImageVariationPayload()
                .ToHttpContent();
            
            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<IEnumerable<Image>>(response);
        }
        
    }
}