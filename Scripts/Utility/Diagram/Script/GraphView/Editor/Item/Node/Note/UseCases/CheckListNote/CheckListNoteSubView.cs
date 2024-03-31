using System.Collections.Generic;
using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class CheckListNoteSubView
    {
        private NoteSectionView mainView;
        // check list
        private VisualElement checkListTopContainer;
        private VisualElement checkListContainer;
        public VisualElement CheckListContainer => checkListContainer;
        public Button addCheckListButton;
        
        public readonly List<CheckList> checkBoxes;
        
        public CheckListNoteSubView(NoteSectionView mainView)
        {
            this.mainView = mainView;
            checkBoxes = new List<CheckList>();
            CreateCheckList();
        }
        
        public CheckList CreateCheckBox()
        {
            return new CheckList();
        }
        
        public CheckList CreateCheckBox(bool value, string description)
        {
            return new CheckList(value, description);
        }
        
        public void CreateCheckList()
        {
            mainView.CreateFoldout("CheckList");
            checkListContainer = new VisualElement();
            checkListTopContainer = new VisualElement();
            
            addCheckListButton = CreateButton("+");

            // add more ? or just three labels.
            Label checkBoxHeader = CreateLabel("Done");
            Label descriptionHeader = CreateLabel("Description");

            VisualElement headerContainer = new VisualElement();

            headerContainer.Add(checkBoxHeader);
            headerContainer.Add(descriptionHeader);

            checkListTopContainer.Add(addCheckListButton);
            checkListTopContainer.CreateVerticalSpace(10);
            checkListTopContainer.Add(headerContainer);
            checkListTopContainer.CreateVerticalSpace(10);
            checkListContainer.Add(checkListTopContainer);

            // headers
            checkBoxHeader.SetHeight(20);
            descriptionHeader.SetHeight(20);
            headerContainer.SetTextAlign(TextAnchor.MiddleLeft);

            // align direction
            headerContainer.SetFlexDirection(FlexDirection.Row);
            checkListTopContainer.SetFlexDirection(FlexDirection.Column);
            checkListContainer.SetFlexDirection(FlexDirection.Column);
            checkListContainer.SetBorderRightWidth(20);

            headerContainer.SetFlexGrow(0);

            mainView.foldout.Add(checkListContainer);
        }
        
        public void Reset()
        {
            mainView.foldout.SafeRemove(checkListContainer);
        }
    }

}