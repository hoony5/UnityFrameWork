using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    public class RowGridElement : GridElement
    {
        public RowGridElement()
        {
            mainContainer
                .element.style.flexDirection = direction = FlexDirection.Row;
            mainContainer
                .element.style.flexWrap = Wrap.Wrap;
            mainContainer
                .element.style.alignItems = Align.Stretch;
        }
        public RowGridElement SetChildren(params VisualElement[] visualElements)
        {
            if(visualElements.Length == 0) return this;
            foreach (VisualElement cell in visualElements)
            {
                AddCellGrid(new UxmlItem<VisualElement>(cell));
            }
            return this;
        }
    }
}