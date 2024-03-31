using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPT
{
    public interface IModel
    {
        Task<IEnumerable<Model>> ListModels(Prompt prompt);
        Task<Model> RetrieveModel(Prompt prompt);
        Task<string> DeleteFineTunedModel(Prompt prompt);
    }
}