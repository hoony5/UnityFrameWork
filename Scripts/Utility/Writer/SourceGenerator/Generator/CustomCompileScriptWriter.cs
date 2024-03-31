using System.IO;
using UnityEditor.Rendering.Universal;

namespace Writer.SourceGenerator
{
    public class CustomCompileScriptWriter : SourceExporterBase
    {
        public async void WriteExternalInit(string rootPath)
        {
            string savePath = Path.Combine(rootPath, $"IsExternalInit.cs");
            await WriteScript(savePath, GenerateScript());
        }

        private string GenerateScript()
        {
            return $@"namespace System.Runtime.CompilerServices
{{
    public class IsExternalInit
    {{
    }}
}}";
        }
    }
}