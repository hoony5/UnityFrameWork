using AYellowpaper.SerializedCollections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramGUIView
    {
        private DiagramGUI gui;
        public SearchWindow searchWindow;
        public MiniMap minimap;
        
        // all nodes
        [SerializedDictionary("ID", "Node")] 
        public SerializedDictionary<string, DiagramNodeBase> diagramNodes;
        // all nodes to remove
        public SerializedDictionary<string, DiagramNodeBase> diagramNodeToCreate;
        public SerializedDictionary<string, DiagramNodeBase> diagramNodeToRemove;
        
        // all group nodes
        [SerializedDictionary("ID", "Node")]
        public SerializedDictionary<string, DiagramGroup> groups;
        public SerializedDictionary<string, DiagramGroup> groupToCreate;
        public SerializedDictionary<string, DiagramGroup> groupToRemove;
        
        // all memo nodes
        [SerializedDictionary("ID", "Node")] 
        public SerializedDictionary<string, DiagramMemo> memos;
        public SerializedDictionary<string, DiagramMemo> memoToCreate;
        public SerializedDictionary<string, DiagramMemo> memoToRemove;
        
        public DiagramGUIView(DiagramGUI gui)
        {
            this.gui = gui;
            diagramNodes = new SerializedDictionary<string, DiagramNodeBase>();
            diagramNodeToRemove = new SerializedDictionary<string, DiagramNodeBase>();
            diagramNodeToCreate = new SerializedDictionary<string, DiagramNodeBase>();
            
            groups = new SerializedDictionary<string, DiagramGroup>();
            groupToRemove = new SerializedDictionary<string, DiagramGroup>();
            groupToCreate = new SerializedDictionary<string, DiagramGroup>();
            
            memos = new SerializedDictionary<string, DiagramMemo>();
            memoToRemove = new SerializedDictionary<string, DiagramMemo>();
            memoToCreate = new SerializedDictionary<string, DiagramMemo>();
            
            AddGridBG();
            AddMinimap();
            AddSearchWindow();
            AddMinimapStyles();
        }

        private void AddGridBG ()
        {
            GridBackground gridBG = new GridBackground();
            gridBG.StretchToParentSize();

            gui.Insert(0, gridBG);
        }
        private void AddMinimapStyles ()
        {
            StyleColor bgColor = new StyleColor(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new StyleColor(new Color32(51, 51, 51, 255));

            minimap.style.backgroundColor = bgColor;
            minimap.style.borderTopColor = borderColor;
            minimap.style.borderBottomColor = borderColor;
            minimap.style.borderLeftColor = borderColor;
            minimap.style.borderRightColor = borderColor;
        }

        private void AddSearchWindow ()
        {
            if(searchWindow == null)
            {
                searchWindow = ScriptableObject.CreateInstance<SearchWindow>();
                searchWindow.Init(gui);
            }
            
            gui.nodeCreationRequest = 
                context => UnityEditor.Experimental.GraphView
                    .SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        private void AddMinimap()
        {
            minimap = new MiniMap();
            minimap.SetPosition(new Rect(15, 50, 200, 180));
            gui.Add(minimap);
        }
    }
}