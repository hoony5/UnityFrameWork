using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramGroupBinder : IDisposable
    {
        private DiagramGroup group;
        private DiagramGroupView view;
        
        public DiagramGroupBinder(DiagramGroup group, DiagramGroupView view)
        {
            this.group = group;
            this.view = view;
        }

        private void BindUpdatePosition()
        {
            group.GUI.graphViewChanged -= OnPositionChanged;
            group.GUI.graphViewChanged += OnPositionChanged;
        }
        private void DisposeUpdatePosition()
        {
            group.GUI.graphViewChanged -= OnPositionChanged;
        }
        private GraphViewChange OnPositionChanged(GraphViewChange change)
        {
            Vector2 newPosition = group.GetPosition().position;
            Vector2 newSize = group.GetPosition().size;
            if (!group.NodeModel.Header.Position.Approximately(newPosition))
            {
                group.NodeModel.Header.Position = newPosition;
                group.NodeModel.Members.ForEach(member =>
                {
                    member.Position = newPosition;
                });
                group.GUI.serialization.pasteCount = 0;   
            }
            
            if (!group.NodeModel.Header.Size.Approximately(newSize))
            {
                group.NodeModel.Header.Size = newSize;
                group.NodeModel.Members.ForEach(member =>
                {
                    member.Size = newSize;
                });
            }
            return change;
        }

        private void ChangeFixedPositionValue(ChangeEvent<bool> evt)
        {
            group.IsFixed = evt.newValue;
        }

        private void BindFixedPosition()
        {
            view.FixedToggle.RegisterValueChangedCallback(ChangeFixedPositionValue);
        }

        private void DisposeFixedPosition()
        {
            view.FixedToggle.UnregisterValueChangedCallback(ChangeFixedPositionValue);
        }
        
        public void Bind()
        {
            BindFixedPosition();
            BindUpdatePosition();
        }
        
        public void Dispose()
        {
            DisposeFixedPosition();
            DisposeUpdatePosition();
        }
    }

}