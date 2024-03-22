using UnityEngine;

[System.Serializable]
public class CurrencyEntity : Entity
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    public CurrencyEntity(string id, string auth , CurrencyDTO currencyDTO) : base(id, auth)
    {
        Name = currencyDTO.Name;
        Amount = currencyDTO.Level;
    }


    public void AddAmount(int value)
    {
        Amount += value;
        if(Amount < 0) Amount = 0;
    }

    public override string ToString()
    {
#if UNITY_EDITOR
        return $@"
[Currency]
    *   Name                          : {Name}
    *   Amount                        : {Amount}
";
#else
        return GetType().ToString();
#endif
    }
}