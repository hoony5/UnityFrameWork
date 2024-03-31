using System;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardMemoBinder : IDisposable
    {
        private BlackboardMemoView view;
        public DiagramMemo memo;
        
        public BlackboardMemoBinder(BlackboardMemoView view)
        {
            this.view = view;
        }

        public void SetUp(DiagramMemo diagramMemo)
        {
            memo = diagramMemo;
        }
        private void BindMemoNodeDescription(ChangeEvent<string> evt)
        {
            memo.NodeModel.Note.Description = evt.newValue;
        }

        private void BindMemoNodeTitle(ChangeEvent<string> evt)
        {
            memo.NodeModel.Note.Title = evt.newValue;
        }
        public void Bind()
        {
            view.titleField.RegisterValueChangedCallback(BindMemoNodeTitle);
            view.memoAreaField.RegisterValueChangedCallback(BindMemoNodeDescription);
        }

        public void Dispose()
        {
            view.titleField.UnregisterValueChangedCallback(BindMemoNodeTitle);
            view.memoAreaField.UnregisterValueChangedCallback(BindMemoNodeDescription);
        }

        public void Load()
        {
            view.memoAreaField.value = memo.NodeModel.Note.Description;
            view.titleField.value = memo.NodeModel.Note.Title;
        }
    }
}