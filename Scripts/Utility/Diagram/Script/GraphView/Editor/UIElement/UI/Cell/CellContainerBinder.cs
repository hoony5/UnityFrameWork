using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEditor;
using UnityEngine.UIElements;

namespace Diagram
{
    public class CellContainerBinder
    {
        private TableNote note;
        private CellContainerView view;
        private CellContainer container;
        public CellContainerBinder(TableNote note, CellContainer container, CellContainerView view)
        {
            this.note = note;
            this.view = view;
            this.container = container;
        }
        private void SetCellValue(FocusOutEvent evt)
        {
            if (!note.node.NodeModel.Note.Cells.TryGetValue(container.index, out List<CellInfo> columnCell))
                return;
            
            if(columnCell.Count == 0) return;
    
            for(int columnIndex = 0; columnIndex < columnCell.Count; columnIndex++)
            {
                columnCell[columnIndex].Value = container.view.cells[columnIndex].TextField.value;
            }
        }
        
        public void BindCells()
        {
            if (container.view.cells.Count == 0) return;
            container.view.cells.ForEach(cell =>
            {
                cell.TextField.RegisterCallback<FocusOutEvent>(SetCellValue);
            });
        }
        
        public void DisposeCells()
        {
            if (container.view.cells.Count == 0) return;
            container.view.cells.ForEach(cell =>
            {
                cell.TextField.UnregisterCallback<FocusOutEvent>(SetCellValue);
            });
        }
        public void DisposeRowDeletion()
        {
            if (view.rowDeleteButton == null) return;
            view.rowDeleteButton.clicked -= DeleteRowCells;
        }
        public void BindRowDeletion()
        {
            if (view.rowDeleteButton == null) return;
            view.rowDeleteButton.clicked += () =>
            {
                if(note.view.safeClearToggle.value)
                {
                    bool doClear = EditorUtility.DisplayDialog("Clear Row Cells", $"Are you sure you want to clear row_{container.index} cells?", "Yes", "No");
                    if (!doClear) return;
                }
                
                DeleteRowModels();
                DeleteRowCells();
            };
        }

        private void DeleteRowModels()
        {
             switch (container.userData)
            {
                case null:
                    return;
                case DiagramNodeModel Model:
                {
                    container.RemoveFromHierarchy();
                    
                    // reorganizing Model
                    Model.Note.Cells.Remove(container.index);

                    SerializedDictionary<int, List<CellInfo>> tempModelDictionary 
                        = new SerializedDictionary<int, List<CellInfo>>(Model.Note.Cells);
                    Model.Note.Cells.Clear();

                    int reOrderCellInfoIndex = 0;
                    foreach (KeyValuePair<int, List<CellInfo>> pair in tempModelDictionary)
                    {
                        pair.Value.ForEach(cell => cell.Row = reOrderCellInfoIndex);
                        Model.Note.Cells.Add(reOrderCellInfoIndex, pair.Value);
                        reOrderCellInfoIndex++;
                    }
                    break;
                }
            }
        }
        private void DeleteRowCells()
        {
            note.view.rows.Remove(container.index);
            // reorganizing view
            SerializedDictionary<int, CellContainer> tempContainerDictionary 
                = new SerializedDictionary<int, CellContainer>(note.view.rows);
            note.view.rows.Clear();
                    
            int reOrderContainerIndex = 0;
            foreach (KeyValuePair<int, CellContainer> pair in tempContainerDictionary)
            {
                pair.Value.index = reOrderContainerIndex;
                pair.Value.binder.DisposeRowDeletion();
                pair.Value.binder.BindRowDeletion();
                note.view.rows.Add(reOrderContainerIndex, pair.Value);
                reOrderContainerIndex++;
            }

            if(note.view.rows.Count == 0)
            {
                note.row = 0;
                note.column = 0;
            }
            else
            {
                note.row--;
                if(note.row < 0) note.row = 0;
            }
            note.Repaint();
        }

        public void BindColumnDeletion()
        {
            if (view.columnDeleteButtons == null) return;
            if (view.columnDeleteButtons.Count == 0) return;
            
            view.columnDeleteButtons.ForEach(button => button.clicked += () =>
            {
                if (note.view.safeClearToggle.value)
                {
                    bool doClear = EditorUtility.DisplayDialog("Clear Column Cells", $"Are you sure you want to clear column_{(int)button.userData} cells?", "Yes", "No");
                    if (!doClear) return;
                }
                DeleteColumnModels(button);
                DeleteColumnCells(button);
                DeleteColumnButton(button);
            });
        }

        public void DisposeColumnDeletion()
        {
            if (view.columnDeleteButtons == null) return;
            if (view.columnDeleteButtons.Count == 0) return;
            
            view.columnDeleteButtons.Clear();
        }
        
        private void DeleteColumnButton(Button button)
        {
            if(view.columnDeleteButtons.Count == 0) return;
            if ((int)button.userData == -1) return;

            int removedIndex = (int)button.userData;
            // button
            container.Remove(container.view.columnDeleteButtons[removedIndex]);
            container.view.columnDeleteButtons.RemoveAt(removedIndex);
            container.view.columnDeleteButtons.For((index, columnDeletionButton) => columnDeletionButton.userData = index);

            if(note.view.rows.Count == 0)
            {
                note.column = 0;
                note.row = 0;
            }
            else
            {
                note.column--;
                if(note.column < 0) note.column = 0;    
            }
            note.Repaint();
        }
        
        private void DeleteColumnCells(Button button)
        {
            if (note.view.rows.Values.Count == 0) return;
            if(view.columnDeleteButtons.Count == 0) return;
            if ((int)button.userData == -1) return;

            int removedIndex = (int)button.userData;
            bool allCleared = false;
            // button
            note.view.rows.Values.ForEach(rowContainer =>
            {
                rowContainer.RemoveCellAt(removedIndex);
                if(rowContainer.IsEmpty)
                {
                    allCleared = true;
                    rowContainer.RemoveFromHierarchy();
                }
            });
            if (!allCleared) return;
            note.view.rows.Clear();
        }

        private void DeleteColumnModels(Button button)
        {
            if(view.columnDeleteButtons.Count == 0) return;
            switch (container.userData)
            {
                case null:
                    return;
                case DiagramNodeModel Model:
                    if (Model.Note.Cells.Count == 0) return;
                    
                    if ((int)button.userData == -1) return;

                    int removedIndex = (int)button.userData;
                    // remove on data
                    for (var rowIndex = 0; rowIndex < Model.Note.Cells.Count; rowIndex++)
                    {
                        if (!Model.Note.Cells.ContainsKey(rowIndex)) continue;

                        Model.Note.Cells[rowIndex].RemoveAt(removedIndex);
                        // reorder column index
                        for (var columnIndex = 0; columnIndex < Model.Note.Cells[rowIndex].Count; columnIndex++)
                        {
                            Model.Note.Cells[rowIndex][columnIndex].Column = columnIndex;
                        }

                        if (Model.Note.Cells[rowIndex].Count == 0)
                            Model.Note.Cells.Remove(rowIndex);
                    }
                    break;
            }
        }
    }
}