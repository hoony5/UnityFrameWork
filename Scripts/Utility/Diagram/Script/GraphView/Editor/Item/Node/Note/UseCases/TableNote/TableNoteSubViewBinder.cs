using System;
using System.Collections.Generic;
using Share;
using UnityEngine.UIElements;

namespace Diagram
{
    public class TableNoteSubViewBinder : IDisposable
    {
        private TableNote note;
        private TableNoteSubView view;
        private IVisualElementScheduledItem visibilityTask;
        
        public TableNoteSubViewBinder(TableNote note, TableNoteSubView view)
        {
            this.view = view;
            this.note = note;
            SetVisibilityTask(note);
        }

        private void SetVisibilityTask(TableNote note)
        {
            visibilityTask = note.node.schedule.Execute(() =>
            {
                bool setVisible = note.view.visibilityButtonToggle.value;
                note.view.tableAddRowButton.SetVisible(setVisible);
                note.view.tableAddColumnButton.SetVisible(setVisible);
                note.view.columnButtonContainer.SetVisible(setVisible);
                note.view.rows.Values.ForEach(eachRow => eachRow.view.rowDeleteButton.SetVisible(setVisible));
            }).Every(100);

        }

        private void AddOrUpdateModel( int rowIndex, int columnIndex)
        {
            if (note.node.NodeModel.Note.Cells.ContainsKey(rowIndex))
            {
                note.node.NodeModel.Note.Cells[rowIndex].Add(new CellInfo { Column = columnIndex, Row = rowIndex, Value = ""});
            }
            else
            {
                note.node.NodeModel.Note.Cells.Add(rowIndex, new List<CellInfo> { new CellInfo { Column = columnIndex, Row = rowIndex, Value = ""}});
            }
        }
        public void AddCell(int rowIndex, int columnIndex)
        {
            if(!view.rows.TryGetValue(rowIndex, out CellContainer rowContainer))
                view.rows.Add(rowIndex, rowContainer = new CellContainer(note, rowIndex));
                
            Cell newCell = new Cell(rowIndex, columnIndex);
            rowContainer.AddCell(newCell);
            AddOrUpdateModel(rowIndex, columnIndex);
        }

        public void AddRow()
        {
            if (note.row < 0) note.row = 0;
            if (note.column <= 0) note.column = 1;
            
            for (int columnIndex = 0; columnIndex < note.column; columnIndex++)
            {
                AddCell(note.row, columnIndex);
            }

            note.row++;
            RepaintCellContainer();
        }

        public void AddColumn()
        {
            if(view.rows.Values.Count == 0)
            {
                AddRow();
                return;
            }
            
            foreach (CellContainer rowContainer in view.rows.Values)
            {
                AddCell(rowContainer.index, note.column);
            }
            note.column++;
            RepaintCellContainer();
        }

        public void ClearCells()
        {
            if (view.rows.Count == 0) return;
            
            foreach (CellContainer rowContainer in view.rows.Values)
            {
                rowContainer.ClearCells();
            }
            note.row = 0;
            note.column = 0;
            view.rows.Clear();
            view.CellContainer.Clear();
            note.node.NodeModel.Note.Cells.Clear();
        }

        public void ClearSwapTextFields()
        {
            view.swapTargetButton.text = "Row";
            view.swapFromTextField.value = "";
            view.swapToTextField.value = "";
        }
        
        private void SwapRowValue(int columnIndex, int rowIndex, int otherRowIndex)
        {
            (view.GetRow(rowIndex).GetCell(columnIndex).TextField.value, view.GetRow(otherRowIndex).GetCell(columnIndex).TextField.value)
                = (view.GetRow(otherRowIndex).GetCell(columnIndex).TextField.value, view.rows[rowIndex].GetCell(columnIndex).TextField.value);
                
            (note.node.NodeModel.Note.Cells[rowIndex][columnIndex].Value, note.node.NodeModel.Note.Cells[otherRowIndex][columnIndex].Value)
                = (note.node.NodeModel.Note.Cells[otherRowIndex][columnIndex].Value, note.node.NodeModel.Note.Cells[rowIndex][columnIndex].Value);
        }
        private void SwapColumnValue(int rowIndex, int columnIndex, int otherColumnIndex)
        {
            ( view.GetRow(rowIndex).GetCell(columnIndex).TextField.value, view.GetRow(rowIndex).GetCell(otherColumnIndex).TextField.value)
                = (view.GetRow(rowIndex).GetCell(otherColumnIndex).TextField.value, view.GetRow(rowIndex).GetCell(columnIndex).TextField.value);
                
            (note.node.NodeModel.Note.Cells[rowIndex][columnIndex].Value, note.node.NodeModel.Note.Cells[rowIndex][otherColumnIndex].Value)
                = (note.node.NodeModel.Note.Cells[rowIndex][otherColumnIndex].Value, note.node.NodeModel.Note.Cells[rowIndex][columnIndex].Value);
        }

        private void SwapRow(int rowIndex, int otherRowIndex)
        {
            if (!view.rows.ContainsKey(rowIndex)) return;
            if (!view.rows.ContainsKey(otherRowIndex)) return;

            view.GetRow(rowIndex).view.cells.For(( columnIndex, _ ) => SwapRowValue(columnIndex, rowIndex, otherRowIndex));
            
            RepaintCellContainer();
        }
        private void SwapColumn(int columnIndex,int otherColumnIndex)
        {
            // iterate all rows
            for (int rowIndex = 0; rowIndex < view.rows.Count; rowIndex++)
            {
                if (!view.rows.ContainsKey(rowIndex)) continue;

                int currentRowIndex = rowIndex;
                view.GetRow(rowIndex).view.cells
                    .For(( _, _ ) => SwapColumnValue(currentRowIndex, columnIndex, otherColumnIndex));
                
            }
            
            RepaintCellContainer();
        }

        private void BindSwap()
        {
            if (view.swapTargetButton.text.Equals("Row", StringComparison.OrdinalIgnoreCase))
            {
                SwapRow(int.TryParse(view.swapFromTextField.value, out int from) ? from : 0,
                    int.TryParse(view.swapToTextField.value, out int to) ? to : 0);
                
                return;
            }
            if (view.swapTargetButton.text.Equals("Column", StringComparison.OrdinalIgnoreCase))
            {
                SwapColumn(int.TryParse(view.swapFromTextField.value, out int from) ? from : 0,
                    int.TryParse(view.swapToTextField.value, out int to) ? to : 0);
            }
        }

        private void BindSwapTarget()
        {
            if (view.swapTargetButton.text.Equals("Row", StringComparison.OrdinalIgnoreCase))
            {
                view.swapTargetButton.text = "Column";
                view.swapTargetButton.SetWidth(50);
                return;
            }
            if (view.swapTargetButton.text.Equals("Column", StringComparison.OrdinalIgnoreCase))
            {
                view.swapTargetButton.text = "Row";
                view.swapTargetButton.SetWidth(40);
            }
        }

        private void BindSwapFromIndexText(ChangeEvent<string> evt)
        {
            view.swapFromTextField.value =
                int.TryParse(view.swapFromTextField.value, out int result) 
                ? result.ToString() 
                : "";
        }
        private void BindSwapToIndexText(ChangeEvent<string> evt)
        {
            view.swapToTextField.value =
                int.TryParse(view.swapToTextField.value, out int result) 
                ? result.ToString() 
                : "";
        }
        
        public void RepaintCellContainer()
        {
            view.CellContainer.Clear();
            
            if(view.rows.Values.Count == 0) return;
            
            view.rows.Values.ForEach(eachRow =>
            {
                eachRow.Repaint();
                view.CellContainer.Add(eachRow);
            });
            
            view.CreateColumnDeletionButtons(note);
            view.columnButtonContainer.binder.BindColumnDeletion();
        }

        public void Bind()
        {
            view.tableAddColumnButton.clicked += AddColumn;
            view.tableAddRowButton.clicked += AddRow;
            
            view.swapButton.clicked += BindSwap;
            view.swapTargetButton.clicked += BindSwapTarget;
            view.swapFromTextField.RegisterValueChangedCallback(BindSwapFromIndexText);
            view.swapToTextField.RegisterValueChangedCallback(BindSwapToIndexText);
        }
        public void Dispose()
        {
            visibilityTask?.Pause();
            visibilityTask = null;
            view.tableAddColumnButton.clicked -= AddColumn;
            view.tableAddRowButton.clicked -= AddRow;
            
            view.swapButton.clicked -= BindSwap;
            view.swapTargetButton.clicked -= BindSwapTarget;
            view.swapFromTextField.UnregisterValueChangedCallback(BindSwapFromIndexText);
            view.swapToTextField.UnregisterValueChangedCallback(BindSwapToIndexText);
            
            view.columnButtonContainer.binder.DisposeColumnDeletion();
            view.columnButtonContainer.binder.DisposeRowDeletion();
        }
    }
}