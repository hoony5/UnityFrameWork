using System.Threading.Tasks;

namespace Writer.Core
{
    public interface IWriter
    {
        Task WriteScript(string path, string content);
    }
}