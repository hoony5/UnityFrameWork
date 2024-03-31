using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.SourceGenerator;
using Writer.SourceGenerator.Format.Writer;
using Directory = UnityEngine.Windows.Directory;

namespace Diagram
{
    public class DiagramWindowBinder : IDisposable
    {
        private DiagramWindow window;
        private DiagramWindowView view;
        
        public DiagramWindowBinder(DiagramWindow window, DiagramWindowView view)
        {
            this.window = window;
            this.view = view;
        }
        private void SetTitleTextField(ChangeEvent<string> evt)
        {
            view.titleTextField.value = evt.newValue;
        }
        private void CreateDDDDirectory(DDDGeneratorSettings settings)
        {
            if(!Directory.Exists(Path.Combine(settings.EntityRootPath, $"Aggregate")))
            {
                Directory.CreateDirectory(settings.EntityRootPath);
            }
            if(!Directory.Exists(Path.Combine(settings.EntityRootPath, $"ValueObject")))
            {
                Directory.CreateDirectory(settings.EntityRootPath);
            }
            if(!Directory.Exists(Path.Combine(settings.OrderSORootPath)))
            {
                Directory.CreateDirectory(settings.OrderSORootPath);
            }
            if(!Directory.Exists(Path.Combine(settings.OrderSOEditorRootPath)))
            {
                Directory.CreateDirectory(settings.OrderSOEditorRootPath);
            }
        }

        private void GenerateDDD(DiagramNodeModel nodeModel, DDDGeneratorSettings dddSettings)
        {
            CreateDDDDirectory(dddSettings);
            

            if (nodeModel.Header.EntityType == EntityType.Aggregate)
            {
                DDDAggregateWriter aggregateWriter = new DDDAggregateWriter(nodeModel); // aggregate
                aggregateWriter.Write(Path.Combine(dddSettings.EntityRootPath,
                    $"Aggregate/{nodeModel.Header.Name}.cs"));

                DDDOrderSOWriter orderSoWriter = new DDDOrderSOWriter(nodeModel); // aggregate order so
                orderSoWriter.Write(Path.Combine(dddSettings.OrderSORootPath,
                    $"{nodeModel.Header.Name}OrderSO.cs"));

                DDDOrderSOEditorWriter
                    orderSoEditorWriter = new DDDOrderSOEditorWriter(nodeModel); // aggregate order so editor
                orderSoEditorWriter.Write(Path.Combine(dddSettings.OrderSOEditorRootPath,
                    $"{nodeModel.Header.Name}OrderEditorSO.cs"));

                DDDRepositorySoWriter
                    repositorySoWriter = new DDDRepositorySoWriter(nodeModel); // aggregate repository
                repositorySoWriter.Write(Path.Combine(dddSettings.RepositorySORootPath,
                    $"{nodeModel.Header.Name}RepositorySO.cs"));
            }

            if (nodeModel.Header.EntityType != EntityType.ValueObject) return;
            
            DDDValueObjectWriter valueObjectWriter = new DDDValueObjectWriter(nodeModel); // value object
            valueObjectWriter.Write(Path.Combine(dddSettings.EntityRootPath,
                "ValueObject",$"{nodeModel.Header.Name}.cs"));
        }

        private void CreateScriptDirectory(ScriptGeneratorSettings settings)
        {
            if (!Directory.Exists(settings.ModelRootPath))
            {
                Directory.CreateDirectory(settings.ModelRootPath);
            }
        } 

        private void GenerateNormalScript(DiagramNodeModel nodeModel, ScriptGeneratorSettings settings)
        {
            if(nodeModel.Status.GraphElementType != GraphElementType.Normal) return;
            
            CreateScriptDirectory(settings);
            
            NormalScriptWriter scriptWriter = new NormalScriptWriter(nodeModel, settings);
            scriptWriter.Write(settings.ModelRootPath);
        }

        private void GenerateScripts()
        {
            if (window.GUI.view.diagramNodes.Count == 0) return;

            DDDGeneratorSettings dddSettings = new DDDGeneratorSettings();
            ScriptGeneratorSettings scriptSettings = new ScriptGeneratorSettings();

            foreach (KeyValuePair<string, DiagramNodeBase> node in window.GUI.view.diagramNodes)
            {
                if(node.Value.NodeModel.Header.EntityType is
                   EntityType.ValueObject or EntityType.Aggregate)
                {
                    GenerateDDD(node.Value.NodeModel, dddSettings);
                    continue;
                }
                
                if (node.Value.NodeModel.Header.EntityType == EntityType.None)
                {
                    GenerateNormalScript(node.Value.NodeModel, scriptSettings);
                }
            }
        }
        private void ToggleMinimap ()
        {
            window.GUI.ToggleMinimap();
        }

        private void Clear ()
        {
            bool isConfirmed = EditorUtility.DisplayDialog(
                "Clear Diagram",
                "Are you sure you want to clear the diagram? It will be removed on assets.",
                "Yes", "No");
            if (!isConfirmed) return;
            window.GUI?.ClearDiagrams();
            
            window.GUI?.gptBoard.viewModel.DrawTreeView();
            window.GUI?.gptBoard.betaView.nodeTreeView.element.Rebuild();
        }

        private void Reset ()
        {
            int needSave = EditorUtility.DisplayDialogComplex(
                "Reset",
                @"Diagram window will be reset. Do you want to save current diagrams?",
                "yes", "no", "cancel");

            switch (needSave)
            {
                case 2: // Cancel
                    return;
                case 1: // No
                    window.GUI?.ResetDiagrams();
                    window.GUI?.gptBoard.viewModel.DrawTreeView();
                    window.GUI?.gptBoard.betaView.nodeTreeView.element.Rebuild();
                    return;
                default: // Yes
                    DiagramWindow.IO.Save();
                    return;
            }
        }

        public void Test()
        {
        }
        public void TestRegister()
        {
        }
        public void Bind()
        {
            view.titleTextField.RegisterValueChangedCallback(SetTitleTextField);
            view.blackboardButton.clicked += window.GUI.ToggleBlackboard;
            view.gptButton.clicked += window.GUI.ToggleGPT;
            view.databaseButton.clicked += window.GUI.OpenDatabase;
            
            view.saveButton.clicked += DiagramWindow.IO.Save;
            view.saveAsButton.clicked += DiagramWindow.IO.SaveAs;
            view.loadButton.clicked += DiagramWindow.IO.Load;
            view.generateButton.clicked += GenerateScripts;
            view.minimapButton.clicked += ToggleMinimap;
            view.resetButton.clicked += Reset;
            view.clearButton.clicked += Clear;
            view.testButton.clicked += Test;
            view.testRegisterButton.clicked += TestRegister;
        }
        
        public void Dispose()
        {
            view.titleTextField.UnregisterValueChangedCallback(SetTitleTextField);
            view.blackboardButton.clicked -= window.GUI.ToggleBlackboard;
            view.gptButton.clicked -= window.GUI.ToggleGPT;
            view.databaseButton.clicked -= window.GUI.OpenDatabase;
            
            view.saveButton.clicked -= DiagramWindow.IO.Save;
            view.saveAsButton.clicked -= DiagramWindow.IO.SaveAs;
            view.loadButton.clicked -= DiagramWindow.IO.Load;
            view.generateButton.clicked -= GenerateScripts;
            view.minimapButton.clicked -= ToggleMinimap;
            view.resetButton.clicked -= Reset;
            view.clearButton.clicked -= Clear;
            view.testButton.clicked -= Test;
            view.testRegisterButton.clicked -= TestRegister;
            view.toolbar.RemoveFromHierarchy();
        }
    }

}