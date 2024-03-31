using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    [System.Serializable]
    public class GridCell : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<VisualElement> mainContainer;
        public readonly UxmlItem<VisualElement> contentContainer;

        public GridCell()
        {
            mainContainer = new VisualElement().SetUp();
            contentContainer = new VisualElement().SetUp();

            mainContainer
                .StyleFlexGrow(0)
                .StyleFlexShrink(1);
            
            contentContainer
                .StyleFlexGrow(0)
                .StyleFlexShrink(1);
            
            mainContainer.Add(contentContainer);
            this.Add(mainContainer);
        }

        private void SetStyle()
        {
            mainContainer
                .StyleAlignItems(Align.Center)
                .StyleJustifyContent(Justify.Center);
        }
        
        public void SetCellWidth(int width)
        {
            mainContainer.element.style.width = width;
        }
        
        public void SetCellHeight(int height)
        {
            mainContainer.element.style.height = height;
        }
        
        public void SetCellSize(int width, int height)
        {
            SetCellWidth(width);
            SetCellHeight(height);
        }
        
        public void SetElementWidth(int width)
        {
            contentContainer.element.style.width = width;
        }
        
        public void SetElementHeight(int height)
        {
            contentContainer.element.style.height = height;
        }
        
        public void SetElementSize(int width, int height)
        {
            SetElementWidth(width);
            SetElementHeight(height);
        }
    }
}