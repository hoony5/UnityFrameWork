using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace GPT
{
    public class SliderLineElement : LineElement
    {
        public readonly UxmlItem<Slider> slider;
            
        public SliderLineElement(string title, float min, float max, float defaultValue)
            : base(title)
        {
            slider = new Slider()
                .SetUp()
                .SetSliderLowValue(min)
                .SetSliderHighValue(max)
                .SetValue(defaultValue)
                .SetSliderShowInputField(true)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(300)
                .StyleHeight(25);
                
            this.Add(slider);
        }
    }
}