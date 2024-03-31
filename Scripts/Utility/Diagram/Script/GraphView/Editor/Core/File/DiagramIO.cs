using System.Collections.Generic;
using System.IO;
using Share;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Diagram
{
    public class DiagramIO
    {
        private DiagramWindow window;
        private readonly DiagramWindowView view;

        private string tempPath = "Assets/Scripts/Utility/Diagram/Temp/Editor";
        private string tempFilePath => $"{tempPath}/{window.GetInstanceID()}.asset";
        
        public DiagramIO(DiagramWindow window, DiagramWindowView view)
        {
            this.window = window;
            this.view = view;
        }
        public bool IsTempDatabase()
        {
            return window.diagramDatabase != null &&
                   !string.IsNullOrEmpty(window.diagramDatabase.OldPath) &&
                   window.diagramDatabase.OldPath.Contains(tempPath);
        }
        public void SetupTempDatabase(string oldPath = "")
        {
            if (window.diagramDatabase)
            {
                window.diagramDatabase.OldPath = AssetDatabase.GetAssetPath(window.diagramDatabase);
            }
            
            if (IsTempDatabase()) return;
            
            window.diagramDatabase = ScriptableObject.CreateInstance<DiagramSO>();
            window.diagramDatabase.Init();
            
            if(!Directory.Exists(tempPath))
            {
                _ = Directory.CreateDirectory(tempPath);
            }
            
            AssetDatabase.CreateAsset(window.diagramDatabase, tempFilePath);
            AssetDatabase.SaveAssets();
            window.diagramDatabase.OldPath = oldPath.IsNullOrEmpty() ? tempFilePath : oldPath;
        }

        public void RemoveCurrentSaveTempFile()
        {
            DiagramSO temp = AssetDatabase.LoadAssetAtPath<DiagramSO>(tempFilePath);
            if (!temp) return;
            AssetDatabase.DeleteAsset(tempFilePath);
            AssetDatabase.SaveAssets();
        }
        private void FlushNodes(DiagramNodeModel nodeModel)
        {
            window.diagramDatabase.Models.AddOrUpdate(nodeModel.Header.ID, new DiagramNodeModel(nodeModel));
        }
        public void Flush()
        {
            window.diagramDatabase.Models.Clear();
            
            // Remove Removed Node
            window.GUI.view.diagramNodeToRemove.ForEach(node => window.GUI.view.diagramNodes.SafeRemove(node.Key));
            window.GUI.view.groupToRemove.ForEach(group => window.GUI.view.groups.SafeRemove(group.Key));
            window.GUI.view.memoToRemove.ForEach(memo => window.GUI.view.memos.SafeRemove(memo.Key));
            
            // Flush all nodes
            window.GUI.view.diagramNodes.Values.ForEach(node => FlushNodes(node.NodeModel));
            window.GUI.view.groups.Values.ForEach(group => FlushNodes(group.NodeModel));
            window.GUI.view.memos.Values.ForEach(memo => FlushNodes(memo.NodeModel));
            
            window.GUI.view.diagramNodeToRemove.Clear();
            window.GUI.view.groupToRemove.Clear();
            window.GUI.view.memoToRemove.Clear();
            window.GUI.view.diagramNodeToCreate.Clear();
            window.GUI.view.groupToCreate.Clear();
            window.GUI.view.memoToCreate.Clear();
            // Must be called save to asset
            EditorUtility.SetDirty(window.diagramDatabase);
        }

        private string GetUserPath()
        {
            return EditorUtility.SaveFilePanelInProject("Save Diagram", string.Empty, "asset", "Save Diagram");
        }
        public void Save()
        {
            Flush();
            if(window.diagramDatabase.OldPath.IsNullOrEmpty() ||
            window.diagramDatabase.OldPath.Contains(tempPath))
            {
                SaveAs();
                return;
            }
            SaveToSO(window.diagramDatabase.OldPath);
            window.GUI.ClearDirty();
            SetupTempDatabase();
        }

        public void SaveAs()
        {
            string filePath = GetUserPath();
            if(filePath.IsNullOrEmpty()) return;
            
            Flush();
            SaveToSO(filePath);
            window.GUI.ClearDirty();
            SetupTempDatabase();
        }
        private void SaveToSO(string newPath)
        {
            string oldPath = AssetDatabase.GetAssetPath(window.diagramDatabase);
            bool isOverWritten = oldPath.Equals(newPath);
            if (isOverWritten)
            {
                AssetDatabase.SaveAssets();
                return;
            }

            if (newPath.Equals(tempFilePath)) return;
            Debug.Log($"Save to {newPath}");
            window.view.titleTextField.value = Path.GetFileNameWithoutExtension(newPath);
            
            AssetDatabase.CopyAsset(oldPath, newPath);
            window.diagramDatabase = AssetDatabase.LoadAssetAtPath(newPath, typeof(DiagramSO)) as DiagramSO;
            EditorUtility.SetDirty(window.diagramDatabase);
            SetupTempDatabase(newPath);
        }

        public void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Graphs", "Assets/", "asset");
            string unityPath = filePath.Replace(Application.dataPath, "Assets");
            if(unityPath.IsNullOrEmpty()) return;
            
            // only allow unity path
            window.diagramDatabase = AssetDatabase.LoadAssetAtPath(unityPath, typeof(DiagramSO)) as DiagramSO;
            
            if(window.diagramDatabase == null)
            {
                Debug.LogError($"Invalidated path | {unityPath}");
                return;
            }
            
            window.RepaintNodes();
            SetupTempDatabase();
        }
        
        public void Load(DiagramSO diagramSO)
        {
            if (window.diagramDatabase)
            {
                AssetDatabase.SaveAssetIfDirty(window.diagramDatabase);
                RemoveCurrentSaveTempFile();
            }
            
            window.diagramDatabase = diagramSO;
            window.RepaintNodes();
            
            SetupTempDatabase(diagramSO.OldPath);
        }


    }

}