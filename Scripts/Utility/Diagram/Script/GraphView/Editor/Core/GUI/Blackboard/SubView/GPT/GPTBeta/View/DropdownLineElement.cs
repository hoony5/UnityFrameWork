using System.Collections.Generic;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace GPT
{
    public class DropdownLineElement : LineElement
    {
        public readonly UxmlItem<DropdownField> dropdown;
            
        public DropdownLineElement(string title, List<string> items)
            : base(title)
        {
            dropdown = new DropdownField()
                .SetUp()
                .SetChoices(items)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .SetIndex(0)
                .StyleWidth(300)
                .StyleHeight(25);
                
            this.Add(dropdown);
        }
    }
}