using UnityEngine;

public static class ResourceObjectBinder
{
    public static void Bind(this MonoBehaviour behaviour)
    {
         FieldResourceBinder.Bind(behaviour);
         PropertyResourceBinder.Bind(behaviour);
    }
    
    public static void Release(this MonoBehaviour behaviour)
    {
        FieldResourceBinder.Release(behaviour);
        PropertyResourceBinder.Release(behaviour);
    }
}