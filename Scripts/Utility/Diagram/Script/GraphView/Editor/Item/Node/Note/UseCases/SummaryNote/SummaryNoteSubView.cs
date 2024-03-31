using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class SummaryNoteSubView
    {
        private NoteSectionView mainView;
        // plain
        public TextField summary;
        public TextField detail;
        
        public SummaryNoteSubView(NoteSectionView mainView)
        {
            this.mainView = mainView;
            CreateSummaryNote();
            CreateDetailNote();
        }

        private void CreateSummaryNote()
        {
            mainView.CreatePlain("Summary", 100);
            summary = CreateTextArea("summary...", null, 65);
            
            summary.SetTextAlign(TextAnchor.UpperLeft);
            summary.SetFlexGrow(0);
            summary.SetWhiteSpace(WhiteSpace.Normal);
            summary.SetWidth(280);
            summary.SetHeight(65);
            
            mainView.plain.Add(summary);
        }

        private void CreateDetailNote()
        {
            mainView.CreateFoldout("Detail");
            detail = CreateTextArea("detail...");
            
            detail.SetTextAlign(TextAnchor.UpperLeft);
            detail.SetFlexGrow(0);
            detail.SetWhiteSpace(WhiteSpace.Normal);
            detail.SetWidth(260);
            detail.SetHeight(100);
            
            mainView.foldout.Add(detail);
        }
        
        public void Reset()
        {
            mainView.plain.SafeRemove(summary);
            mainView.foldout.SafeRemove(detail);
        }
    }

}