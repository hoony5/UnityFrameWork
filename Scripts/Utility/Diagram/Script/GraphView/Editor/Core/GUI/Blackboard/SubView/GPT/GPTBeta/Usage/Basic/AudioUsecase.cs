using System.Net.Http;
using System.Threading.Tasks;
using Diagram.Config;
using Newtonsoft.Json;
using static GPT.GPTURL;

namespace GPT
{
    public class AudioUsecase : UsecaseBase, IAudio
    {
        public AudioUsecase(DiagramConfig config) : base(config)
        {
        }

        public async Task<object> CreateSpeech(Prompt prompt, Voice voice,
            AudioResponseFormat responseFormat = AudioResponseFormat.wav, float speed = 1.0f)
        {
            if (speed > 4) speed = 4;
            if (speed < 0.25f) speed = 0.25f;

            string url = Audio.postCreateSpeechURL;

            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetInput(prompt.CreateFullPrompt())
                .SetVoice(voice)
                .SetAudioResponseFormat(responseFormat)
                .SetSeed(speed)
                .BuildCreateSpeechPayload()
                .ToHttpContent();

            string response = await PostAsync(url, content);
            return response;
        }
        public async Task<Transcription> CreateTranscription(Prompt prompt, Files files,
            ScriptResponseFormat responseFormat = ScriptResponseFormat.json)
        {
            // after upload file and then call this function so, parameter file is there response from upload file
            string url = Audio.postCreateTranscriptionURL;

            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel(config.gpt.model)
                .SetFile(files)
                .SetLanguage(config.gpt.language.ToString())
                .SetPrompt(prompt.CreateFullPrompt())
                .SetScriptResponseFormat(responseFormat)
                .SetTemperature(config.gpt.temperature)
                .BuildCreateTranscriptionPayload()
                .ToHttpContent();

            string response = await PostAsync(url, content);
            return JsonConvert.DeserializeObject<Transcription>(response);
        }

        public async Task<string> CreateTranslation(Prompt prompt, Files files,
            ScriptResponseFormat responseFormat = ScriptResponseFormat.json)
        {
            string url = Audio.postCreateTranslationURL;

            HttpContent content = BasicPayloadBuilder
                .NewBuilder()
                .SetModel("whisper-1") // only support this model
                .SetFile(files)
                .SetPrompt(prompt.CreateFullPrompt())
                .SetScriptResponseFormat(responseFormat)
                .SetTemperature(config.gpt.temperature)
                .BuildCreateTranslationPayload()
                .ToHttpContent();

            string response = await PostAsync(url, content);
            return response;
        }
    }
}