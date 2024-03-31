using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ProcessEx
{
    private static List<Process> _processes = new List<Process>();
    private static string GetOpenFolderCommand()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
            return "explorer.exe";
        if(Application.platform == RuntimePlatform.OSXEditor)
            return "open";
        if(Application.platform == RuntimePlatform.LinuxEditor)
            return "xdg-open";
        return string.Empty;
    }
    public static void OpenFolder(string path)
    {
        _processes.Add(Process.Start(GetOpenFolderCommand(), path));
    }

    public static void OpenFile(string path)
    {
        if (!File.Exists(path)) return;
        _processes.Add(Process.Start(path));
    }
    public static void OpenFile(string application, string path)
    {
        if (!File.Exists(path)) return;
        if(Environment.OSVersion.Platform == PlatformID.MacOSX)
            _processes.Add(Process.Start("open", $"-e {path}"));
        else
            _processes.Add(Process.Start(application, path));
    }

    public static void Dispose()
    {
        foreach (var process in _processes)
        {
            if (process.HasExited) continue;
            
            process.Kill();
            process.Dispose();
        }
        
        _processes.Clear();
    }
}
