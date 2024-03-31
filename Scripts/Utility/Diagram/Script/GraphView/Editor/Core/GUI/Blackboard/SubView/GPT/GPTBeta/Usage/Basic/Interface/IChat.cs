using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IChat
    {
        Task<dynamic> CreateChatCompletion(Prompt prompt, GPTResponseMode mode = GPTResponseMode.Streaming, params CreateChatMessagePayload[] messages);
    }
}