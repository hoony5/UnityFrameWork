namespace GPT
{
    public static class PromptExtension
    {
        public static string CreateSystemInstruction(this Prompt prompt)
        {
            return $@"Instruction : ""{prompt.Instruction}"",
Reward : ""{prompt.Reward}."",
Penalty : ""{prompt.Penalty}."",
Language : ""your language is {prompt.Language}."",
Tone : ""{prompt.Tone}."",
MaxToken : ""{(prompt.MaxTokens > 0 ? $"within {prompt.MaxTokens} Tokens." : "")}."",
Dialogue History : ""here is history until now {prompt.History}.""";
        }
        public static string CreateFullPrompt(this Prompt prompt)
        {
            return $@"Instruction : ""{prompt.Instruction}."",
Reward : ""{prompt.Reward}."",
Penalty : ""{prompt.Penalty}."",
Language : ""your language is {prompt.Language}."",
Tone : ""{prompt.Tone}."",
MaxToken : ""{(prompt.MaxTokens > 0 ? $"within {prompt.MaxTokens} Tokens." : "")}."",
Dialogue History : ""here is history until now {prompt.History}"".";
        }
    }
}