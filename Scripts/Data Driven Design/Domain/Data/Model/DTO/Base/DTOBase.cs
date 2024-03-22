using UnityEngine;

[System.Serializable]
public abstract class DTOBase
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public int Level { get; private set; }

    protected DTOBase(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public bool Equals(DTOBase other)
    {
        return Name == other.Name && Level == other.Level;
    }
}