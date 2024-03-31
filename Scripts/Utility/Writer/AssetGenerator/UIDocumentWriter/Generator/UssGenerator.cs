using System.Collections.Generic;

namespace Writer.AssetGenerator
{
    public class UssGenerator
    {
        private static List<string> blocks = new List<string>();
        
        public static void Clear()
        {
            blocks.Clear();
        }
        
        public static void AddBlock(string block)
        {
            blocks.Add(block);
        }
        
        public static async void ExportUssAsync(string path)
        {
            await FileExporter.BatchWriteAsync(path, blocks);
        }

        public static async void ExportUssAsync(string path, string content)
        {
            await FileExporter.Write(path, content);
        }
    }

}