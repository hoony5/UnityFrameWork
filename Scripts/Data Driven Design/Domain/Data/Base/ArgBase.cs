using UnityEngine;

[System.Serializable]
public abstract class ArgBase<TData>
{
    [field:SerializeField] public string Name { get; protected set; }
    [field:SerializeField] public TData Value { get; protected set; }

    public abstract override string ToString();
    public abstract void Modify(TData value, ModificationType modificationType);

    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();
}