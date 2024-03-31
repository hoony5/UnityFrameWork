using System.Collections.Generic;
using System.Linq;
using Share;

namespace Diagram
{
    public class TableNote : ISubNote
    {
        public readonly DiagramNoteNode node;
        
        public readonly TableNoteSubView view;
        private TableNoteSubViewBinder binder;
        private TextFormatWriter writer;
        
        public int row;
        public int column;
        
        public TableNote(DiagramNoteNode node)
        {
            this.node = node;
            view = new TableNoteSubView(node.noteSection.view);
            binder = new TableNoteSubViewBinder(this, view);
            writer = new TextFormatWriter();
            binder.Bind();
            binder.AddCell(0,0);
            
            row = 1;
            column = 1;
            
            Repaint();
        }

        public void Load(DiagramNodeModel nodeModel)
        {
            if(nodeModel.Note.Cells.Count == 0) return;

            view.CellContainer.Clear();
            view.rows.Clear();

            row = nodeModel.Note.Cells.Count;
            column = nodeModel.Note.Cells.First().Value.Count;

            if (row < 0)
                row = 0;
            if (column < 0)
                column = 1;
            
            foreach (KeyValuePair<int, List<CellInfo>> rowCellInfo in nodeModel.Note.Cells)
            {
                int rowIndex = rowCellInfo.Key;
                // row
                view.rows.Add(rowIndex, new CellContainer(this, rowIndex));
                
                // columns
                foreach (CellInfo cell in rowCellInfo.Value)
                {
                    Cell loadedCell = new Cell(cell.Row, cell.Column, cell.Value);
                    view.rows[rowIndex].AddCell(loadedCell);
                }
            }
            Repaint();   
        }

        public void Repaint()
        {
            binder.RepaintCellContainer();
        }
        public void Clear()
        {
            binder?.ClearSwapTextFields();
            binder?.ClearCells();
        }

        private string GetCSVFormat(List<List<string>> rows)
        {
            writer.Setup();
            foreach (List<string> rowContainer in rows)
            {
                string line = string.Join(",", rowContainer);
                writer.AppendLine(line);
            }
            return writer.Submit();
        }
        private string GetMarkDownFormat(List<List<string>> rows)
        {
            if(rows.Count == 0) return string.Empty;
            writer.Clear();
            List<string> header = rows[0];
            string headerLine = string.Join(" | ", header);
            writer.Append("| ");
            writer.Append(headerLine); // header | header | header |
            writer.Append(" |");
            writer.AppendLine();
            
            if(node.NodeModel.Note.ExportFileType != ExportFileType.Confluence)
            {
                writer.Append("| ");
                writer.Append(string.Join(" | ", header.Select(_ => "---"))); // --- | --- | --- |
                writer.Append(" |");
                writer.AppendLine();
            }

            for (var index = 1; index < rows.Count; index++)
            {
                List<string> rowContainer = rows[index];
                string columns = string.Join(" | ", rowContainer);
                writer.Append("| ");
                writer.Append(columns);
                writer.Append(" |");
                writer.AppendLine();
            }
            return writer.Submit();
        }
        public string GetDescription(ExportFileType exportFileType)
        {
            if(node.NodeModel.Note.Cells.Count == 0) return string.Empty;
            List<List<string>> rows = node.NodeModel.Note.Cells.Values
                .Select(rowContainer 
                    => rowContainer
                        .Select(cell => cell.Value)
                        .ToList())
                .ToList();
            
            if(exportFileType == ExportFileType.CSV)
            {
                return GetCSVFormat(rows);
            }
            
            return exportFileType switch
            {
                ExportFileType.MarkDown => GetMarkDownFormat(rows),
                ExportFileType.Text => GetCSVFormat(rows),
                _ => GetMarkDownFormat(rows),
            };
        }

        public void Dispose()
        {
            view?.Reset();
            binder?.Dispose();
        }
    }

}