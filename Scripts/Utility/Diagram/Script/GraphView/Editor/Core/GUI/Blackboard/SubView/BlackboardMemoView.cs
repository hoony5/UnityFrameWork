using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class BlackboardMemoView : VisualElement
    {
        public VisualElement container;
        public VisualElement headerContainer;
        public TextField titleField;
        public TextField memoAreaField;
        
        public BlackboardMemoView(string title, string memo)
        {
            container = new VisualElement();
            headerContainer = new VisualElement();
            Label titleLabel = CreateLabel("Title");
            titleField = CreateTextField(title.IsNullOrEmptyThen("title..."));
            Label memoLabel = CreateLabel("Memo");
            memoAreaField = CreateTextField(memo.IsNullOrEmptyThen("memo..."));
            
            headerContainer.Add(titleField);
            container.Add(titleLabel);
            container.CreateVerticalSpace(15);
            container.Add(headerContainer);
            container.CreateVerticalSpace(15);
            container.CreateHorizontalBar(5, Color.yellow);
            container.CreateVerticalSpace(15);
            container.Add(memoLabel);
            container.CreateVerticalSpace(15);
            container.Add(memoAreaField);
            
            container.SetFlexDirection(FlexDirection.Column);
            
            titleLabel.SetFlexGrow(0);
            titleLabel.SetHeight(25);
            titleLabel.SetFontSize(20);
            titleLabel.SetFontStyle(FontStyle.Bold);
            
            titleField.SetFlexGrow(0);
            titleField.SetWidth(300);
            titleField.SetHeight(25);
            titleField.SetFontSize(12);
            titleField.SetBorderLeftWidth(15);
            
            memoLabel.SetFlexGrow(0);
            memoLabel.SetHeight(25);
            memoLabel.SetFontSize(20);
            memoLabel.SetFontStyle(FontStyle.Bold);
            
            memoAreaField.SetFlexGrow(0);
            memoAreaField.SetHeight(200);
            memoAreaField.SetWidth(300);
            memoAreaField.SetWhiteSpace(WhiteSpace.Normal);
            memoAreaField.SetTextAlign(TextAnchor.UpperLeft);
            memoAreaField.SetBorderLeftWidth(15);
            memoAreaField.multiline = true;
            
            Add(container);
        }
        
        public void ClearValue()
        {
            titleField.value = string.Empty;
            memoAreaField.value = string.Empty;
        }
    }
}