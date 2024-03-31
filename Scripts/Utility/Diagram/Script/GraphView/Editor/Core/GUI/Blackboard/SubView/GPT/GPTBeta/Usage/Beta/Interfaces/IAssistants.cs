using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IAssistants
    {
        Task<Assistant> CreateAssistant(Prompt prompt);
        Task<AssistantFile> CreateAssistantFile(Prompt prompt);
        Task<IEnumerable<Assistant>> ListAssistants();
        Task<IEnumerable<AssistantFile>> ListAssistantFiles(Prompt prompt);
        Task<Assistant> RetrieveAssistant(Prompt prompt);
        Task<AssistantFile> RetrieveAssistantFile(Prompt prompt);
        Task<Assistant> ModifyAssistant(Prompt prompt);
        Task<string> DeleteAssistant(Prompt prompt);
        Task<string> DeleteAssistantFile(Prompt prompt);
    }
}