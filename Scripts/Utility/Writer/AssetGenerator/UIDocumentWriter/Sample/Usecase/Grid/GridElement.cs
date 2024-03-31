using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    [System.Serializable]
    public class GridElement : UxmlItem<VisualElement>
    {
        protected UxmlItem<VisualElement> mainContainer;
        protected List<GridCell> elements;
        
        [SerializeField] protected FlexDirection direction;
        [SerializeField] protected int row;
        [SerializeField] protected int column;
        
        [SerializeField] protected int elementWidth;
        [SerializeField] protected int elementHeight;
        [SerializeField] protected int elementSize;
        
        [SerializeField] protected int spaceWidth;
        [SerializeField] protected int spaceHeight;
        [SerializeField] protected int spaceSize;
        
        [SerializeField] protected int upPadding;
        [SerializeField] protected int downPadding;
        [SerializeField] protected int rightPadding;
        [SerializeField] protected int leftPadding;
        [SerializeField] protected int padding;
        
        protected GridElement()
        {
            mainContainer = new UxmlItem<VisualElement>();
            mainContainer.element.style.flexWrap = Wrap.Wrap;
            mainContainer.element.style.flexDirection = FlexDirection.Row;
            
            elements = new List<GridCell>();

            this.Add(mainContainer);
        }
        public int Row
        {
            get => row;
            set
            {
                row = value;
                if(elements?.Count <= 0) return;
                mainContainer.StyleHeight((2 * spaceWidth + elementHeight) * row);
            }
        }
        
        public int Column
        {
            get => column;
            set
            {
                column = value;
                if(elements?.Count <= 0) return;
                mainContainer.StyleWidth((2 * spaceHeight + elementWidth) * column);
            }
        }
        
        public int ElementWidth
        {
            get => elementWidth;
            set
            {
                elementWidth = value;
                elementSize = (elementWidth + elementHeight) / 2;
                
                if (elements?.Count <= 0) return;
                elements?.ForEach(cell => cell.SetElementWidth(elementWidth));
            }
        }
        public int ElementHeight
        {
            get => elementHeight;
            set
            {
                elementHeight = value;
                elementSize = (elementWidth + elementHeight) / 2;
                
                if (elements?.Count <= 0) return;
                elements?.ForEach(cell => cell.SetElementHeight(elementHeight));
            }
        }
        public int ElementSize
        {
            get => elementSize;
            set
            {
                elementSize = value;
                elementWidth = elementSize;
                elementHeight = elementSize;
                
                if (elements?.Count <= 0) return;
                elements?.ForEach(cell => cell.SetElementSize(elementSize, elementSize));
            }
        }
        public FlexDirection Direction
        {
            get => direction;
            set
            {
                direction = value;
                mainContainer.StyleFlexDirection(direction);
            }
        }
        public int Padding
        {
            get => padding;
            set
            {
                padding = value;
                upPadding = downPadding = rightPadding = leftPadding = padding;
                mainContainer.element.style.paddingBottom = padding;
                mainContainer.element.style.paddingTop = padding;
                mainContainer.element.style.paddingRight = padding;
                mainContainer.element.style.paddingLeft = padding;
            }
        }
        
        public int UpPadding
        {
            get => upPadding;
            set
            {
                upPadding = value;
                mainContainer.element.style.paddingTop = upPadding;
                padding = (upPadding + downPadding + rightPadding + leftPadding) / 4;
            }
        }
        
        public int DownPadding
        {
            get => downPadding;
            set
            {
                downPadding = value;
                mainContainer.element.style.paddingBottom = downPadding;
                padding = (upPadding + downPadding + rightPadding + leftPadding) / 4;
            }
        }
        
        public int RightPadding
        {
            get => rightPadding;
            set
            {
                rightPadding = value;
                mainContainer.element.style.paddingRight = rightPadding;
                padding = (upPadding + downPadding + rightPadding + leftPadding) / 4;
            }
        }
        
        public int LeftPadding
        {
            get => leftPadding;
            set
            {
                leftPadding = value;
                mainContainer.element.style.paddingLeft = leftPadding;
                padding = (upPadding + downPadding + rightPadding + leftPadding) / 4;
            }
        }

        public int SpaceWidth
        {
            get => spaceWidth;
            set
            {
                spaceWidth = value;
                spaceSize = (spaceWidth + spaceHeight) / 2;
                
                if(elements.Count <= 0) return;
                elements.ForEach(cell 
                    => cell.SetCellWidth(2 * spaceWidth + elementWidth));
            }
        }
        
        public int SpaceHeight
        {
            get => spaceHeight;
            set
            {
                spaceHeight = value;
                spaceSize = (spaceWidth + spaceHeight) / 2;
                
                if(elements.Count <= 0) return;
                elements.ForEach(cell 
                    => cell.SetCellHeight(2 * spaceHeight + elementHeight));
            }
        }
        
        public int SpaceSize
        {
            get => spaceSize;
            set
            {
                spaceSize = value;
                SpaceWidth = spaceSize;
                SpaceHeight = spaceSize;
                
                if(elements.Count <= 0) return;
                elements.ForEach(cell 
                    => cell.SetCellSize(
                        2 * spaceSize + elementWidth,
                        2 * spaceSize + elementHeight));
            }
        }
        
        public void Repaint()
        {
            // container size
            mainContainer.element.style.height = (2 * spaceWidth + elementHeight) * row;
            mainContainer.element.style.width = (2 * spaceHeight + elementWidth) * column;
            
            if (elements?.Count <= 0) return;
            
            mainContainer.Clear();
            elements?.ForEach(cell => mainContainer.Add(cell));
            
            // element content size
            elements?.ForEach(cell 
                => cell.SetElementSize(elementSize, elementSize));
            
            // element cell size = element content size + space size
            elements?.ForEach(cell 
                => cell.SetCellSize(
                    2 * spaceSize + elementWidth,
                    2 * spaceSize + elementHeight));
            
            // container padding
            upPadding = downPadding = rightPadding = leftPadding = padding;
            mainContainer.element.style.paddingBottom = padding;
            mainContainer.element.style.paddingTop = padding;
            mainContainer.element.style.paddingRight = padding;
            mainContainer.element.style.paddingLeft = padding;
        }

        public void AddCellGrid(GridCell cell)
        {
            elements.Add(cell);
        }
        
        public GridCell AddCellGrid(UxmlItem<VisualElement> content)
        {
            GridCell gridCell = new GridCell();
            gridCell.contentContainer.Add(content);
            elements.Add(gridCell);
            return gridCell;
        }
        
        public void RemoveCellGrid(GridCell cell)
        {
            if (!elements.Contains(cell)) return;
            elements.Remove(cell);
        }
        
        public void ClearContainer()
        {
            elements.Clear();
            mainContainer.Clear();
        }
    }
}