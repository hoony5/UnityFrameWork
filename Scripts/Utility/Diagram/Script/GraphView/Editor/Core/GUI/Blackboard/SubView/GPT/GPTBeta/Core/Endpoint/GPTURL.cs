namespace GPT
{
    public static class GPTURL
    {
        private static readonly string baseURL = "https://api.openai.com/v1";
        
        // Basic
        public static class Audio
        {
            private static readonly string audioBaseURL = $"{baseURL}/audio";
            public static readonly string postCreateSpeechURL = $"{audioBaseURL}/speech";
            public static readonly string postCreateTranscriptionURL = $"{audioBaseURL}/transcriptions";
            public static readonly string postCreateTranslationURL = $"{audioBaseURL}/translations";
        }
        
        public static class Chat
        {
            public static readonly string postCreateChatCompletionURL = $"{baseURL}/chat/completions";
        }
        
        public static class Embedding
        {
            public static readonly string postCreateEmbeddingURL = $"{baseURL}/embeddings";
        }
        
        public static class FineTune
        {
            private static readonly string fineTuneBaseURL = $"{baseURL}/fine_tuning";
            public static readonly string postCreateFineTuningJobURL = $"{fineTuneBaseURL}/jobs";
            public static readonly string getListFineTuningJobsURL = postCreateFineTuningJobURL;
            public static string getListFineTuningJobEventsURL(string fineTuningJobID) => $"{postCreateFineTuningJobURL}/{fineTuningJobID}/events";
            public static string getRetrieveFineTuningJobURL(string fineTuningJobID) => $"{postCreateFineTuningJobURL}/{fineTuningJobID}";
            public static string postCancelFineTuningJobURL(string fineTuningJobID) => $"{getRetrieveFineTuningJobURL(fineTuningJobID)}/cancel";
        }
        
        public static class File
        {
            private static readonly string filesBaseURL = $"{baseURL}/files";
            public static readonly string postUploadFileURL = filesBaseURL;
            public static readonly string getListFilesURL = postUploadFileURL;
            public static string getRetrieveFileURL(string fileID) => $"{filesBaseURL}/{fileID}";
            public static string getRetrieveFileContentURL(string fileID) => $"{getRetrieveFileURL(fileID)}/content";
            public static string deleteFileURL(string fileID) => getRetrieveFileURL(fileID);
        }

        public static class Image
        {
            private static readonly string imageBaseURL = $"{baseURL}/images";
            public static readonly string postCreateImageURL = $"{imageBaseURL}/generations";
            public static readonly string postCreateImageEditURL = $"{imageBaseURL}/edits";
            public static readonly string postCreateImageVariationURL = $"{imageBaseURL}/variations";
        }
        
        public static class Model
        {
            private static readonly string modelBaseURL = $"{baseURL}/models";
            public static readonly string getListModelsURL = modelBaseURL;
            public static string getRetrieveModelURL(string model) => $"{modelBaseURL}/{model}";
            public static string deleteModelURL(string model) => getRetrieveModelURL(model);
        }
        public static class Moderation
        {
            public static readonly string postCreateModerationURL = $"{baseURL}/moderation";
        }
        // Beta
        public static class AssistantURL
        {
            public static readonly string postCreateAssistantURL = $"{baseURL}/assistants";
            public static string postCreateAssistantFileURL(string assistantID) => $"{postCreateAssistantURL}/{assistantID}/files";
            public static readonly string getListAssistantsURL = postCreateAssistantURL;
            public static string getListAssistantFilesURL(string assistantID) => postCreateAssistantFileURL(assistantID);
            public static string getRetrieveAssistantURL(string assistantID) => $"{postCreateAssistantURL}/{assistantID}";
            public static string getRetrieveAssistantFileURL(string assistantID, string fileID) => $"{postCreateAssistantFileURL(assistantID)}/{fileID}";
            public static string postModifyAssistantURL(string assistantID) => getRetrieveAssistantURL(assistantID);
            public static string deleteAssistantURL(string assistantID) => getRetrieveAssistantURL(assistantID);
            public static string deleteAssistantFileURL(string assistantID, string fileID) => $"{postCreateAssistantFileURL(assistantID)}/{fileID}";
        }
        
        public static class ThreadURL
        {
            public static readonly string postCreateThreadURL = $"{baseURL}/threads";
            public static string getRetrieveThreadURL(string threadID) => $"{postCreateThreadURL}/{threadID}";
            public static string postModifyThreadURL(string threadID) => getRetrieveThreadURL(threadID);
            public static string deleteThreadURL(string threadID) => getRetrieveThreadURL(threadID);
            public static string postCreateMessageURL(string threadID) => $"{getRetrieveThreadURL(threadID)}/messages";
            public static string getListMessagesURL(string threadID) => postCreateMessageURL(threadID);
            public static string getListMessageFilesURL(string threadID, string messageID) => $"{postCreateMessageURL(threadID)}/{messageID}/files";
            public static string getRetrieveMessageURL(string threadID, string messageID) => $"{postCreateMessageURL(threadID)}/{messageID}";
            public static string getRetrieveMessageFileURL(string threadID, string messageID, string fileID) => $"{getListMessageFilesURL(threadID, messageID)}/{fileID}";
            public static string postModifyMessageURL(string threadID, string messageID) => getRetrieveMessageURL(threadID, messageID);
            public static string postCreateRunURL(string threadID) => $"{getRetrieveThreadURL(threadID)}/runs";
            public static readonly string postCreateThreadAndRunURL = $"{postCreateThreadURL}/runs";
            public static string getListRunsURL(string threadID) => postCreateRunURL(threadID);
            public static string getRetrieveRunURL(string threadID, string runID) => $"{getListRunsURL(threadID)}/{runID}";
            public static string getListRunStepsURL(string threadID, string runID) => $"{getRetrieveRunURL(threadID, runID)}/steps";
            public static string getRetrieveRunStepsURL(string threadID, string runID, string stepID) => $"{getListRunStepsURL(threadID, runID)}/{stepID}";
            public static string postModifyRunURL(string threadID, string runID) => getRetrieveRunURL(threadID, runID);
            public static string postSubmitToolOutputToRunURL(string threadID, string runID) => $"{getRetrieveRunURL(threadID, runID)}/submit_tool_outputs";
            public static string postCancelRunURL(string threadID, string runID) => $"{getRetrieveRunURL(threadID, runID)}/cancel";
        }
    }
}