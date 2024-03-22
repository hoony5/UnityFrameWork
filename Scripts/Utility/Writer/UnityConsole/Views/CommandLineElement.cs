using System;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Writer.CommandConsole
{
    public class CommandLineElement : LineElement
    {
        public readonly CommandLineType commandLineType;
        
        public CommandLineElement(CommandLineType type)
        {
            commandLineType = type;
            
            Color color = type.GetColor();

            this
                .StyleFlexDirection(FlexDirection.Column)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);

            headerLabel.StyleColor(color);
            contentLabel.StyleColor(color);
            
            headerContainer.Add(headerLabel, contentLabel);
            
            this.Add(headerContainer);
        }
        
        public CommandLineElement SetHeader(string header)
        {
            headerLabel.element.text = $" > [{commandLineType.GetSuffix()}] {header} {DateTime.Now}";
            return this;
        }

        public CommandLineElement SetActiveContent(bool display)
        {
            contentLabel.element.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
            return this;
        }
        
        public CommandLineElement SetContent(string content)
        {
            contentLabel.element.text = content;
            return this;
        }
    }
}