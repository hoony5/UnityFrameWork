using Share;
using Share.Enums;

namespace Diagram
{
    public class SummaryNote : ISubNote
    {
        private SummaryNoteSubView view;
        private SummaryNoteSubViewBinder binder;
        private TextFormatWriter writer;
        
        public SummaryNote(DiagramNoteNode noteNode)
        {
            view = new SummaryNoteSubView(noteNode.noteSection.view);
            binder = new SummaryNoteSubViewBinder(noteNode, view);
            writer = new TextFormatWriter();
            binder.Bind();
        }
        public void Load(DiagramNodeModel nodeModel)
        {
            view.summary.value = nodeModel.Note.Summary;
            view.detail.value = nodeModel.Note.Detail;
        }
        public void Clear()
        {
            view.summary.value = "";
            view.detail.value = "";
        }

        public string GetDescription(ExportFileType exportFileType)
        {
            writer.Setup();
            if(exportFileType == ExportFileType.CSV)
            {
                _ = writer.Write(view.summary.value, view.detail.value, WriterTextStyle.CSV);
                return writer.ToCSVFormat();
            }

            switch (exportFileType)
            {
                case ExportFileType.MarkDown:
                    writer.AppendLine(writer.Write("Summary", view.summary.value, WriterTextStyle.MarkDown));
                    writer.AppendLine();
                    writer.AppendLine(writer.Write("Detail", view.detail.value, WriterTextStyle.MarkDown));
                    return writer.Submit();
                case ExportFileType.Confluence:
                case ExportFileType.Slack:
                    writer.AppendLine(writer.Write("Summary", view.summary.value, WriterTextStyle.Indent));
                    writer.AppendLine();
                    writer.AppendLine(writer.Write("Detail", view.detail.value, WriterTextStyle.Indent));
                    return writer.Submit();
                default:
                    writer.AppendLine(writer.Write("Summary", view.summary.value, WriterTextStyle.Default));
                    writer.AppendLine();
                    writer.AppendLine(writer.Write("Detail", view.detail.value, WriterTextStyle.Default));
                    return writer.Submit();
            }
        }

        public void Dispose()
        {
            view?.Reset();
            binder?.Dispose();
        }
    }
}