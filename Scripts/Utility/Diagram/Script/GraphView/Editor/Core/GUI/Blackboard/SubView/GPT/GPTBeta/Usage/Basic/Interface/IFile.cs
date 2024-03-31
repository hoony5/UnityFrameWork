using System.Collections.Generic;
using System.IO;

namespace GPT
{
    using System.Threading.Tasks;

    public interface IFile
    {
        Task<File> UploadFile(string path, string purpose = "assistants"); // or fine-tune
        Task<Files> ListFiles();
        Task<Files> RetrieveFile(Prompt prompt);
        Task<string> DeleteFile(Prompt prompt);
        Task<string> RetrieveFileContent(Prompt prompt);
    }
}