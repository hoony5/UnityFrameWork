using UnityEngine;
using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    public static class UxmlGUI
    {
#if UNITY_EDITOR
       public static UxmlItem<VisualElement> CreateSpace(float width, float height)
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = space
                .SetUp()
                .SetName($"space-{width}-{height}")
                .StyleWidth(width)
                .StyleHeight(height)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(Color.clear);

            return spaceItem;
        }
       public static UxmlItem<VisualElement> CreateSpace(float width, float height,Color color)
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = space
                .SetUp()
                .SetName($"space-{width}-{height}")
                .StyleWidth(width)
                .StyleHeight(height)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(color);

            return spaceItem;
        }
        
       public static UxmlItem<VisualElement> CreateSpace(
           float width,
           LengthUnit widthUnit,
           float height,
           LengthUnit heightUnit)
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = space
                .SetUp()
                .SetName($"space-{width}-{height}")
                .StyleWidth(width, widthUnit)
                .StyleHeight(height, heightUnit)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(Color.clear);

            return spaceItem;
        }
       public static UxmlItem<VisualElement> CreateSpace(
           float width,
           LengthUnit widthUnit,
           float height,
           LengthUnit heightUnit,
           Color color)
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = space
                .SetUp()
                .SetName($"space-{width}-{height}")
                .StyleWidth(width, widthUnit)
                .StyleHeight(height, heightUnit)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(color);

            return spaceItem;
        }
        
        public static UxmlItem<T> AddSpace<T>(this UxmlItem<T> parent, float width, float height) where T : VisualElement
        {
            VisualElement space = new VisualElement();
           UxmlItem<VisualElement> spaceItem =
               CreateSpace(width, height);

            parent.Add(spaceItem);
            return parent;
        }
        
        public static UxmlItem<T> AddSpace<T>(this UxmlItem<T> parent, float width, float height, Color color) where T : VisualElement
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = CreateSpace(width,height,color);

            parent.Add(spaceItem);
            return parent;
        }
        
        public static UxmlItem<T> AddStretchHorizontalSpace<T>(this UxmlItem<T> parent, float thick) where T : VisualElement
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = CreateSpace(100, LengthUnit.Percent, thick, LengthUnit.Pixel, Color.clear);

            parent.Add(spaceItem);
            return parent;
        }
        
        public static UxmlItem<T> AddStretchHorizontalBar<T>(this UxmlItem<T> parent, float thick, Color color) where T : VisualElement
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = CreateSpace(100, LengthUnit.Percent,thick, LengthUnit.Pixel, color);

            parent.Add(spaceItem);
            return parent;
        }
        
        public static UxmlItem<T> AddStretchVerticalSpace<T>(this UxmlItem<T> parent, float thick) where T : VisualElement
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = CreateSpace(thick, LengthUnit.Pixel, 100, LengthUnit.Percent, Color.clear);

            parent.Add(spaceItem);
            return parent;
        }
        
        public static UxmlItem<T> AddStretchVerticalBar<T>(this UxmlItem<T> parent, float thick, Color color) where T : VisualElement
        {
            VisualElement space = new VisualElement();
            UxmlItem<VisualElement> spaceItem = CreateSpace(thick, LengthUnit.Pixel, 100, LengthUnit.Percent, color);

            parent.Add(spaceItem);
            return parent;
        }
#endif
    }
}