using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    public class ColumnGridElement : GridElement
    {
        public ColumnGridElement()
        {
            mainContainer
                .element.style.flexDirection = direction = FlexDirection.Column;
            mainContainer
                .element.style.flexWrap = Wrap.Wrap;
            mainContainer
                .element.style.alignItems = Align.Stretch;
        }
    
        public ColumnGridElement SetChildren(params VisualElement[] visualElements)
        {
            if (visualElements.Length == 0) return this;
            foreach (VisualElement cell in visualElements)
            {
                AddCellGrid(new UxmlItem<VisualElement>(cell));
            }
            return this;
        }

    }
}