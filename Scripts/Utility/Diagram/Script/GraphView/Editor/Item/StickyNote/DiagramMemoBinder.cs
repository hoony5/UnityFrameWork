using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramMemoBinder : IDisposable
    {
        public readonly DiagramMemoView view;
        public readonly DiagramMemo memo;
        public DiagramMemoBinder(DiagramMemo memo, DiagramMemoView view)
        {
            this.memo = memo;
            this.view = view;
        }
        
        private void BindUpdatePosition()
        {
            memo.GUI.graphViewChanged -= OnPositionChanged;
            memo.GUI.graphViewChanged += OnPositionChanged;
        }
        private void DisposeUpdatePosition()
        {
            memo.GUI.graphViewChanged -= OnPositionChanged;
        }
        private GraphViewChange OnPositionChanged(GraphViewChange change)
        {
            Vector2 newPosition = memo.GetPosition().position;
            Vector2 newSize = memo.GetPosition().size;
            if (!memo.NodeModel.Header.Position.Approximately(newPosition))
            {
                memo.NodeModel.Header.Position = newPosition;
                memo.GUI.serialization.pasteCount = 0;   
            }
            
            if (!memo.NodeModel.Header.Size.Approximately(newSize))
            {
                memo.NodeModel.Header.Size = newSize;
            }
            return change;
        }
        private void BindChangeNoteColorText()
        {
            view.noteColorButton.userData = view.GetNextNoteColorText((string)view.noteColorButton.userData);
            Color noteColor = view.GetNextNoteColor((string)view.noteColorButton.userData);
            memo.contentContainer.SetBackgroundColor(noteColor);
            view.noteColorButton.SetFontColor(noteColor);
            memo.elementTypeColor = noteColor;
        }

        private void BindFontColor()
        {
            Color fontColor = view.GetSwitchFontColor((string)view.fontColorButton.userData);
            view.fontColorButton.SetFontColor(fontColor);
            memo.ChangeFontColor(fontColor);
            view.fontColorButton.userData = view.GetSwitchFontColorText((string)view.fontColorButton.userData);
        }

        private void BindTitle(ChangeEvent<string> evt)
        {
            memo.NodeModel.Note.Title = evt.newValue;
        }
        private void BindDescription(ChangeEvent<string> evt)
        {
            memo.NodeModel.Note.Description = evt.newValue;
        }
        public void Bind()
        {
            view.noteColorButton.clicked += BindChangeNoteColorText;
            view.fontColorButton.clicked += BindFontColor;
            
            view.title.RegisterValueChangedCallback(BindTitle);
            view.contents.RegisterValueChangedCallback(BindDescription);
            BindUpdatePosition();
        }

        public void Dispose()
        {
            view.noteColorButton.clicked -= BindChangeNoteColorText;
            view.fontColorButton.clicked -= BindFontColor;
            
            view.title.UnregisterValueChangedCallback(BindTitle);
            view.contents.UnregisterValueChangedCallback(BindDescription);
            DisposeUpdatePosition();
        }
    }
}