using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public static class FieldResourceBinder
{
    private static readonly char[] separator = { '/', '\\', '.', ' ', '_', '-', '|', ':', ',' };
    public static void Bind<T>(T obj)
    {
        FieldInfo[] fields = obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<BindAttribute>() != null)
            .ToArray();
            
        foreach (FieldInfo field in fields)
        {
            BindAttribute bindAttribute = field.GetCustomAttribute<BindAttribute>();
            if (bindAttribute != null)
            {
                switch (bindAttribute.bindType)
                {
                    case BindType.Resources:
                        // Handle AssetPath loading
                        LoadResourceAtPath(bindAttribute.path, field, obj);
                        break;
                    case BindType.URL:
                        // Handle URL loading
                        LoadFromURL(bindAttribute.path, field, obj);
                        break;
                    case BindType.Component:
                        // Handle Component binding
                        LoadComponent(bindAttribute.path, field, obj);
                        break;
                    case BindType.StreamingAssets:
                        LoadFromStreamingAssets(bindAttribute.path, field, obj);
                        break;
                    case BindType.Addressable:
                        LoadFromAddressableAsset(bindAttribute.path, field, obj);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
    
    public static void Release<T>(T obj)
    {
        FieldInfo[] fields = obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<BindAttribute>() != null)
            .ToArray();
            
        foreach (FieldInfo field in fields)
        {
            BindAttribute bindAttribute = field.GetCustomAttribute<BindAttribute>();
            if (bindAttribute == null) continue;
            field.SetValue(obj, null);
        }
    }

    private static void LoadResourceAtPath<T>(string path, FieldInfo field, T obj)
    {
        if(obj is MonoBehaviour behaviour)
        {
            UnityCoroutine coroutine = new UnityCoroutine();
            coroutine.AddRoutine(LoadResource());
            coroutine.Run();
        }
        else
        {
             field.SetValue(obj, Resources.Load(path, field.FieldType));
        }
        return;
        IEnumerator LoadResource()
        {
            ResourceRequest asset = Resources.LoadAsync(path, field.FieldType);
            yield return asset;
            field.SetValue(behaviour, asset.asset);
        }
    }
    private static async void LoadFromStreamingAssets<T>(string path, FieldInfo field, T obj)
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, path);
        if(File.Exists(fullPath))
        {
            byte[] file = await File.ReadAllBytesAsync(fullPath);
            if(file == null)
            {
                Debug.LogError($"Failed to load file from StreamingAssets: {path}");
                return;
            }
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case ".json":
                    field.SetValue(obj, JsonUtility.FromJson(System.Text.Encoding.UTF8.GetString(file), field.FieldType));
                    break;
                case ".png":
                case ".jpg":
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(file);
                    field.SetValue(obj, texture);
                    break;
                case ".mp3":
                case ".wav":
                    SetAudioClip(file, fullPath, field, obj);
                    break;  
                case ".xml":
                case ".txt":
                    field.SetValue(obj, System.Text.Encoding.UTF8.GetString(file));
                    break;
                case ".mp4":
                case ".mov":
                case ".avi":
                    SetURLOnVideoPlayer(fullPath, field, obj);
                    break;
                default:
                    Debug.LogError($"Unsupported file type: {extension}");
                    break;
            }
        }
        else
        {
            Debug.LogError($"File not found in StreamingAssets: {path}");
        }
    }

    private static void SetAudioClip<T>(byte[] file, string path, FieldInfo field, T obj)
    {
        if(field.FieldType != typeof(AudioSource))
        {
            Debug.LogError($"Field type is not AudioSource for audio file: {path}");
            return;
        }

        AudioSource audioSource = field.GetValue(obj) as AudioSource;
        if(audioSource == null)
        {
            Debug.LogError($"AudioSource component not found in field: {field.Name}");
            return;
        }
        audioSource.clip = AudioClip.Create("AudioClip", file.Length, 1, 44100, false);
        
        // 바이트 배열을 float 배열로 변환
        float[] floatArray = new float[file.Length / 2];
        for (int i = 0; i < floatArray.Length; i++)
        {
            // 바이트 배열에서 한 쌍을 읽어 하나의 float로 변환
            floatArray[i] = BitConverter.ToInt16(file, i * 2) / 32768.0f;
        }
        audioSource.Play();
        audioSource.clip.SetData(floatArray, 0);
    }

    private static void SetURLOnVideoPlayer<T>(string path, FieldInfo field, T obj)
    {
        if(field.FieldType != typeof(VideoPlayer))
        {
            Debug.LogError($"Field type is not VideoPlayer for video file: {path}");
            return;
        }
                    
        VideoPlayer vp = field.GetValue(obj) as VideoPlayer;
        if(vp == null)
        {
            Debug.LogError($"VideoPlayer component not found in field: {field.Name}");
            return;
        }
        vp.source = VideoSource.Url;
        vp.url = path;
    }
    private static void OnAddressableComplete<T>(AsyncOperationHandle<Object> handle, FieldInfo field, T obj)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            field.SetValue(obj, handle.Result);
            Debug.Log($"Addressable asset loaded | {handle.DebugName} | {handle.Status}");
        }
        else
        {
            Debug.LogError($"Failed to load asset from Addressables at path: {handle.DebugName}");
        }
    }
    private static IEnumerator LoadFromAddressable<T>(string path, FieldInfo field, T obj)
    {
        AsyncOperationHandle<Object> handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Object>(path);
        handle.Completed += operationHandle => OnAddressableComplete(operationHandle, field, obj);
        yield return handle;
        Debug.Log($"Addressable asset loaded | {handle.DebugName} | {handle.Status}");
    }
    
    private static void LoadFromAddressableAsset<T>(string path, FieldInfo field, T obj)
    {
        UnityCoroutine coroutine = new UnityCoroutine();
        coroutine.AddRoutine(LoadFromAddressable(path, field, obj));
        coroutine.Run();
    }

    private static void LoadFromURL<T>(string url, FieldInfo field, T obj)
    {
        UnityCoroutine coroutine = new UnityCoroutine();
        coroutine.AddRoutine(Load());
        coroutine.Run();
        
        return;
        IEnumerator Load()
        {
            yield return null;
            string extension = Path.GetExtension(url);
            if (extension == ".json")
            {
                yield return LoadFromURLJson(url, field, obj);
            }
            else if (extension is ".png" or ".jpg")
            {
                yield return LoadFromURLTexture(url, field, obj);
            }
            else if (extension is ".mp3" or ".wav")
            {
                yield return LoadFromURLAudioClip(url, field, obj);
            }
            else if (extension == ".xml")
            {
                yield return LoadFromURLXml(url, field, obj);
            }
            else if (extension == ".txt")
            {
                yield return LoadFromURLTextAsset(url, field, obj);
            }
            else if (extension is ".mp4" or ".mov" or ".avi")
            {
                if(obj is MonoBehaviour unityObject)
                {
                    SetURLOnVideoPlayer(url, field, unityObject);
                }
            }
        }
    }
    private static IEnumerator LoadFromURLJson<T>(string url, FieldInfo field, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string json = request.downloadHandler.text;
        field.SetValue(obj, JsonUtility.FromJson(json, field.FieldType));
    }
    
    private static IEnumerator LoadFromURLTexture<T>(string url, FieldInfo field, T obj)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        field.SetValue(obj, texture);
    }
    
    private static IEnumerator LoadFromURLAudioClip<T>(string url, FieldInfo field, T obj)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN);
        yield return request.SendWebRequest();
        
        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
        field.SetValue(obj, audioClip);
    }
    
    private static IEnumerator LoadFromURLXml<T>(string url, FieldInfo field, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string xml = request.downloadHandler.text;
        field.SetValue(obj, xml);
    }
    
    private static IEnumerator LoadFromURLTextAsset<T>(string url, FieldInfo field, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string text = request.downloadHandler.text;
        field.SetValue(obj, text);
    }

    private static void LoadComponent<T>(string path, FieldInfo field, T obj)
    {
        string[] names = path.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        Component component;
        GameObject targetGameObject;

        string rootName = names[0];
        string childName = names.Length > 1 ? names[1] : null;
        
        targetGameObject = GameObject.Find(rootName); // todo:: other ways to find GameObject
        if (targetGameObject == null)
        {
            Debug.LogError($"GameObject not found for name: {rootName}");
            return;
        }
        
        if (names.Length > 1)
        {
            for (int i = 1; i < names.Length; i++)
            {
                childName = names[i];
                Transform childTransform = targetGameObject.transform.Find(childName);
                if (childTransform != null)
                {
                    targetGameObject = childTransform.gameObject;
                }
            }
            
            if (targetGameObject == null)
            {
                Debug.LogError($"GameObject not found for name: {childName}");
                return;
            }
        }
        
        component = targetGameObject.GetComponent(field.FieldType);
        if (component != null)
        {
            field.SetValue(obj, component);
        }
        else
        {
            Debug.LogError($"Component of type {field.FieldType} not found in GameObject {path}");
        }
    }
}