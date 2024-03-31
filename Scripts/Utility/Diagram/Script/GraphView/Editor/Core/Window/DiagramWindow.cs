using System;
using System.Collections.Generic;
using Share;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramWindow : EditorWindow, IDisposable
    {
        public static DiagramIO IO;
        public static DiagramWindow instance;
        public DiagramGUI GUI;
        public DiagramWindowView view;
        private DiagramWindowBinder binder;
        
        public DiagramSO diagramDatabase;
        public static bool IsOpened { get; set; }

        private void CreateDiagramGUI ()
        {
            GUI = new DiagramGUI(this);
            view = new DiagramWindowView(this);
            binder = new DiagramWindowBinder(this, view);
            IO = new DiagramIO(this, view);
            EditorApplication.quitting += IO.RemoveCurrentSaveTempFile;
            GUI.StretchToParentSize();
            rootVisualElement.Add(GUI);
        }
        private void OnDisable()
        {
            EditorApplication.quitting -= IO.RemoveCurrentSaveTempFile;
        }

        [MenuItem("DDD/Diagram", priority = 1)]
        public static void OpenWindow()
        {
            Setup();
        }

        public static void OpenWindowWithoutTempDatabase()
        {
            Setup();
        }

        private static void Setup()
        {
            if(IsOpened)
            { 
                instance.Close();
                return;
            }
            instance = GetWindow<DiagramWindow>("Diagram");
            instance.minSize = new Vector2(800, 600);
            IsOpened = true;
        }
        
        public static void CloseWindow ()
        {
            instance.Close();
            IsOpened = false;
        }

        private void OnInspectorUpdate()
        {
            UpdateWindowTitle();
        }
        
        private void UpdateWindowTitle()
        {
            titleContent.text = GUI.IsNeedSavingDiagram() 
                ? "*Diagram" 
                : "Diagram";
        }

        private void CreateGUI()
        {
            CreateDiagramGUI();
            view.Create();
            binder.Bind();
            IO.SetupTempDatabase();
        }

        public void RepaintNodes()
        {
            GUI.ResetDiagrams();
            
            if(diagramDatabase.Models.Count == 0) return;
            
            LoadModels();
            LoadNodes();
            LoadMemos();
            LoadGroups();
        }

        private void LoadGroups()
        {
            foreach (KeyValuePair<string, DiagramNodeModel> node in diagramDatabase.Models)
            {
                if(node.Value.Status.GraphElementType is GraphElementType.Group)
                    _ = GUI.LoadGroup(node.Value);
            }
        }

        private void LoadMemos()
        {
            diagramDatabase.Models.ForEach(node =>
            {
                if(node.Value.Status.GraphElementType is GraphElementType.Memo)
                    _ = GUI.LoadMemo(node.Value);
            });
        }

        private void LoadModels()
        {
            foreach (KeyValuePair<string, DiagramNodeModel> node in diagramDatabase.Models)
            {
                if(node.Value.Status.GraphElementType is GraphElementType.Normal) 
                    _ = GUI.LoadNode(typeof(DiagramNormalNode), node.Value); 
                
                if(node.Value.Status.GraphElementType is GraphElementType.Event)
                    _ = GUI.LoadNode(typeof(DiagramEventNode), node.Value);
                
                if(node.Value.Status.GraphElementType is GraphElementType.Note)
                    _ = GUI.LoadNode(typeof(DiagramNoteNode), node.Value);
            }
        }

        private void LoadNodes()
        {
            foreach (DiagramNodeBase node in GUI.view.diagramNodes.Values)
            {
                node.Load();
                GUI.AddElement(node);   
            }
        }

        public void Dispose()
        {
           binder.Dispose();
           IO.RemoveCurrentSaveTempFile();
           diagramDatabase = null;
           IsOpened = false;
           instance = null;
        }
        private void OnDestroy ()
        {
            if(GUI.IsNeedSavingDiagram())
            {
                int needSave = EditorUtility.DisplayDialogComplex("Close",
                    @"Diagram window will be closed. Do you want to save current diagrams?", "yes", "no", "cancel");
                switch (needSave)
                {
                    default: // Cancel
                        return;
                    case 1: // No
                        GUI.view.diagramNodes.AddRange(GUI.view.diagramNodeToRemove);
                        GUI.view.memos.AddRange(GUI.view.memoToRemove);
                        GUI.view.groups.AddRange(GUI.view.groupToRemove);
                        
                        GUI.view.diagramNodeToCreate.ForEach(node => GUI.view.diagramNodes.SafeRemove(node.Key));
                        GUI.view.memoToCreate.ForEach(memo => GUI.view.memos.SafeRemove(memo.Key));
                        GUI.view.groupToCreate.ForEach(group => GUI.view.groups.SafeRemove(group.Key));
                            
                        IO.Flush();
                        Dispose();
                        return;
                    case 0: // Yes
                        IO.SaveAs();
                        break;
                }
            }
            Dispose();
        }
    }
}
