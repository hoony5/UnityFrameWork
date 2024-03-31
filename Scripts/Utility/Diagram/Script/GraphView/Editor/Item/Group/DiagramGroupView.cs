using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class DiagramGroupView
    {
        public Toggle FixedToggle { get; private set; }
        public Label FixedToggleLabel { get; private set; }
        
        public DiagramGroupView()
        {
            FixedToggle = CreateToggle(false);
            FixedToggleLabel = CreateLabel("Fix Position");
            FixedToggle.SetFlexGrow(0);
            FixedToggle.SetSize(20 ,20);
            FixedToggleLabel.SetFlexGrow(0);
            FixedToggleLabel.SetFontSize(12);
            FixedToggleLabel.SetFontColor(Color.white);
            FixedToggleLabel.SetSize(100, 20);
            FixedToggleLabel.SetTextAlign(TextAnchor.MiddleCenter);
        }
    }

}