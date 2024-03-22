using UnityEngine;

[System.Serializable]
public class LevelExpEntity : Entity
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }

    public LevelExpEntity(string id, string auth , LevelDTO levelDto) : base(id, auth)
    {
        Level = levelDto.Level;
        Exp = levelDto.Exp;
    }

    public int GetLevel()
    {
        return Level;
    }

    public void AddLevel(int value)
    {
        Level += value;
        if(Level < 1) Level = 1;
    }
    public void AddExp(int value)
    {
        Exp += value;
        if(Exp < 0) Exp = 0;
    }

    public override string ToString()
    {
#if UNITY_EDITOR
        return $@"
[LevelExpEntity]
    *   ID                            : {ID}
    *   Auth                          : {Auth}
    *   Level                         : {Level}
    *   Exp                           : {Exp}
";
#else
        return GetType().ToString();
#endif
    }
}