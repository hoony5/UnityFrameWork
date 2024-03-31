using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Diagram
{
    public class CellContainerView
    {
        private CellContainer container;
        public Button rowDeleteButton;
        public readonly List<Cell> cells;
        public readonly List<Button> columnDeleteButtons;
        
        public CellContainerView(CellContainer container)
        {
            this.container = container;
            cells = new List<Cell>();
            columnDeleteButtons = new List<Button>();
        }

        public Button CreateRowDeletionButton()
        {
            return rowDeleteButton = CreateDeleteButton();
        }

        private Button CreateDeleteButton()
        {
            return new Button
            {
                text = "-",
                style =
                {
                    width = 20,
                    height = 20,
                    flexGrow = 0,
                    flexShrink = 0,
                }
            };
        }

        public void CreateColumnDeletionButton(int cellCount)
        {
            columnDeleteButtons.Clear();
            if (cellCount < 0) return;
            if (cellCount == 1) cellCount = 1;

            for (var count = 0; count < cellCount; count++)
            {
                Button deleteButton = CreateDeleteButton();
                // manage
                columnDeleteButtons.Add(deleteButton);
                // userdata
                deleteButton.userData = count;
                deleteButton.SetWidth(60);
                container.Add(deleteButton);
            }
        }
    }
}