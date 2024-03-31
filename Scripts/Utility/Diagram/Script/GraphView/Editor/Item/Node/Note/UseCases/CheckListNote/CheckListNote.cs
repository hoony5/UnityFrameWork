using System.Collections.Generic;
using System.Linq;
using Share;
using Share.Enums;

namespace Diagram
{
    public class CheckListNote : ISubNote
    {
        private DiagramNoteNode node;
        public CheckListNoteSubView view;
        private CheckListNoteSubViewBinder binder;
        private TextFormatWriter writer;
        
        public CheckListNote(DiagramNoteNode node)
        {
            this.node = node;
            view = new CheckListNoteSubView(this.node.noteSection.view);
            (binder = new CheckListNoteSubViewBinder(node, view))
                .Bind();
            writer = new TextFormatWriter();
        }

        public void Load(DiagramNodeModel nodeModel)
        {
            view.checkBoxes.Clear();
            
            foreach (CheckListInfo item in nodeModel.Note.CheckList)
            {
                binder.LoadCheckBox(item);
            }

            node.RepaintAllSections();
        }

        public string GetDescription(ExportFileType exportFileType)
        {
            if (exportFileType is ExportFileType.CSV)
            {
                writer.Setup();
                
                view.checkBoxes.ForEach(checkBox =>
                {
                    _ = writer.Write(checkBox.descriptionField.value, checkBox.toggle.value.ToString(), WriterTextStyle.CSV);
                });
                
                return writer.ToCSVFormat();
            }

            switch (exportFileType)
            {
                case ExportFileType.MarkDown:
                    IEnumerable<string> markDownStyle = GetCheckListToStrings(WriterTextStyle.MarkDown);
                    return markDownStyle == null 
                        ? string.Empty 
                        : string.Join("\n", GetCheckListToStrings(WriterTextStyle.MarkDown));
                case ExportFileType.Text:
                    IEnumerable<string> textStyle = GetCheckListToStrings(WriterTextStyle.Default);
                    return textStyle == null 
                        ? string.Empty 
                        : string.Join("\n", GetCheckListToStrings(WriterTextStyle.Default));
                case ExportFileType.Confluence:
                case ExportFileType.Slack:
                    IEnumerable<string> bulletStyle = GetCheckListToStrings(WriterTextStyle.Bullet);
                    return bulletStyle == null 
                        ? string.Empty 
                        : string.Join("\n", GetCheckListToStrings(WriterTextStyle.Bullet));
                default:
                    IEnumerable<string> defaultStyle = GetCheckListToStrings(WriterTextStyle.Default);
                    return defaultStyle == null 
                        ? string.Empty 
                        : string.Join("\n", GetCheckListToStrings(WriterTextStyle.Default));
            }
        }
        
        private IEnumerable<string> GetCheckListToStrings(WriterTextStyle textStyle)
        {
            if (view.checkBoxes.Count == 0) return null;

            return view.checkBoxes.Select(checkBox
                => writer.Write(
                    checkBox.descriptionField.value,
                    checkBox.toggle.value,
                    textStyle));
        }
        public void Clear()
        {
            binder?.ClearCheckBoxes();
        }
        public void Dispose()
        {
            view?.Reset();
            binder?.Dispose();
        }
    }

}