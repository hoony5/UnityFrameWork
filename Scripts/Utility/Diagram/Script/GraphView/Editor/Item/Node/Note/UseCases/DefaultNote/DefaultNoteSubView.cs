using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class DefaultNoteSubView
    {
        private NoteSectionView mainView;
        // default
        public TextField subtitle;
        private VisualElement vertical40;
        public TextField description;
        
        public DefaultNoteSubView(NoteSectionView mainView)
        {
            this.mainView = mainView;
            CreateDefaultNote();
        }

        public void CreateDefaultNote()
        {
            mainView.CreatePlain("Note", 300);

            subtitle = CreateTextField("Sub Title", "sub...");
            description = CreateTextArea("note...");

            subtitle.SetHeight(25);
            subtitle.SetFlexGrow(0);

            description.SetTextAlign(TextAnchor.UpperLeft);
            description.SetWhiteSpace(WhiteSpace.Normal);
            description.SetHeight(250);

            mainView.plain.Add(subtitle);
            mainView.plain.Add(description);
        }

        public void Reset()
        {
            mainView.plain.SafeRemove(subtitle);
            mainView.plain.SafeRemove(description);
        }
    }
}