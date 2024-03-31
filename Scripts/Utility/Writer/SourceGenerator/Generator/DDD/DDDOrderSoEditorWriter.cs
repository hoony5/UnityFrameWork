using Diagram;
using Writer.Core;
using Writer.Ex;
using Writer.SourceGenerator.Format;

namespace Writer.SourceGenerator
{
    public class DDDOrderSOEditorWriter : DDDSourceExporter
    {
        private IWriterFormat orderSOEditorFormat;
        public DDDOrderSOEditorWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            string usingSpace = @"using UnityEditor;
using UnityEngine;
";
            orderSOEditorFormat = new DDDOrderSOEditorFormat(nodeModel.Header.Namespace, usingSpace);
        }
        protected override string BaseInheritanceName => "Editor";

        public async void Write(string path)
        {
            string contentFormat = orderSOEditorFormat.GetFormat();
            string content = contentFormat.Replace("{0}", NodeModel.Header.Name);
            await base.WriteScript(path, content);
        }
    }
}