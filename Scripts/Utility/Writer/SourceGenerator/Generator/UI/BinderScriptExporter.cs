using System;
using System.IO;
using Writer.Core;
using Writer.SourceGenerator.Format.UIElements;
using Writer.SourceGenerator.Format.Writer;

namespace Writer.SourceGenerator
{
    public sealed class BinderScriptExporter : UISourceExporter
    {
        private IWriterFormat binderFormat;
        private IContentWriter uiFieldWriter;
        private IContentWriter callbackWriter;
        private IContentWriter registerWriter;
        private IContentWriter unregisterWriter;
        
        private AssetGeneratorSettings settings;
        public BinderScriptExporter(string nameSpace, AssetGeneratorSettings settings)
        {
            this.settings = settings;
            string usingSpace = @"using UnityEngine;
using UnityEngine.UIElements;
";
            
            binderFormat = new UIBinderFrameFormat(nameSpace, usingSpace);
            
            uiFieldWriter = new UIContentFieldWriter(_elementInfos);
            callbackWriter = new UIContentCallbackWriter(_elementInfos);
            registerWriter = new UIContentRegisterWriter(_elementInfos, RegisterType.Add);
            unregisterWriter = new UIContentRegisterWriter(_elementInfos, RegisterType.Remove);
        }
        
        public async void Write<T>(string uxmlPath, string savePath)
        {
            GetVisualElementNames(uxmlPath);
            
            string name = Path.GetFileNameWithoutExtension(savePath);
            await base.WriteScript(savePath, WriteBinderClass(name, nameof(T)));
        }
        private string WriteBinderClass(string binderName, string modelName)
        {
            _classFrameBuilder.Clear();
            Span<char> newName = new Span<char>(modelName.ToCharArray());
            newName[0] = char.ToLower(newName[0]);
            string contentFormat = binderFormat.GetFormat();
            
            ((UIContentRegisterWriter)registerWriter).modelName = ((UIContentRegisterWriter)unregisterWriter).modelName
                = modelName;
            
            string content = string.Format(
                contentFormat,
                binderName,
                uiFieldWriter.WriteContent(),
                registerWriter.WriteContent(),
                unregisterWriter.WriteContent(),
                callbackWriter.WriteContent());
            
            _classFrameBuilder.Append(content);
            return _classFrameBuilder.ToString();
        }
    }
}