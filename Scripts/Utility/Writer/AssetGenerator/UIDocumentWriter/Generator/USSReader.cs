using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Writer.AssetGenerator.UIElement
{
    public class USSReader 
    {
        private const int Capacity = 64;

        public List<string> classNames = new List<string>(Capacity);
        // Test
        public async void Read(string ussPath)
        {
            using FileStream fs = new FileStream(ussPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new StreamReader(fs);
            string css = await reader.ReadToEndAsync();
        
            // .selector , must have block nested
            string pattern = @"(?<=^|\n)(?<className>\.[a-zA-Z0-9_\-]+)";
            classNames.Clear();
            MatchCollection matches = Regex.Matches(css, pattern, RegexOptions.Multiline);

            if (matches.Count == 0) return;
            foreach (Match match in matches)
            {
                string selector = match.Groups["className"].Value.TrimStart('.');
                if(!classNames.Contains(selector))
                    classNames.Add(selector);
            }
            reader.Close();
            fs.Close();
        }
    }
}