using System.Threading.Tasks;

namespace GPT
{
    public interface IThreads
    {
        Task<Thread> CreateThread(Prompt prompt, params MessagePayload[] messages);
        Task<Thread> RetrieveThread(Prompt prompt);
        Task<Thread> ModifyThread(Prompt prompt);
        Task<string> DeleteThread(Prompt prompt);
    }
}