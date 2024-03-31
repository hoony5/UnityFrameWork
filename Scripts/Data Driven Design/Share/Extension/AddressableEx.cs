using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableEx
{
    private static IEnumerator DownloadAssets(string groupName, Action<float> onProgressChanged = null)
    {
        AsyncOperationHandle<List<string>> checkForUpdate =
            UnityEngine.AddressableAssets.Addressables.CheckForCatalogUpdates(false);
        yield return checkForUpdate;
        if(checkForUpdate.Result.Count > 0)
        {
            AsyncOperationHandle<List<IResourceLocator>> updateCatalog =
                UnityEngine.AddressableAssets.Addressables.UpdateCatalogs(checkForUpdate.Result, false);
            yield return updateCatalog;
        }
        
        AsyncOperationHandle<long> downloadSize = UnityEngine.AddressableAssets.Addressables.GetDownloadSizeAsync(groupName);
        while (!downloadSize.IsDone)
        {
            onProgressChanged?.Invoke(downloadSize.PercentComplete * 100);
            yield return null;
        }
        Debug.Log($"Download Complete. \n Download size in MB: {downloadSize.Result / 1024 / 1024}");
    }
    private static void DownloadFromAddressableAsset(string groupName, Action<float> onProgressChanged = null)
    {
        UnityCoroutine coroutine = new UnityCoroutine();
        coroutine.AddRoutine(DownloadAssets(groupName, onProgressChanged));
        coroutine.Run();
    }
    public static IEnumerator LoadAsset<T>(string addressableKey, Action<T> callback)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(addressableKey);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            T asset = handle.Result;
            callback?.Invoke(asset);
        }
        else
        {
            Debug.LogError($"Failed to load asset with key: {addressableKey} | Error: {handle.Status} | {handle.OperationException}");
        }
    }
    public static IEnumerator RedownloadAsset<T>(string addressableKey, Action<T> callback)
    {
        Addressables.Release(Addressables.LoadAssetAsync<T>(addressableKey));
        yield return LoadAsset<T>(addressableKey, callback);
    }
    
    public static void UnloadAsset<T>(T asset)
    {
        Addressables.Release(asset);
    }

}
