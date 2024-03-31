using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class LogSectionView : SectionView
    {
        public readonly Label logLabel;
        public TextField noteField;
        
        public LogSectionView(string title)
        {
            CreateFoldout(title);
            
            logLabel = CreateLabel(string.Empty);
            noteField = CreateTextField("note...");
            noteField.multiline = true;
            
            foldout.Add(container);
            container.Add(noteField);
            container.CreateHorizontalSpace(10);
            container.Add(logLabel);
            container.CreateHorizontalSpace(10);
            
            logLabel.SetTextAlign(TextAnchor.UpperLeft);
            noteField.SetTextAlign(TextAnchor.UpperLeft);
            noteField.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
            noteField.SetHeight(150);

            foldout.SetWidth(container.style.width);
            foldout.SetHeight(container.style.height);
            
            container.SetFlexDirection(FlexDirection.Column);
            container.SetBorderWidth(12);
            container.SetFlexGrow(1);
            container.SetFlexShrink(1);
        }
    }
}