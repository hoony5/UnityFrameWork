using System.Collections.Generic;
using System.Text;
using AYellowpaper.SerializedCollections;
using Share.Enums;

namespace Share
{
    // TODO:: refactor to use the builder pattern
    public class TextFormatWriter
    {
        private StringBuilder stringBuilder;
        public string spliter;
        private int index;
        private int indent;
        
        private string mainIndentSignature = @"├─";
        private string subIndentSignature = @"└─";

        private SerializedDictionary<int, List<string>> csvRows;
        
        public TextFormatWriter()
        {
            index = 1;
            stringBuilder = new StringBuilder();
            spliter = "-";
            
            csvRows = new SerializedDictionary<int, List<string>>();
        }
        
        public void Reset()
        {
            index = 1;
            indent = 0;
            stringBuilder?.Clear();
        }

        private void UpdateToNextIndex()
        {
            index++;
        }

        private string GetIndentLevelSpace()
        {
            string space = string.Empty;
            for (int i = 0; i < indent; i++)
            {
                space += "\t\t";
            }
            return space;
        }
        private string GetIndentSignature()
        {
            return indent switch
            {
                0 => mainIndentSignature,
                _ => $"{GetIndentLevelSpace()}{subIndentSignature}"
            };
        }
        
        private string GetBulletSignature()
        {
            return indent switch
            {
                0 => "•",
                _ => $"{GetIndentLevelSpace()}•"
            };
        }

        public void Append(string header, string value, WriterTextStyle writerTextStyle)
        {
            stringBuilder.Append(Write(header, value, writerTextStyle));
        }
        public void AppendLine(string header, string value, WriterTextStyle writerTextStyle)
        {
            stringBuilder.AppendLine(Write(header, value, writerTextStyle));
        }
        public void Append(string input, WriterTextStyle writerTextStyle)
        {
            stringBuilder.Append(Write(input, writerTextStyle));
        }
        public void AppendLine(string input, WriterTextStyle writerTextStyle)
        {
            stringBuilder.AppendLine(Write(input, writerTextStyle));
        }
        public void AppendLine(string input)
        {
            stringBuilder.AppendLine(input);
        }
        public void Append(string input)
        {
            stringBuilder.Append(input);
        }
        public void AppendLine()
        {
            stringBuilder.AppendLine();
        }
        public void Clear()
        {
            stringBuilder.Clear();
        }
        public string Submit()
        {
            string result = stringBuilder.ToString();
            stringBuilder.Clear();
            return result;
        }

        private bool IsBool(string input)
        {
            return bool.TryParse(input, out _);
        }
        
        private bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }
        
        private bool IsIndent(string input)
        {
            // the input contains a tab at the beginning
            return input.StartsWith("\t");
        }
        
        private bool IsBullet(string input)
        {
            // the input contains a bullet at the beginning
            return input.StartsWith("•");
        }

        private string ToMarkDown(string input)
        {
            if (input.IsNullOrEmpty())
                return string.Empty;

            if (IsBool(input))
            {
                // checkBox
                return $"- [{(input.Equals("true") ? "x" : " ")}] ";
            }
            
            if (IsNumber(input))
            {
                // number
                return $"{index}. ";
            }
            
            if (IsIndent(input))
            {
                // indent
                return $"{GetIndentLevelSpace()}";
            }
            
            if (IsBullet(input))
            {
                // bullet
                return $"{GetIndentLevelSpace()}{input} ";
            }
            
            return string.Empty;
        }

        private void AppendCSV(int rowIndex, params string[] values)
        {
            if(rowIndex < 0) return;
            if(values.Length == 0) return;
            
            if(csvRows.ContainsKey(rowIndex))
                csvRows[rowIndex].AddRange(values);
            else
                csvRows.Add(rowIndex, new List<string> (values));
        }

        public string Write(string input, WriterTextStyle writerTextStyle)
        {
            if(input.IsNullOrEmpty())
                return string.Empty;
            
            if (writerTextStyle is WriterTextStyle.Number)
                UpdateToNextIndex();
            
            return writerTextStyle switch
            {
                WriterTextStyle.Default => $"{input}",
                WriterTextStyle.Indent => $"{GetIndentSignature()}   {input}",
                WriterTextStyle.Bullet => $"{GetBulletSignature()} {input}",
                WriterTextStyle.Number => $"{index}. {input}",
                WriterTextStyle.Split => string.Join(spliter, input),
                WriterTextStyle.MarkDown => ToMarkDown(input),
                WriterTextStyle.CSV => $"{input},",
                _ => input
            };
        }

        // title, subTitle, description
        public string Write(string title, string subTitle, string description, WriterTextStyle writerTextStyle)
        {
            if(title.IsNullOrEmpty() && 
               subTitle.IsNullOrEmpty() && 
               description.IsNullOrEmpty())
                return string.Empty;

            if(writerTextStyle is WriterTextStyle.CSV)
            {
                if(!csvRows.ContainsKey(0))
                    AppendCSV(0, "A", "B", "C");
                
                AppendCSV(index, title, subTitle, description);
                UpdateToNextIndex();
            }
            
            if (writerTextStyle is WriterTextStyle.Number)
                UpdateToNextIndex();
            
            return writerTextStyle switch
            {
                WriterTextStyle.Default => $@"Title : {title}
SubTitle : {subTitle}
Description : {description}",
                WriterTextStyle.Split => $"{title} {spliter} {subTitle} {spliter} {description}",
                WriterTextStyle.Indent => $@"├─   {title} 
├─  {subTitle}
│
└─  {description}",
                WriterTextStyle.Bullet => $"• {title} ({subTitle}) : {description}",
                WriterTextStyle.Number => $"{index}. {title} ({subTitle}) : {description}",
                WriterTextStyle.MarkDown => $@"## {title}
#### {subTitle}
---
{description}",
                _ => $@"{title}
{subTitle}
{description}"
            };
        }
        // pair 
        public string Write(string header, string value, WriterTextStyle writerTextStyle)
        {
            if(header.IsNullOrEmpty() && 
               value.IsNullOrEmpty())
                return string.Empty;

            if (writerTextStyle is WriterTextStyle.CSV)
            {
                if(!csvRows.ContainsKey(0))
                    AppendCSV(0, "A", "B");
                
                AppendCSV(index, header, value);
                UpdateToNextIndex();
            }
            
            if (writerTextStyle is WriterTextStyle.Number)
            {
                UpdateToNextIndex();
            }
            
            return writerTextStyle switch
            {
                WriterTextStyle.Default => $"{header} : {value}",
                WriterTextStyle.Indent => $@"├─   {header} 
└─  {value}",
                WriterTextStyle.Bullet => $@"• {header}
    {value}",
                WriterTextStyle.Number => $"{index}. {header} : {value}",
                WriterTextStyle.Split => $"{header} {spliter} {value}",
                WriterTextStyle.MarkDown => $@"#### {header}
---
    - {value}",
                _ => null
            };
        }
        
        // checkList
        public string Write(string header, bool value, WriterTextStyle writerTextStyle)
        {
            if(header.IsNullOrEmpty())
                return string.Empty;

            if (writerTextStyle is WriterTextStyle.CSV)
            {
                if(!csvRows.ContainsKey(0))
                    AppendCSV(0, "A", "B");
                
                AppendCSV(index, header, value.ToString());
                UpdateToNextIndex();
            }
            
            if (writerTextStyle is WriterTextStyle.Number)
                UpdateToNextIndex();

            return writerTextStyle switch
            {
                WriterTextStyle.Default => $"{header} : {value}",
                WriterTextStyle.Indent => $@"├─   {header} 
└─  {value}",
                WriterTextStyle.Bullet => $@"• {header}
    {value}",
                WriterTextStyle.Number => $"{index}. {header} : {value}",
                WriterTextStyle.Split => $"{header} {spliter} {value}",
                WriterTextStyle.MarkDown => $@"{ToMarkDown(value.ToString())} : {header}",
                _ => null
            };
        }

        public void Setup()
        {
            index = 1;
            csvRows.Clear();
        }
        public string ToCSVFormat()
        {
            if(csvRows.Count == 0) return string.Empty;
            
            stringBuilder.Clear();
            
            foreach (KeyValuePair<int, List<string>> row in csvRows)
            {
                stringBuilder.AppendLine(string.Join(",", row.Value));
            }
            
            index = 1;
            return stringBuilder.ToString();
        }
    }
}