using Share;
using Share.Enums;

namespace Diagram
{
    public class DefaultNote : ISubNote
    {
        private DefaultNoteSubView view;
        private DefaultNoteSubViewBinder binder;
        private TextFormatWriter writer;
        private DiagramNoteNode node;
        
        public DefaultNote(DiagramNoteNode node)
        {
            this.node = node;
            view = new DefaultNoteSubView(node.noteSection.view);
            binder = new DefaultNoteSubViewBinder(node, view);
            writer = new TextFormatWriter();
            binder.Bind();
        }
        public void Load(DiagramNodeModel nodeModel)
        {
            view.subtitle.value = nodeModel.Note.SubTitle;
            view.description.value = nodeModel.Note.Description;
        }
        public void Clear()
        {
            view.subtitle.value = "";
            view.description.value = "";
        }

        public string GetDescription(ExportFileType exportFileType)
        {
            if(exportFileType == ExportFileType.CSV)
            {
                writer.Setup();
                _ = writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.CSV);
                return writer.ToCSVFormat();
            }
            return exportFileType switch
            {
                ExportFileType.MarkDown => writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.MarkDown),
                ExportFileType.Text => writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.Default),
                ExportFileType.Confluence => writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.Bullet),
                ExportFileType.Slack => writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.Bullet),
                _ =>  writer.Write(node.NodeModel.Note.Title, view.subtitle.value, view.description.value, WriterTextStyle.Default),
            };
        }

        public void Dispose()
        {
            binder?.Dispose();
            view?.Reset();
        }

    }
}