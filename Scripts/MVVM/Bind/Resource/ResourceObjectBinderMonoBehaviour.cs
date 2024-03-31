using System;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceObjectBinderMonoBehaviour : MonoBehaviour, IDisposable
{
    private void OnValidate()
    {
        MonoBehaviour[] monoBehaviours =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            FieldResourceBinder.Bind(monoBehaviour);
            PropertyResourceBinder.Bind(monoBehaviour);
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        MonoBehaviour[] monoBehaviours =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            FieldResourceBinder.Release(monoBehaviour);
            PropertyResourceBinder.Release(monoBehaviour);
        }
    }
}