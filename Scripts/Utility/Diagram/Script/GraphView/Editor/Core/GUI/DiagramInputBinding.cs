using System;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramInputBinding : IDisposable
    {
        private DiagramGUI gui;
        public DiagramInputBinding(DiagramGUI gui)
        {
            this.gui = gui;
        }
        private void SaveCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.S || !evt.ctrlKey) return;
            DiagramWindow.IO.Save();
        }

        private void SaveAsCommand(KeyDownEvent evt)
        {
            if (!evt.shiftKey || !evt.ctrlKey || evt.keyCode != KeyCode.S) 
                return;
            DiagramWindow.IO.SaveAs();
        }

        private void CopyCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.C || !evt.ctrlKey)
                return;
            gui.serialization.Copy(CopyType.Default);
        }

        private void PasteCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.V || !evt.ctrlKey)
                return;
            gui.serialization.Paste();
        }

        private void DuplicationCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.D || !evt.ctrlKey) 
                return;
            gui.serialization.Copy(CopyType.Duplicate);
        }

        private void CutCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.X || !evt.ctrlKey) 
                return;
            gui.serialization.Copy(CopyType.Cut);
        }

        private void SelectAllCommand(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.A || !evt.ctrlKey) 
                return;
            gui.SelectAll();
        }

        private void SetUpSubmitGPTPrompt(KeyDownEvent evt)
        {
            if (!gui.gptBoard.visible) return;
            if (evt.keyCode != KeyCode.Return || !evt.shiftKey) return;
            if (gui.gptBoard.betaView.promptTextField.element.value.Trim().IsNullOrEmpty()) 
                return;

            gui.gptBoard.SubmitPrompt();
        }
        public void SetUpShotCutCommand()
        {
            gui.RegisterCallback<KeyDownEvent>(SaveCommand);
            gui.RegisterCallback<KeyDownEvent>(SaveAsCommand);
            gui.RegisterCallback<KeyDownEvent>(CopyCommand);
            gui.RegisterCallback<KeyDownEvent>(PasteCommand);
            gui.RegisterCallback<KeyDownEvent>(DuplicationCommand);
            gui.RegisterCallback<KeyDownEvent>(CutCommand);
            gui.RegisterCallback<KeyDownEvent>(SelectAllCommand);
            
            gui.gptBoard.betaView.promptTextField.element.RegisterCallback<KeyDownEvent>(SetUpSubmitGPTPrompt);
        }
        
        public void Dispose()
        {
            gui.UnregisterCallback<KeyDownEvent>(SaveCommand);
            gui.UnregisterCallback<KeyDownEvent>(SaveAsCommand);
            gui.UnregisterCallback<KeyDownEvent>(CopyCommand);
            gui.UnregisterCallback<KeyDownEvent>(PasteCommand);
            gui.UnregisterCallback<KeyDownEvent>(DuplicationCommand);
            gui.UnregisterCallback<KeyDownEvent>(CutCommand);
            gui.UnregisterCallback<KeyDownEvent>(SelectAllCommand);
            
            gui.gptBoard.betaView.promptTextField.element.UnregisterCallback<KeyDownEvent>(SetUpSubmitGPTPrompt);
        }
    }

}