using Diagram;
using Writer.Core;
using Writer.SourceGenerator.Format;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Writer.SourceGenerator
{
    public class DDDRepositorySoWriter : DDDSourceExporter
    {
        private IWriterFormat repositorySOFormat;

        public DDDRepositorySoWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            string usingSpace = @"using UnityEngine;";
            repositorySOFormat = new DDDRepositorySOFormat(nodeModel.Header.Namespace, usingSpace);
        }

        protected override string BaseInheritanceName => "RepositorySOBase";

        public async void Write(string path)
        {
            string contentFormat = repositorySOFormat.GetFormat();
            string accessType = DiagramModelModifier.GetNodeViewTypeName<AccessType>();
            string content = contentFormat
                .Replace("{0}", accessType)
                .Replace("{1}", NodeModel.Header.Name);
            
            await base.WriteScript(path, content);
        }
    }
}
