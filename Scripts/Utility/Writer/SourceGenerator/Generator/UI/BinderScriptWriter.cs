using System;
using System.IO;
using System.Threading.Tasks;
using Writer.SourceGenerator.Format;
using Writer.SourceGenerator;
using Writer.Core;
using Writer.SourceGenerator.Format.UIElements;
using Writer.SourceGenerator.Format.Writer;

namespace Writer.SourceGenerator
{
    public sealed class BinderScriptWriter : UISourceWriter
    {
        private IWriterFormat binderFormat;
        private IContentWriter uiFieldWriter;
        private IContentWriter callbackWriter;
        private IContentWriter registerWriter;
        private IContentWriter unregisterWriter;
        
        public BinderScriptWriter()
        {
            binderFormat = new UIBinderFormat();
            
            uiFieldWriter = new UIFieldWriter(_elementInfos);
            callbackWriter = new UICallbackWriter(_elementInfos);
            registerWriter = new UIRegisterWriter(_elementInfos, RegisterType.Add);
            unregisterWriter = new UIRegisterWriter(_elementInfos, RegisterType.Remove);
        }
        
        public async void WriteBinderAsync<T>(string uxmlPath, string savePath)
        {
            GetVisualElementNames(uxmlPath);
            
            string name = Path.GetFileNameWithoutExtension(savePath);
            await Write(savePath, WriteBinderClass(name, nameof(T)));
        }
        private string WriteBinderClass(string binderName, string modelName)
        {
            _classFrameBuilder.Clear();
            Span<char> newName = new Span<char>(modelName.ToCharArray());
            newName[0] = char.ToLower(newName[0]);
            string contentFormat = binderFormat.GetFormat();
            
            ((UIRegisterWriter)registerWriter).modelName = ((UIRegisterWriter)unregisterWriter).modelName
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