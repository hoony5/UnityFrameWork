using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DocumentGenerator : Editor
{
    private static DocumentGeneratorConfig config;
    private static Process documentCreator;

    [MenuItem("Document/Refresh")]
    public static void Refresh()
    {
        ProcessEx.Dispose();
    }
    [MenuItem("Document/Generate/MarkDown")]
    public static void GenerateMarkDown()
    {
        FindConfig();
        Run("MarkDown", config.creatorPath);

        string[] lines = GetConfigText(config.markDownConfig);
        string destDir = string.Empty;
        string update = string.Empty;

        foreach (var line in lines)
        {
            GetConfigItem(line, "destDir", ref destDir);
            GetConfigItem(line, "update", ref update);
        }

        Debug.Log($"Document Generated : {destDir} | update : {update}");
        Debug.Log($"Document update : {update}");
        
        ProcessEx.OpenFolder(destDir);
    }
    [MenuItem("Document/Generate/ConvertToPDF")]
    public static void GeneratePDF()
    {
        FindConfig();
        Environment.SetEnvironmentVariable("PATH", config.converterPath);
        Run("ConvertToPDF", config.converterPath);
        ProcessEx.OpenFolder(config.converterPath);
    }

    [MenuItem("Document/Generate/Publish To Confluence")]
    public static void PublishToConfluence()
    {
        FindConfig();
        Run("Confluence", config.publisherPath);

        OpenConfluenceURL();
    }

    [MenuItem("Document/Config/Markdown Config")]
    public static void OpenMarkdownConfig()
    {
        FindConfig();
        ProcessEx.OpenFile(config.markDownConfig);
    }

    [MenuItem("Document/Config/Converter Config")]
    public static void OpenConvertToPdfConfig()
    {
        FindConfig();
        ProcessEx.OpenFile("notepad.exe", config.converterConfigPath);
    }

    [MenuItem("Document/Config/Publisher Config")]
    public static void OpenPublisherConfig()
    {
        FindConfig();
        ProcessEx.OpenFile(config.publisherConfigPath);
    }

    [MenuItem("Document/Directory/Change Destination Directory")]
    public static void ChangeDestDirectory()
    {
        FindConfig();
        string path = EditorUtility.OpenFolderPanel("Select Export Directory", "", "");
        if (string.IsNullOrEmpty(path)) return;
        // replace / with \ for windows
        path = path.Replace("/", "\\");
        // if contains korean it is invalid only accept english
        if (Regex.IsMatch(path, @"[ㄱ-ㅎ가-힣]+"))
        {
            Debug.LogError("Invalid Path!! it contains Korean");
            return;
        }

        ChangeDirectory(config.markDownConfig, "destDir", path);
        ChangeDirectory(config.converterConfigPath, "destDir", path);
    }

    [MenuItem("Document/Directory/Open Confluence Space")]
    public static void OpenConfluenceURL()
    {
        FindConfig();
        string[] lines = GetConfigText(config.publisherConfigPath);
        string url = string.Empty;
        string spaceKey = string.Empty;
        foreach (var line in lines)
        {
            GetConfigItem(line, "url", ref url);
            GetConfigItem(line, "spaceKey", ref spaceKey);
        }

        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(spaceKey))
        {
            Application.OpenURL($"https://www.atlassian.com/ko/software/confluence");
            return;
        }
        string address = $@"{url}/spaces/{spaceKey}/overview";
        Debug.Log($"Document Generated : {address}");
        
        Application.OpenURL(address);
    }
    [MenuItem("Document/Directory/Open Destination Directory")]
    public static void OpenExportDirectory()
    {
        FindConfig();
        string cotent = File.ReadAllText(config.markDownConfig);
        string[] lines = cotent.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        string destDir = string.Empty;

        foreach (var line in lines)
        {
            if (line.StartsWith("destDir"))
            {
                destDir = line.Split('=')[1];
            }
        }
        Debug.Log($"Document Generated : {destDir}");
        // open directory
        ProcessEx.OpenFolder(destDir);
    }

    private static void FindConfig()
    {
        var guid = AssetDatabase.FindAssets($"t:{nameof(DocumentGeneratorConfig)}");
        if (guid.Length == 0)
        {
            Debug.LogError("Config not found");
            return;
        }

        var path = AssetDatabase.GUIDToAssetPath(guid[0]);
        config = AssetDatabase.LoadAssetAtPath<DocumentGeneratorConfig>(path);
    }

    private static string[] GetConfigText(string path)
    {
        string cotent = File.ReadAllText(path);
        return cotent.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }

    private static void Run(string title, string path)
    {
        bool accepted = EditorUtility.DisplayDialog($"{title} Generator",
            $"{title} Generation Started. It will be overwritten.", "OK", "Cancel");
        if (!accepted) return;
        
        documentCreator = new Process();
        if(path.EndsWith(".js"))
        {
            documentCreator.StartInfo = SetUpJavaScript(path);
        }
        else
        {
            documentCreator.StartInfo.FileName = path;    
        }
        documentCreator.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        documentCreator.Start();
    }

    private static ProcessStartInfo SetUpJavaScript(string appPath)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "node"; // Node.js의 실행 파일 경로를 지정해야 할 수도 있습니다. 예: C:\Program Files\nodejs\node.exe
        startInfo.Arguments = appPath; // Node.js 스크립트 파일 경로
        startInfo.UseShellExecute = false; // 셸 실행 사용 안 함
        return startInfo;
    }

    private static void GetConfigItem(string baseLine, string keyword, ref string item)
    {
        if (!baseLine.StartsWith(keyword)) return;
        item = baseLine.Split('=')[1];
    }

    private static void ChangeDirectory(string configPath, string keyword, string newPath)
    {
        string content = File.ReadAllText(configPath);
        // the line contains destDir=... is replaced with destDir=newPath
        Regex regex = new Regex($@"{keyword}=.*");
        content = regex.Replace(content, $"{keyword}={newPath}");
        File.WriteAllText(configPath, content);
        ProcessEx.OpenFile(configPath);
    }
}
