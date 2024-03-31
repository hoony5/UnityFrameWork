using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IMessages
    {
        Task<Message> CreateMessage(Prompt prompt);
        Task<dynamic> ListMessages(Prompt prompt);
        Task<IEnumerable<MessageFile>> ListMessageFiles(Prompt prompt);
        Task<Message> RetrieveMessage(Prompt prompt);
        Task<MessageFile> RetrieveMessageFile(Prompt prompt);
        Task<Message> ModifyMessage(Prompt prompt);
    }
}