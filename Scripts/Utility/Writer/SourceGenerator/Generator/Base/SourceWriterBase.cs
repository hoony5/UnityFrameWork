using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Writer;
using Writer.Core;

namespace Writer.SourceGenerator
{
    public class SourceWriterBase : IWriter
    {
        async Task IWriter.WriteScript(string path, string content)
        {
            await FileExporter.Write(path, content);
            
            FileExporter.RefreshEditor();
            
            FileExporter.PingObject(path);
        }
        
        protected async Task Write(string path, string content)
        {
            await ((IWriter)this).WriteScript(path, content);
        }
    }
}