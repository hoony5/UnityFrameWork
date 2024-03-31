using System.Threading.Tasks;

namespace GPT
{
    public interface IModeration
    {
        Task<Moderation> CreateModeration(Prompt prompt);
    }
}