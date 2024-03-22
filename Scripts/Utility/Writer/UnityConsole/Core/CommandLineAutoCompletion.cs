using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;

namespace Writer.CommandConsole
{
    public class CommandLineAutoCompletion
    {
        private static SerializedDictionary<string, List<CommandFormatInfo>> commandFormats 
            = new SerializedDictionary<string, List<CommandFormatInfo>>();
        private static List<string> autoCompletionOptions = new List<string>();
        public static void AddCommand(string tag, CommandFormatInfo formatInfo)
        {
            if (!commandFormats.ContainsKey(tag))
            {
                commandFormats.Add(tag, new List<CommandFormatInfo>());
            }
            if (commandFormats[tag].Contains(formatInfo)) return;
            
            commandFormats[tag].Add(formatInfo);
        }

        public static void Clear()
        {
            commandFormats.Clear();
        }
        
        public static string[] GetAutoCompletionOptions(string input)
        {
            autoCompletionOptions.Clear();

            // before input tag

            if (input.Length > 0 && !input.Contains('.'))
            {
                IEnumerable<string> matchesValues = 
                    commandFormats
                        .Where(item => item.Key.StartsWith(input)) // find matched tag
                        .SelectMany(x => x.Value.Select(y => y.commandFormat)); // get all command formats
                 
                autoCompletionOptions.AddRange(matchesValues);
                return autoCompletionOptions.ToArray();
            }
            
            string[] parts = input.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string tag = parts.Length > 0 ? parts[0] : "";
            
            // after input tag
            if (!string.IsNullOrEmpty(tag) && commandFormats.TryGetValue(tag, out List<CommandFormatInfo> commandFormatList))
            {
                foreach (CommandFormatInfo format in commandFormatList)
                {
                    bool matchesCommandFormat = format.commandFormat.StartsWith(input);

                    // if fit to simple format -> test.abc.
                    if (input.EndsWith(".") && !input.Contains("(") && matchesCommandFormat)
                    {
                        // if contains 'test.abc.' , then add command formats
                        if (autoCompletionOptions.Contains(format.commandFormat)) continue;
                        autoCompletionOptions.Add(format.commandFormat);
                    }
                    // -> test.abc.(
                    else if (input.Contains("("))
                    {
                        string inputWithoutParams = input.Split('(')[0] + "("; // 'test.abc.(' 

                        if (!format.commandFormat.StartsWith(inputWithoutParams)) continue;
                        // -> 'test.abc.(abd,'
                        if (autoCompletionOptions.Contains(format.commandFormat)) continue;
                        autoCompletionOptions.Add(format.commandFormat);
                    }
                    else if (matchesCommandFormat)
                    {
                        // common case
                        if (autoCompletionOptions.Contains(format.commandFormat)) continue;
                        autoCompletionOptions.Add(format.commandFormat);
                    }
                    
                    // if is matched, pass all command formats
                    if (input != format.commandFormat) continue;
                    
                    autoCompletionOptions.Clear();
                    return autoCompletionOptions.ToArray();
                }
            }
            return autoCompletionOptions.Distinct().ToArray();
        }
        
        
    }
}