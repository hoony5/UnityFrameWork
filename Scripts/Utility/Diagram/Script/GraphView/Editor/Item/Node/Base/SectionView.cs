using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public abstract class SectionView
    {
        public VisualElement container { get; protected set; }
        // foldout container
        public Foldout foldout { get; protected set; }
        // main container
        public VisualElement plain { get; protected set; }
        public Label plainLabel;
        protected SectionView()
        {
            container = new VisualElement();
            container.SetFlexDirection(FlexDirection.Column);
            container.SetFlexShrink(0);
            container.SetFlexGrow(1);
        }
        
        public void CreateFoldout(string title)
        {
            if (title.IsNullOrEmpty()) return;
            foldout = Share.VisualElementEx.CreateFoldout(title);
            
        }

        public void CreatePlain(string title)
        {
            plain = new VisualElement();
            plain.name = "plain";
            plain.SetFontSize(12);
            plain.SetBorderLeftWidth(5);
            plain.SetBorderRightWidth(10);
            plain.SetTextAlign(TextAnchor.UpperLeft);
            plain.SetFlexDirection(FlexDirection.Column);
            
            plainLabel = CreateLabel(title, 10);
            plain.CreateVerticalSpace(10);
            plain.Add(plainLabel);
            
            if (title.IsNullOrEmpty()) return;
            plain.CreateVerticalSpace(10);
        }
        public void CreatePlain(string title, float height)
        {
            CreatePlain(title);
            plain.SetHeight(height);
            plain.name = "plain";
        }
    }
}