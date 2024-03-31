using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class CellContainer : VisualElement
    {
        private DiagramNodeModel model;
        public readonly CellContainerView view;
        public readonly CellContainerBinder binder;
        public int index;
        public int ColumnCount => view.cells.Count;
        public bool IsEmpty => view.cells.Count == 0;
        
        public CellContainer(TableNote note, int index = -1)
        {
            model = note.node.NodeModel;
            this.index = index;
            this.SetFlexGrow(0);
            this.SetFlexDirection(FlexDirection.Row);
            view = new CellContainerView(this);
            binder = new CellContainerBinder(note, this, view);
            userData = note.node.NodeModel;
            
            if (index == -1) return;
            _ = view.CreateRowDeletionButton();
            binder.BindRowDeletion();
        }

        public Cell GetCell(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= view.cells.Count)
                return null;
            return view.cells[columnIndex];
        }
        public void Repaint()
        {
            Clear();
            binder.DisposeCells();
            foreach (Cell cell in view.cells)
            {
                Add(cell);
            }
            VisualElement empty = new VisualElement();
            empty.SetSize(20, 20);
            Add(empty);
            RepaintRowDeletion();
            binder.BindCells();
        }
        private void RepaintRowDeletion()
        {
            if (view.rowDeleteButton == null) return;
            // create
            _ = view.CreateRowDeletionButton();
            // bind event
            binder.BindRowDeletion();
            // add to hierarchy
            Add(view.rowDeleteButton);
        }

        public void AddCell(Cell cell)
        {
            if (cell == null) return;
            
            if(view.cells.Exists(c => c.row == cell.row && c.column == cell.column))
            {
                Debug.LogWarning($"Already Exist {cell.row} {cell.column}.");
                return;
            }
            view.cells.Add(cell);
            Add(cell);
        }
        public void RemoveCellAt(int cellColumnIndex)
        {
            if(view.cells.Count == 0) return;
            if (cellColumnIndex < 0 || cellColumnIndex >= view.cells.Count)
            {
                Debug.LogWarning($"Out of Range {cellColumnIndex}");
                return;
            }
            if(view.cells[cellColumnIndex] != null)
                view.cells[cellColumnIndex].RemoveFromHierarchy();
            
            view.cells.RemoveAt(cellColumnIndex);
            if(view.cells.Count == 0)
            {
                RemoveFromHierarchy();
                return;
            }
            view.cells.For((columnIndex, cell) => cell.column = columnIndex);
        }
        public void ClearCells()
        {
            foreach (Cell cell in view.cells)
            {
                cell.RemoveFromHierarchy();
            }

            view.cells.Clear();
            RemoveFromHierarchy();
            Remove(view.rowDeleteButton);
        }
    }
}