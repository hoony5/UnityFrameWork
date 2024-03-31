using System.Threading.Tasks;

namespace GPT
{
    public interface IAudio
    {
        Task<object> CreateSpeech(Prompt prompt, Voice voice, AudioResponseFormat responseFormat = AudioResponseFormat.wav, float speed = 1.0f);
        Task<Transcription> CreateTranscription(Prompt prompt, Files files, ScriptResponseFormat responseFormat = ScriptResponseFormat.json);
        Task<string>CreateTranslation(Prompt prompt, Files files, ScriptResponseFormat responseFormat = ScriptResponseFormat.json);
    }
}