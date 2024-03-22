using UnityEngine;

[System.Serializable]
public abstract class Entity
{
    [field:SerializeField] public string ID { get; private set; }
    [field:SerializeField] public string Auth { get; private set; }

    protected Entity(string id, string auth)
    {
        ID = id;
        Auth = auth;
    }
    // Exp , Currency, Item, Skill, etc...
    public abstract override string ToString();
}