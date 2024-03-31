using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IImage
    {
        Task<IEnumerable<Image>> CreateImage(Prompt prompt, bool isHD = true, ImageStyle style = ImageStyle.natural, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url);
        Task<IEnumerable<Image>> CreateImageEdit(Prompt prompt, Files image, Files mask = default, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url);
        Task<IEnumerable<Image>> CreateImageVariation(Prompt prompt,  Files image, string size = "1024x1024", ImageResponseFormat responseFormat = ImageResponseFormat.Url);
    }
}