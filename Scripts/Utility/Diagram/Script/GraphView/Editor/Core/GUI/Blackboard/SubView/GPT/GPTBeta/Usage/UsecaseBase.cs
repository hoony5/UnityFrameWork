using Diagram.Config;

namespace GPT
{
    public abstract class UsecaseBase : GPTHttpBase
    {
        protected DiagramConfig config;
        protected UsecaseBase(DiagramConfig config) : base(config.gpt.apiKey)
        {
            this.config = config;
        }
    }
}