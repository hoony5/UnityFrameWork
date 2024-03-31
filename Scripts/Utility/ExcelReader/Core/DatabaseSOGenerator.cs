using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEngine;
using Utility.ExcelReader;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DatabaseSOGenerator
{
    public string databasePath = "Assets";
    public string destinationPath = "Assets";
    
    public DatabaseSOGenerator()
    {
    }

    public DatabaseSOGenerator(DDDGeneratorSettings settings)
    {
        databasePath = settings.DatabasePath;
        destinationPath = settings.DestinationPath;
    }

    public void GenerateDatabaseSO()
    {
#if UNITY_EDITOR
        // Load from database Path
        string[] xlsxFiles = Directory.GetFiles(databasePath, "*.xlsx", SearchOption.AllDirectories);
        if(xlsxFiles.Length == 0) return;
            
        // Save destination Path
        if(!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }
            
        foreach (string xlsxFile in xlsxFiles)
        {
            UnityEngine.Object xlsx = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(xlsxFile);
            UnityEngine.Object so = ScriptableObject.CreateInstance<ExcelDataSO>();
            string soPath = Path.Combine(destinationPath, $"{Path.GetFileNameWithoutExtension(xlsxFile)}.asset");
            AssetDatabase.CreateAsset(so, soPath);
            AssetDatabase.SaveAssets();
            ((ExcelDataSO)so).ExcelData = xlsx;
            ((ExcelDataSO)so).Load();
        }
#endif
    }

    public void Remove()
    {
#if UNITY_EDITOR
        if(!Directory.Exists(destinationPath))
        {
            return;
        }
        string[] databaseSOs = AssetDatabase.FindAssets("t:ExcelDataSO", new[] {destinationPath});
        if(databaseSOs == null) return;
        if(databaseSOs.Length == 0) return;
            
        foreach (string databaseSO in databaseSOs)
        {
            string path = AssetDatabase.GUIDToAssetPath(databaseSO);
            AssetDatabase.DeleteAsset(path);
        }
#endif
    }

    public async void GenerateModel()
    {
        DDDGeneratorSettings settings = new DDDGeneratorSettings();
        string[] xlsxFiles = Directory.GetFiles(databasePath, "*.xlsx", SearchOption.AllDirectories);
        if(xlsxFiles.Length == 0) return;
            
        // Save destination Path
        if(!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        foreach (string xlsxFile in xlsxFiles)
        {
            (string topCategory, SerializedDictionary<string, ExcelSheetInfo> map) data = ExcelCsvReader.Read(xlsxFile);
            string[] basics = new[] { "topCategory", "middleCategory", "bottomCategory", "objectName", "objectID", "displayName" };
            
            foreach (KeyValuePair<string, ExcelSheetInfo> sheetInfo in data.map)
            {
                int currentLineCount = 1;
                int cursorIndex = 0;
                string sheetName = sheetInfo.Key;
                string basePath = Path.Combine(settings.EntityRootPath, "BaseModel");
                string modelPath = Path.Combine(basePath, $"{sheetName}.cs");
                
                if(!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                
                string className = sheetName;
                string propertyFormat = $"\t[field:SerializeField, JsonProperty(nameof({{1}}))] public {{0}} {{1}} {{ get; init; }}";
                RowData standard = sheetInfo.Value.RowDataDict.Values.FirstOrDefault();
                if(standard == null) continue;
                string parameterFormat = "{0} {1}";
                string constructorFormat = "\t\t{0} = {1};";
                string toStringFormat = "\t\t*   {0} : {{1}}";
                
                
                StringBuilder propertyBuilder = new StringBuilder();
                StringBuilder parameterBuilder = new StringBuilder();
                StringBuilder constructorBuilder = new StringBuilder();
                StringBuilder toStringBuilder = new StringBuilder();

                for (var index = 0; index < standard.Headers.Count; index++)
                {
                    string header = standard.Headers[index];
                    string type = standard.Types[index];

                    if (type.Contains("enum:"))
                        type = type.Replace("enum:", string.Empty);
                    
                    parameterBuilder.Append(string.Format(parameterFormat, type, header.ToLowerAt(0)));
                    if (index < standard.Headers.Count - 1)
                    {
                        parameterBuilder.Append(", ");
                    }
                    if (currentLineCount == 1)
                    {
                        if (parameterBuilder.Length / 50 <= currentLineCount)
                            continue;
                        
                        parameterBuilder.AppendLine();
                        currentLineCount++;
                    }
                    else
                    {
                        cursorIndex = parameterBuilder.Length % 60;
                        if (parameterBuilder.Length / 90 <= currentLineCount)
                            continue;
                        
                        parameterBuilder.AppendLine();
                        currentLineCount++;
                    }

                    if (Array.Exists(basics, item => item.Equals(header, StringComparison.OrdinalIgnoreCase))) 
                        continue;
                    
                    propertyBuilder.AppendLine(propertyFormat.Replace("{0}", type).Replace("{1}", header));
                    constructorBuilder.AppendLine(string.Format(constructorFormat, header, header));
                    toStringBuilder.AppendLine(toStringFormat.Replace("{0}", header).Replace("{1}", header));
                }

                string contentFormat = $@"using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class {className} : ValueObject
{{
{propertyBuilder}
    [JsonConstructor]
    public {className}({parameterBuilder}) 
: base(topCategory, middleCategory, bottomCategory, objectName, objectID)
    {{
{constructorBuilder}
    }}
    
    public override string ToString()
    {{
#if UNITY_EDITOR
        return $@""
{{base.ToString()}}
[{className}]
{toStringBuilder}
"";
#else
        return base.ToString();
#endif
    }}
}}";
                
                using (FileStream fs = new FileStream(modelPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        await writer.WriteAsync(contentFormat);
                    }
                }
            }
            await Task.Delay(500);
            AssetDatabase.Refresh();
        }
    }

    public void GenerateExternalInit()
    {
        // search on the assets
        if(AssetDatabase.FindAssets("IsExternalInit") != null)
        {
            return;
        }
        
        string path = Path.Combine(destinationPath, "IsExternalInit.cs");
        
        if(!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }
        
        string content = @"namespace System.Runtime.CompilerServices
{    
    public class IsExternalInit 
    {
 
    }
}";
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write(content);
            }
        }
        
        AssetDatabase.Refresh();
    }
}
