using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public static class PropertyResourceBinder
{
    private static readonly char[] separator = { '/', '\\', '.', ' ', '_', '-', '|', ':', ',' };
    public static void Bind<T>(T obj)
    {
        PropertyInfo[] propertys = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<BindAttribute>() != null)
            .ToArray();
            
        foreach (PropertyInfo property in propertys)
        {
            BindAttribute bindAttribute = property.GetCustomAttribute<BindAttribute>();
            if (bindAttribute != null)
            {
                switch (bindAttribute.bindType)
                {
                    case BindType.Resources:
                        // Handle AssetPath loading
                        LoadResourceAtPath(bindAttribute.path, property, obj);
                        break;
                    case BindType.URL:
                        // Handle URL loading
                        LoadFromURL(bindAttribute.path, property, obj);
                        break;
                    case BindType.Component:
                        // Handle Component binding
                        LoadComponent(bindAttribute.path, property, obj);
                        break;
                    case BindType.StreamingAssets:
                        LoadFromStreamingAssets(bindAttribute.path, property, obj);
                        break;
                    case BindType.Addressable:
                        LoadFromAddressableAsset(bindAttribute.path, property, obj);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
    public static void Release<T>(T obj)
    {
        PropertyInfo[] propertys = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<BindAttribute>() != null)
            .ToArray();
            
        foreach (PropertyInfo property in propertys)
        {
            BindAttribute bindAttribute = property.GetCustomAttribute<BindAttribute>();
            if (bindAttribute == null) continue;
            property.SetValue(obj, null);
        }
    }
    private static void LoadResourceAtPath<T>(string path, PropertyInfo property, T obj)
    {
        if(obj is MonoBehaviour behaviour)
        {
            UnityCoroutine coroutine = new UnityCoroutine();
            coroutine.AddRoutine(LoadResource());
            coroutine.Run();
        }
        else
        {
             property.SetValue(obj, Resources.Load(path, property.PropertyType));
        }
        return;
        IEnumerator LoadResource()
        {
            ResourceRequest asset = Resources.LoadAsync(path, property.PropertyType);
            yield return asset;
            property.SetValue(behaviour, asset.asset);
        }
    }
    private static async void LoadFromStreamingAssets<T>(string path, PropertyInfo property, T obj)
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
                    property.SetValue(obj, JsonUtility.FromJson(System.Text.Encoding.UTF8.GetString(file), property.PropertyType));
                    break;
                case ".png":
                case ".jpg":
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(file);
                    property.SetValue(obj, texture);
                    break;
                case ".mp3":
                case ".wav":
                    SetAudioClip(file, fullPath, property, obj);
                    break;  
                case ".xml":
                case ".txt":
                    property.SetValue(obj, System.Text.Encoding.UTF8.GetString(file));
                    break;
                case ".mp4":
                case ".mov":
                case ".avi":
                    SetURLOnVideoPlayer(fullPath, property, obj);
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

    private static void SetAudioClip<T>(byte[] file, string path, PropertyInfo property, T obj)
    {
        if(property.PropertyType != typeof(AudioSource))
        {
            Debug.LogError($"Field type is not AudioSource for audio file: {path}");
            return;
        }

        AudioSource audioSource = property.GetValue(obj) as AudioSource;
        if(audioSource == null)
        {
            Debug.LogError($"AudioSource component not found in property: {property.Name}");
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

    private static void SetURLOnVideoPlayer<T>(string path, PropertyInfo property, T obj)
    {
        if(property.PropertyType != typeof(VideoPlayer))
        {
            Debug.LogError($"Field type is not VideoPlayer for video file: {path}");
            return;
        }
                    
        VideoPlayer vp = property.GetValue(obj) as VideoPlayer;
        if(vp == null)
        {
            Debug.LogError($"VideoPlayer component not found in property: {property.Name}");
            return;
        }
        vp.source = VideoSource.Url;
        vp.url = path;
    }
    private static void OnAddressableComplete<T>(AsyncOperationHandle<Object> handle, PropertyInfo property, T obj)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            property.SetValue(obj, handle.Result);
            Debug.Log($"Addressable asset loaded | {handle.DebugName} | {handle.Status}");
        }
        else
        {
            Debug.LogError($"Failed to load asset from Addressables at path: {handle.DebugName}");
        }
    }
    private static IEnumerator LoadFromAddressable<T>(string key, PropertyInfo property, T obj)
    {
        AsyncOperationHandle<Object> handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Object>(key);
        handle.Completed += operationHandle => OnAddressableComplete(operationHandle, property, obj);
        yield return handle;
        Debug.Log($"Addressable asset loaded | {handle.DebugName} | {handle.Status}");
    }
    
    private static void LoadFromAddressableAsset<T>(string key, PropertyInfo property, T obj)
    {
        UnityCoroutine coroutine = new UnityCoroutine();
        coroutine.AddRoutine(LoadFromAddressable(key, property, obj));
        coroutine.Run();
    }

    private static void LoadFromURL<T>(string url, PropertyInfo property, T obj)
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
                yield return LoadFromURLJson(url, property, obj);
            }
            else if (extension is ".png" or ".jpg")
            {
                yield return LoadFromURLTexture(url, property, obj);
            }
            else if (extension is ".mp3" or ".wav")
            {
                yield return LoadFromURLAudioClip(url, property, obj);
            }
            else if (extension == ".xml")
            {
                yield return LoadFromURLXml(url, property, obj);
            }
            else if (extension == ".txt")
            {
                yield return LoadFromURLTextAsset(url, property, obj);
            }
            else if (extension is ".mp4" or ".mov" or ".avi")
            {
                if(obj is MonoBehaviour unityObject)
                {
                    SetURLOnVideoPlayer(url, property, unityObject);
                }
            }
        }
    }
    private static IEnumerator LoadFromURLJson<T>(string url, PropertyInfo property, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string json = request.downloadHandler.text;
        property.SetValue(obj, JsonUtility.FromJson(json, property.PropertyType));
    }
    
    private static IEnumerator LoadFromURLTexture<T>(string url, PropertyInfo property, T obj)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        property.SetValue(obj, texture);
    }
    
    private static IEnumerator LoadFromURLAudioClip<T>(string url, PropertyInfo property, T obj)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN);
        yield return request.SendWebRequest();
        
        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
        property.SetValue(obj, audioClip);
    }
    
    private static IEnumerator LoadFromURLXml<T>(string url, PropertyInfo property, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string xml = request.downloadHandler.text;
        property.SetValue(obj, xml);
    }
    
    private static IEnumerator LoadFromURLTextAsset<T>(string url, PropertyInfo property, T obj)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string text = request.downloadHandler.text;
        property.SetValue(obj, text);
    }

    private static void LoadComponent<T>(string path, PropertyInfo property, T obj)
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
        
        component = targetGameObject.GetComponent(property.PropertyType);
        if (component != null)
        {
            property.SetValue(obj, component);
        }
        else
        {
            Debug.LogError($"Component of type {property.PropertyType} not found in GameObject {path}");
        }
    }
}