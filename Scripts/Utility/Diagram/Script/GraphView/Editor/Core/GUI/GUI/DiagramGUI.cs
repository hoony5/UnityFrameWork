using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Diagram.Config;
using Share;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramGUI : GraphView, IDisposable
    {
        private readonly DiagramGUIBinder binder;
        private DiagramConfig config;
        public readonly DiagramWindow window;
        public readonly DiagramGUIView view;
        public readonly DiagramSerialization serialization;
        public readonly DiagramBlackboard blackboard;
        public readonly BlackboardGPT gptBoard;
        public DiagramConfig Config => config;
        private IVisualElementScheduledItem guiUpdate;
        
        [SerializedDictionary("Parent.ID", "ChildrenNames")]
        public SerializedDictionary<string, List<string>> inheritanceRelationships;
        
        public DiagramGUI(DiagramWindow window)
        {
            this.window = window;
            SetUpNodeConfig();
            
            view = new DiagramGUIView(this);
            blackboard = new DiagramBlackboard(this);
            serialization = new DiagramSerialization(this);
            binder = new DiagramGUIBinder(this, view);
            gptBoard = new BlackboardGPT(this, config);
            inheritanceRelationships = new SerializedDictionary<string, List<string>>();
            AddBlackboard();
            AddGPTBoard();
            
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            binder.Bind();
            OnUpdate(500);
        }

        private void AddBlackboard()
        {
            blackboard.SetFlexGrow(1);
            Add(blackboard);
            blackboard.visible = false;
        }
        private void AddGPTBoard()
        {
            gptBoard.SetFlexGrow(1);
            Add(gptBoard);
            gptBoard.visible = false;
        }

        private void OnUpdate(int time)
        {
            guiUpdate
                = schedule
                    .Execute(
                        () =>
                        {
                            if(selection.Count == 0)
                            {
                                blackboard.DisplaySimpleInfo(true);
                                return;
                            }

                            blackboard.DisplaySimpleInfo(false);
                            ISelectable diagramNode 
                                = selection
                                    .FirstOrDefault(node => node is CustomNode or DiagramMemo or DiagramGroup);
                            
                            if(diagramNode == null) return;
                            blackboard?.OnUpdate(diagramNode);
                            gptBoard?.OnUpdate(diagramNode);
                        })
                    .Every(time);
        }


        private void SetUpNodeConfig()
        {
            if(EditorPrefs.HasKey("NoteNodeConfig"))
            {
                string path = EditorPrefs.GetString("NoteNodeConfig");
                if(path.IsNullOrEmpty()) return;
                
                config = AssetDatabase.LoadAssetAtPath(path, typeof(DiagramConfig)) as DiagramConfig;
            }
          
            if(!config)
            {
                const string configRootPath = "Assets/Scripts/Utility/Diagram/Config/Editor/";
                config = ScriptableObject.CreateInstance<DiagramConfig>();
                if(!Directory.Exists(configRootPath))
                {
                    Directory.CreateDirectory(configRootPath);
                }
                AssetDatabase.CreateAsset(config, Path.Combine(configRootPath, "Config.asset"));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorPrefs.SetString("NoteNodeConfig", AssetDatabase.GetAssetPath(config));
        }
        public void BindOpenConfigPathPanel()
        {
            // open SO
            string path = EditorUtility.OpenFilePanel("Open Config File", "Assets/", "asset"); 
            
            DiagramConfig asset = AssetDatabase.LoadAssetAtPath(path, typeof(DiagramConfig)) as DiagramConfig;
            if (asset == null)
            {
                Debug.LogError("Failed to load config file");
                return;
            }
            // load config
            config = asset;
            EditorPrefs.SetString("NoteNodeConfig", AssetDatabase.GetAssetPath(config));
        }

        public Vector2 GetGraphMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePos = mousePosition;
            if(isSearchWindow) worldMousePos -= window.position.position;
            Vector2 localMousePos = contentViewContainer.WorldToLocal(worldMousePos);

            return localMousePos;
        }

        private bool IsNeedSavingDiagramStatus()
        {
            return view.diagramNodes.Values.Any(node => node.NodeModel.Status.NeedSavingDiagram) ||
                view.memos.Values.Any(memo => memo.NodeModel.Status.NeedSavingDiagram) ||
                view.groups.Values.Any(group => group.NodeModel.Status.NeedSavingDiagram);
        }

        private bool IsNeedSavingCreatedDiagram()
        {
            return view.diagramNodeToCreate.Count != 0 || view.memoToCreate.Count != 0 || view.groupToCreate.Count != 0;
        }
        private bool IsNeedSavingRemovedDiagram()
        {
            return view.diagramNodeToRemove.Count != 0 || view.memoToRemove.Count != 0 || view.groupToRemove.Count != 0;
        }
        public bool IsNeedSavingDiagram()
        {
            return IsNeedSavingDiagramStatus() || IsNeedSavingCreatedDiagram() || IsNeedSavingRemovedDiagram();
        }

        public void ClearDirty()
        {
            view.diagramNodes.Values.ForEach(node => node.NodeModel.Status.NeedSavingDiagram = false);
            view.memos.Values.ForEach(memo => memo.NodeModel.Status.NeedSavingDiagram = false);
            view.groups.Values.ForEach(group => group.NodeModel.Status.NeedSavingDiagram = false);
        }
        public DiagramMemo CreateMemo(string memoName, Vector2 position)
        {
            DiagramMemo memo = new DiagramMemo(this, new DiagramNodeModel(memoName, position));
            memo.SetUp();
            view.memos.AddOrUpdate(memo.NodeModel.Header.ID, memo);
            memo.GUI.view.memoToCreate.Add(memo.ID, memo);
            AddElement(memo);
            return memo;
        }
        public DiagramMemo LoadMemo(DiagramNodeModel nodeModel)
        {
            DiagramMemo memo = new DiagramMemo(this, nodeModel);
            memo.SetUp();
            memo.Load();
            view.memos.AddOrUpdate(memo.NodeModel.Header.ID, memo);
            AddElement(memo);
            return memo;
        }
        public DiagramGroup CreateGroup(string group, Vector2 mousePosition)
        {
            DiagramGroup diagramGroup = new DiagramGroup(this, new DiagramNodeModel(group, mousePosition));
            diagramGroup.SetUp();
            view.groups.AddOrUpdate(diagramGroup.NodeModel.Header.ID, diagramGroup);
            diagramGroup.GUI.view.groupToCreate.Add(diagramGroup.ID, diagramGroup);
            AddElement(diagramGroup);
            return diagramGroup;
        }
        public DiagramGroup LoadGroup(DiagramNodeModel nodeModel)
        {
            DiagramGroup diagramGroup = new DiagramGroup(this, nodeModel);
            diagramGroup.SetUp();
            diagramGroup.Load();
            view.groups.AddOrUpdate(diagramGroup.NodeModel.Header.ID, diagramGroup);
            AddElement(diagramGroup);
            return diagramGroup;
        }
        
        public DiagramNodeBase CreateNode (Type type, string nodeName, Vector2 startPosition)
        {
            DiagramNodeBase node;
            if(type == typeof(DiagramNormalNode))
            {
                node = new DiagramNormalNode(this, new DiagramNodeModel(nodeName, startPosition));
            }
            else if(type == typeof(DiagramEventNode))
            {
                node = new DiagramEventNode(this, new DiagramNodeModel(nodeName, startPosition));
            }
            else if(type == typeof(DiagramNoteNode))
            {
                node = new DiagramNoteNode(this, new DiagramNodeModel(nodeName, startPosition));
            }
            else
            {
                return null;
            }
            node.SetUp();
            node.GUI.view.diagramNodeToCreate.Add(node.ID, node);
            view.diagramNodes.AddOrUpdate(node.NodeModel.Header.ID, node);      
            return node;
        }

        public DiagramNodeBase LoadNode(Type type, DiagramNodeModel nodeModel) 
        {
            DiagramNodeBase node;
            if (type == typeof(DiagramNormalNode))
            {
                node = new DiagramNormalNode(this, nodeModel);
            }
            else if (type == typeof(DiagramEventNode))
            {
                node = new DiagramEventNode(this, nodeModel);
            }
            else if (type == typeof(DiagramNoteNode))
            {
                node = new DiagramNoteNode(this, nodeModel);
            }
            else
            {
                return default;
            }

            node.SetUp();
            view.diagramNodes.AddOrUpdate(nodeModel.Header.ID, node);
            return node;
        }

        public void SelectAll()
        {
            ClearSelection();
            graphElements
                .Where(element => element is IDiagramNode 
                    or DiagramGroup 
                    or Edge  
                    or DiagramMemo)
                .ForEach(AddToSelection);
        }

        public void ToggleBlackboard()
        {
            blackboard.visible = !blackboard.visible;
            gptBoard.visible = false;
        }
        public void ToggleGPT()
        {
            blackboard.visible = false;
            gptBoard.visible = !gptBoard.visible;
        }
        public override List<Port> GetCompatiblePorts (Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if(startPort == port) return;
                if(startPort.node == port.node) return;
                if(startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        private void RemoveNode(Action<DiagramNodeBase> callback)
        {
            inheritanceRelationships.Clear();
            if (view.diagramNodes.Count != 0)
            {
                DeleteElements(view.diagramNodes.Values.Cast<GraphElement>().ToList());
                foreach (CustomNode customNode in view.diagramNodes.Values)
                {
                    if (customNode is not DiagramNodeBase node) continue;
                    callback?.Invoke(node);
                }
            }
            graphElements.ForEach(RemoveElement);
        }
        public void ResetDiagrams()
        {
            RemoveNode(node => view.diagramNodeToRemove.AddOrUpdate(node.ID, node));
            EditorUtility.SetDirty(window.diagramDatabase);
        }
        public void ClearDiagrams()
        {
            window.diagramDatabase.Clear();
            inheritanceRelationships.Clear();
            
            RemoveNode(node => node.Dispose());
            
            view.diagramNodeToRemove.Clear();
            view.groupToRemove.Clear();
            view.memoToRemove.Clear();
            
            view.diagramNodes.Clear();
            view.groups.Clear();
            view.memos.Clear();
            
            EditorUtility.SetDirty(window.diagramDatabase);
        }

        public void ToggleMinimap()
        {
            view.minimap.visible = !view.minimap.visible;
        }
        public void OpenDatabase()
        {
            EditorWindow.CreateWindow<DatabaseWindow>(typeof(DiagramWindow)).diagramWindow = window;
        }

        public void CollectCopyableElements(IEnumerable<GraphElement> elements, HashSet<GraphElement> copyableElements)
        {
            base.CollectCopyableGraphElements(elements, copyableElements);
        }
        public void CreateInheritanceMapFrom(UnityEngine.Object folder)
        {
            if(folder == null)
            {
                Debug.LogError("Folder is null");
                return;
            }
        
            string path = AssetDatabase.GetAssetPath(folder);
            if(path.IsNullOrEmpty() ||
               !AssetDatabase.IsValidFolder(path))
            {
                return;
            }
        
            inheritanceRelationships.Clear();
            string[] guids = AssetDatabase.FindAssets("t:Script", new[] {path});
            foreach(string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
                Type scriptType = monoScript.GetClass();

                if (scriptType == null) continue;
                Type baseType = scriptType.BaseType;
                if (baseType == null) continue;
                
                if(!inheritanceRelationships.ContainsKey(baseType.Name))
                {
                    inheritanceRelationships.Add(baseType.Name, new List<string> { scriptType.Name });
                }
                else
                {
                    if(!inheritanceRelationships[baseType.Name].Contains(scriptType.Name))
                    {
                        inheritanceRelationships[baseType.Name].Add(scriptType.Name);
                    }
                }
            }
        }
        
        public void Dispose()
        {
            guiUpdate.Pause();
            guiUpdate = null;
            window.Dispose();
            binder.Dispose();
        }
    }
}
