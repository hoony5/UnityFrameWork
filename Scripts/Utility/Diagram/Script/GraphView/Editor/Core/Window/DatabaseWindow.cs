using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utility.ExcelReader;
using Object = UnityEngine.Object;

namespace Diagram
{
    public class DatabaseWindow : EditorWindow , IDisposable
    {
        public DiagramWindow diagramWindow;
        private ListView databaseFilesListView;
        private Button refreshButton;
        private Button loadButton;
        private List<ExcelDataSO> databaseFiles;
        private ExcelDataSO selectedExcelDataSO;
        
        public void CreateGUI()
        {
            databaseFilesListView = new ListView();
            databaseFilesListView.style.flexGrow = 0;
            databaseFilesListView.style.flexShrink = 1;
            databaseFilesListView.selectionType = SelectionType.Single;
            databaseFilesListView.style.height = new StyleLength(new Length(200, LengthUnit.Pixel));
            databaseFilesListView.showAddRemoveFooter = false;
            databaseFilesListView.showBorder = true;
            databaseFilesListView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            
            refreshButton = new Button();
            refreshButton.text = "Refresh";
            refreshButton.style.flexGrow = 0;
            refreshButton.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            refreshButton.style.height = 30;
            refreshButton.clickable.clicked += RefreshDatabaseFiles;
            
            loadButton = new Button();
            loadButton.text = "Load";
            loadButton.style.flexGrow = 0;
            loadButton.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            loadButton.style.height = 30;
            loadButton.clickable.clicked += ConvertExcelDataSOToNodes;
            
            Label label = new Label("Database ExcelData SO");

            rootVisualElement.CreateVerticalSpace(10);
            rootVisualElement.Add(label);
            rootVisualElement.CreateVerticalSpace(10);
            rootVisualElement.Add(databaseFilesListView);
            rootVisualElement.CreateVerticalSpace(10);
            rootVisualElement.Add(refreshButton);
            rootVisualElement.CreateVerticalSpace(10);
            rootVisualElement.Add(loadButton);
            
            RefreshDatabaseFiles();
        }

        private void RefreshDatabaseFiles()
        {
            databaseFiles = 
                AssetDatabase.FindAssets($"t:{nameof(ExcelDataSO)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ExcelDataSO>)
                .ToList();
            
            databaseFilesListView.itemsSource = databaseFiles;
        }

        private void ConvertExcelDataSOToNodes()
        {
            if(databaseFilesListView.selectedIndex < 0)
            {
                titleContent = new GUIContent($"DatabaseWindow");
                return;
            }
            selectedExcelDataSO = databaseFilesListView.itemsSource[databaseFilesListView.selectedIndex] as ExcelDataSO;
            if (selectedExcelDataSO == null)
            {
                titleContent = new GUIContent($"DatabaseWindow");
                return;
            }
            ConvertExcelToDiagramModel(selectedExcelDataSO);
            diagramWindow.RepaintNodes();
            DiagramWindow.IO.SetupTempDatabase();
            titleContent = new GUIContent($"{selectedExcelDataSO.name} Database ");
        }

        private void ConvertExcelToDiagramModel(Object @object)
        {
            // Load this file
            ExcelDataSO excelDataSO = (ExcelDataSO)@object;
            excelDataSO.Load();
            
            // only allow unity path
            diagramWindow.diagramDatabase = ScriptableObject.CreateInstance<DiagramSO>();
            diagramWindow.diagramDatabase.ID = System.Guid.NewGuid().ToString();
            diagramWindow.diagramDatabase.Models = new SerializedDictionary<string, DiagramNodeModel>();
            
            Vector2 center = diagramWindow.GUI.viewport.contentRect.center;
            Vector2 startPosition = center - new Vector2(center.x / 2, 0);
            Vector2 offset = new Vector2(0, 0);
            int distance = 1000;

            foreach (KeyValuePair<string, ExcelSheetInfo> item in excelDataSO.Database)
            {
                if (item.Value.RowDataDict.Count == 0) continue;
                // excel Model
                string scriptName = item.Value.MiddleCategory;
                KeyValuePair<string, RowData> obj = item.Value.RowDataDict.First();
                List<string> headers = obj.Value.Headers;
                List<string> types = obj.Value.Types;

                // node Model
                DiagramNodeModel model = new DiagramNodeModel
                {
                    ExcelData = excelDataSO,
                };

                model.Header = new DiagramHeaderModel(model.Status)
                {
                    Name = scriptName,
                    Position = startPosition + offset,
                    EntityType = EntityType.ValueObject,
                    Description = $"{excelDataSO.TopCategory}/{scriptName}",
                    Properties = new List<PropertyFieldInfo>()
                };
                // set properties
                for (var index = 0; index < headers.Count; index++)
                {
                    model.Header.Properties.Add(new PropertyFieldInfo
                    {
                        Name = headers[index],
                        Type = types[index],
                        ScriptName = scriptName
                    });
                }

                // Migration
                diagramWindow.diagramDatabase.Models.Add(model.Header.ID, model);
                offset += new Vector2(distance, 0);
            }

            EditorUtility.SetDirty(diagramWindow.diagramDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public void Dispose()
        {
            loadButton.clickable.clicked -= ConvertExcelDataSOToNodes;
            refreshButton.clickable.clicked -= RefreshDatabaseFiles;
        }
    }
}