using System;
using System.IO;
using UnityEngine;

namespace Writer.CommandConsole
{
    /*
     *    When user quit the game, flag true "isNormalQuit".
     *    But if the game crash, flag false "isNormalQuit".
     *
     *    Normal case how to check :
     *      - User quit the game on UI
     *      - User quit the game by Alt + F4      
     */
    
    public class CommandConsoleExporter
    {
        private static string folder = "View/Console/Log";
        private static string exportRootPath = Path.Combine(Application.dataPath, folder);

        private static string GetTime()
        {
            DateTime time = DateTime.Now;
            return time.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        private static string GetExportCaseName(ConsoleLogExportCase @case)
        {
            switch (@case)
            {
                default:
                case ConsoleLogExportCase.Default:
                    return string.Empty;
                case ConsoleLogExportCase.Abnormal:
                    return "Abnormal";
                case ConsoleLogExportCase.Error:
                    return "Error";
                case ConsoleLogExportCase.Warning:
                    return "Warning";
                case ConsoleLogExportCase.Info:
                    return "Info";
            }
        }
        
        public static async void Export(string content, ConsoleLogExportCase exportCase)
        {
            string fileName = $"[{GetExportCaseName(exportCase)}]_{GetTime()}.txt";
            string filePath = Path.Combine(exportRootPath, fileName);
            
            if (!Directory.Exists(exportRootPath))
            {
                Directory.CreateDirectory(exportRootPath);
            }
            
            await File.WriteAllTextAsync(filePath, content);
        }
    }

    class CommandConsoleExporterImpl : CommandConsoleExporter
    {
    }
}