using System;
using Share;
using UnityEngine.UIElements;

namespace Diagram
{
    public class CheckListNoteSubViewBinder : IDisposable
    {
        private IDiagramNode node;
        private CheckListNoteSubView view;
        public CheckListNoteSubViewBinder(IDiagramNode node, CheckListNoteSubView view)
        {
            this.node = node;
            this.view = view;
        }
        
        private void AddCheckBox()
        {
            CheckList checkList = view.CreateCheckBox();
            view.checkBoxes.Add(checkList);
            view.CheckListContainer.Add(checkList);
            
            node.NodeModel.Note.CheckList.Add(new CheckListInfo
            {
                Id = view.checkBoxes.Count - 1,
                toggleValue = false,
                Description = ""
            });
            
            checkList.index = view.checkBoxes.Count - 1;
            BindCheckList(checkList);
        }
        public void LoadCheckBox(CheckListInfo info)
        {
            CheckList checkList = view.CreateCheckBox(info.toggleValue, info.Description);
            view.checkBoxes.Add(checkList);
            view.CheckListContainer.Add(checkList);
            checkList.index = view.checkBoxes.Count - 1;
            
            BindCheckList(checkList);
        }

        private void RemoveCheckBox(CheckList checkList)
        {
            if(checkList.index < 0) return;
            if(checkList.index >= node.NodeModel.Note.CheckList.Count) return;
            if(!view.checkBoxes.Contains(checkList)) return;
            
            DisposeCheckList(checkList);
            // on data structure
            node.NodeModel.Note.CheckList.RemoveAt(checkList.index);
            view.checkBoxes.Remove(checkList);
            // on ui
            checkList.RemoveFromHierarchy();
            
            // reindex
            view.checkBoxes.For((reIndex, checkBox) => checkBox.index = reIndex);
        }
        
        public void ClearCheckBoxes()
        {
            foreach (CheckList checkBox in view.checkBoxes)
            {
                DisposeCheckList(checkBox);
                checkBox.RemoveFromHierarchy();
            }
            view.checkBoxes.Clear();
            node.NodeModel.Note.CheckList.Clear();
        }

        private void BindCheckBox(int index, bool value)
        {
            if (index < 0) return;
            node.NodeModel.Note.CheckList[index].toggleValue = value;
        }

        private void BindDescription(int index, string value)
        {
            if(index < 0) return;
            node.NodeModel.Note.CheckList[index].Description = value;
        }
        
        private void BindCheckList(CheckList checkList)
        {
            checkList.toggle.RegisterValueChangedCallback(evt => BindCheckBox(view.checkBoxes.IndexOf(checkList), evt.newValue));
            checkList.descriptionField.RegisterCallback<FocusOutEvent>( _ => BindDescription(view.checkBoxes.IndexOf(checkList), checkList.descriptionField.value));
            checkList.removeButton.clicked += () => RemoveCheckBox(checkList);
        }
        private void DisposeCheckList(CheckList checkList)
        {
            checkList.toggle.UnregisterValueChangedCallback(evt => BindCheckBox(view.checkBoxes.IndexOf(checkList), evt.newValue));
            checkList.descriptionField.UnregisterCallback<FocusOutEvent>( _ => BindDescription(view.checkBoxes.IndexOf(checkList), checkList.descriptionField.value));
        }
        
        public void Bind()
        {
            view.addCheckListButton.clicked += AddCheckBox;
        }
        
        public void Dispose()
        {
            view.addCheckListButton.clicked -= AddCheckBox;
            view.checkBoxes.ForEach(checkList =>
            {
                checkList.toggle.UnregisterValueChangedCallback(evt => BindCheckBox(view.checkBoxes.IndexOf(checkList), evt.newValue));
                checkList.descriptionField.UnregisterCallback<FocusOutEvent>( _ => BindDescription(view.checkBoxes.IndexOf(checkList), checkList.descriptionField.value));
            });
        }
    }
}