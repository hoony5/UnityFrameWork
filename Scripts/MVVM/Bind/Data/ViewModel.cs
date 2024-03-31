using System;
using Unity.Properties;
using UnityEngine;

[System.Serializable]
public class ViewModel<T> : IDisposable
{
    [SerializeField, DontCreateProperty]
    private T value;
    
    [CreateProperty]
    public T Value
    {
        get => value;
        set
        {
            if (Equals(this.value, value))
                return;

            this.value = value;
            OnValueChanged?.Invoke(this.value);
        }
    }
    
    public void SetValueWithoutNotify(T value)
    {
        this.value = value;
    }
    
    public event Action<T> OnValueChanged;
    
    public ViewModel() : this(default(T))
    {
    }
    public ViewModel(T value)
    {
        this.value = value;
    }

    public void Dispose()
    {
        OnValueChanged = null;
    }
}
