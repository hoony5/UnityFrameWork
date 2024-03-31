using System.Threading.Tasks;

namespace GPT
{
    public interface IEmbedding
    {
        Task<Embedding> CreateEmbeddings(Prompt prompt, EncodingFormat encodingFormat = EncodingFormat.Base64);
    }
}