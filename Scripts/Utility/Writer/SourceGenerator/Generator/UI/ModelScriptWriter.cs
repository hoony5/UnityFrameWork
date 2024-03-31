using System.IO;
using System.Threading.Tasks;
using Writer.Core;
using Writer.SourceGenerator.Format;
using Writer.SourceGenerator.Format.Writer;

namespace Writer.SourceGenerator
{
    public sealed class UxmlToModelScript : UISourceWriter
    {
        private IWriterFormat UIModelFormat;
        private IContentWriter uiFieldWriter;
        
        public UxmlToModelScript()
        {
            UIModelFormat = new UIModelFormat();
            uiFieldWriter = new UIFieldWriter(_elementInfos);
        }
        
        public async void WriteModelAsync(string uxmlPath, string savePath)
        {
            GetVisualElementNames(uxmlPath);

            string name = Path.GetFileNameWithoutExtension(savePath);
            await Write(savePath, GenerateModelClass(name));
        }
        
        private string GenerateModelClass(string name)
        {
            _classFrameBuilder.Clear();

            string contentFormat = UIModelFormat.GetFormat();
            string content = string.Format(contentFormat,
                name,
                uiFieldWriter.WriteContent(),
                GenerateInitModelQuery());
            
            _classFrameBuilder.Append(content);
            
            return _classFrameBuilder.ToString();
        }
    }
}
