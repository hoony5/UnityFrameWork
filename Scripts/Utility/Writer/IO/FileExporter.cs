using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Writer
{
    public class TextStreamer
    {
        private StreamWriter writer;
        public TextStreamer(string path)
        {
            writer = new StreamWriter(path, true, Encoding.UTF8);
        }
            
        public async void Write(string content)
        {
            await writer?.WriteAsync(content)!;
            await writer?.FlushAsync()!;
        }
            
        public void Close()
        {
            writer?.Close();
        }
    }
    public class FileExporter : IDisposable
    {
        private static List<TextStreamer> streamers = new List<TextStreamer>();
        private static CancellationTokenSource delayTokenSource = new CancellationTokenSource();
        private const string root = "Assets";
        public static async Task<string> Write(string path, string content)
        {
            string fullPath = Path.Combine(root, path);
            if(!Directory.Exists(Path.GetPathRoot(fullPath)))
            {
                string fileName = Path.GetFileName(fullPath);
                string fileRoot = fullPath.Replace(fileName, string.Empty);
                DirectoryInfo dirInfo = Directory.CreateDirectory(fileRoot);
                dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Normal;
            }
#if UNITY_EDITOR
            if (AssetDatabase.LoadAssetAtPath(fullPath, typeof(Object)) != null)
                AssetDatabase.DeleteAsset(fullPath);
#endif
            using (FileStream fileStream =
                   new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    try
                    {
                        await streamWriter.WriteAsync(content);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        throw;
                    }
                    finally
                    {
                        streamWriter.Close();
                    }
                }
            }
            return path;
        }

        public static async Task<string> BatchWriteAsync(string path, IEnumerable<string> contents)
        {
            string fullPath = Path.Combine(root, path);
            if(!Directory.Exists(Path.GetPathRoot(fullPath)))
            {
                string fileName = Path.GetFileName(fullPath);
                string fileRoot = fullPath.Replace(fileName, string.Empty);
                DirectoryInfo dirInfo = Directory.CreateDirectory(fileRoot);
                dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Normal;
            }
#if UNITY_EDITOR
            if (AssetDatabase.LoadAssetAtPath(fullPath, typeof(Object)) != null)
                AssetDatabase.DeleteAsset(fullPath);
#endif
            List<Task> tasks = new List<Task>();
            foreach (var content in contents)
            {
                 tasks.Add(Write(fullPath, content));
            }
            try
            {
                await Task.WhenAll(tasks).ContinueWith(result => Debug.Log($"Done. | {result.Status}"));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            finally
            {
                tasks.Clear();
            }
            return path;
        }

        public static async void RefreshEditor(
#if UNITY_EDITOR
            ImportAssetOptions option = ImportAssetOptions.Default
#endif
            )
        {
            await Task.Delay(500, delayTokenSource.Token);
#if UNITY_EDITOR
            AssetDatabase.Refresh(option);
#endif
        }
        
        public static TextStreamer GetTextStreamer(string root, string fileName, string extension)
        {
            if(!Directory.Exists(root))
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(root);
                dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Normal;
            }
            
            if(!extension.StartsWith('.')) extension = $".{extension}";
            string filePath = Path.Combine(root, $"{fileName}{extension}");
            
            if(AssetDatabase.LoadAssetAtPath(filePath, typeof(Object)) != null)
            {
#if UNITY_EDITOR
                AssetDatabase.DeleteAsset(filePath);
#endif
            }
            
            RefreshEditor();
            
            TextStreamer streamer = new TextStreamer(filePath);
            streamers.Add(streamer);
            return streamer;
        }

        public void Dispose()
        {
            delayTokenSource.Cancel();
            delayTokenSource.Dispose();
            delayTokenSource = null;
            
            if(streamers.Count == 0) return;
            
            foreach (var streamer in streamers)
            {
                streamer.Close();
            }
        }

        public static void PingObject(string path)
        {
#if UNITY_EDITOR
            Object result = AssetDatabase.LoadAssetAtPath<Object>(path);
            EditorGUIUtility.PingObject(result);
#endif
        }
    }
}