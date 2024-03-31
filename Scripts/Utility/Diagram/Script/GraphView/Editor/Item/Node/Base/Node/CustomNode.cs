using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public abstract class CustomNode : Node, IDiagramNode
    {
        public string ID { get; set; }
        public DiagramGroup Group { get; set;}
        public virtual SectionFactory SectionFactory { get; protected set; }
        public DiagramNodeModel NodeModel { get; protected set;}
        public DiagramModelModifier ModelModifier { get; protected set;}
        public DiagramModelReflection ModelReflection { get; protected set;}
        public VisualElement MainContainer => mainContainer;
        public DiagramGUI GUI { get; protected set;}
        
        protected CustomNode(DiagramGUI gui, DiagramNodeModel nodeModel) 
        {
            GUI = gui;
            NodeModel = new DiagramNodeModel(nodeModel);
            ID = NodeModel.Header.ID;
            ModelReflection = new DiagramModelReflection(NodeModel);
            ModelModifier = new DiagramModelModifier(NodeModel);
            userData = NodeModel;
            RegisterOnGraphChanged(OnPositionChanged);
            Color extensionBackgroundColor = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1);
            Color mainBackgroundColor = new Color(0.1647059f, 0.1647059f, 0.1647059f, 1);
            //extensionContainer.style.backgroundImage = new StyleBackground(Texture2D.whiteTexture);
            extensionContainer.style.backgroundColor = extensionBackgroundColor;
            mainContainer.style.backgroundColor = mainBackgroundColor;
        }

        private GraphViewChange OnPositionChanged(GraphViewChange change)
        {
            Vector2 newPosition = GetPosition().position;
            Vector2 newSize = GetPosition().size;
            if (!NodeModel.Header.Position.Approximately(newPosition))
            {
                NodeModel.Header.Position = newPosition;
                GUI.serialization.pasteCount = 0;   
            }
            
            if (!NodeModel.Header.Size.Approximately(newSize))
            {
                NodeModel.Header.Size = newSize;
            }
            return change;
        }

        public void RegisterOnGraphChanged(Func<GraphViewChange, GraphViewChange> changed)
        {
            GUI.graphViewChanged -= changed.Invoke;
            GUI.graphViewChanged += changed.Invoke;
        }
        public void UnregisterOnGraphChanged(Func<GraphViewChange, GraphViewChange> changed)
        {
            GUI.graphViewChanged -= changed.Invoke;
        }
        public virtual void SetUp()
        {
            extensionContainer.SetFlexGrow(1);
            titleContainer.SetBorderWidth(6);
            SectionFactory.Setup();
            RefreshExpandedState();
            MarkDirtyRepaint();
        }

        private string ToDepthString(IDictionary<int , List<string>> dictionary)
        {
            if (dictionary.Count == 0) return string.Empty;

            IEnumerable<string> list = dictionary
                .Select(item => $@"[Depth ({item.Key})]
{string.Join('\n', item.Value)}
");
            
            return string.Join('\n', list);
        }

        private void SetBaseNoteDescription(int level, IDictionary<int, List<string>> noteDictionary)
        {
            if(noteDictionary.ContainsKey(level))
            {
                noteDictionary[level].Add($"{noteDictionary[level].Count + 1}.{NodeModel.Note.Title} : {NodeModel.Header.Description}");
            }
            else
            {
                noteDictionary.Add(level, new List<string>{ $"1.{NodeModel.Note.Title} : {NodeModel.Header.Description}" });
            }
            
            if (NodeModel.NoteNodes.Count == 0) return;
            
            NodeModel.NoteNodes.ForEach(note =>
            {
                if(!GUI.view.diagramNodes.TryGetValue(note.ID, out DiagramNodeBase noteModel)) return;
                if(noteModel is not DiagramNoteNode noteNode) return;
                noteNode.SetBaseNoteDescription(level + 1, noteDictionary);
            });
        }
        public virtual string GetDescription()
        {
            SerializedDictionary<int, List<string>> noteDictionary 
                = new SerializedDictionary<int, List<string>>();
    
            // self
            noteDictionary.Add(0, new List<string>{ $"1.{NodeModel.Note.Title.IsNullOrEmptyThen(NodeModel.Header.Name)} : {NodeModel.Header.Description}" });
            
            if (NodeModel.NoteNodes.Count == 0) return ToDepthString(noteDictionary);
            
            NodeModel.NoteNodes.ForEach(note =>
            {
                if(!GUI.view.diagramNodes.TryGetValue(note.ID, out DiagramNodeBase noteModel)) return;
                if(noteModel is not DiagramNoteNode noteNode) return;
                noteNode.SetBaseNoteDescription(1, noteDictionary);
            });
            
            return ToDepthString(noteDictionary);
        }

        public virtual void Load()
        {
            SectionFactory.Load();
            RefreshExpandedState();
            
            NodeModel.Status.NeedSavingDiagram = false;
        }
        public virtual void Dispose()
        {
        }

        public virtual void SetupContainerHierarchy()
        {
            
        }

        public virtual void RepaintAllSections()
        {
            
        }

        public virtual void Reload()
        {
            
        }
    }
}