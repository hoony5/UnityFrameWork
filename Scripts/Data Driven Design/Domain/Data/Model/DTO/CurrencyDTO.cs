using Newtonsoft.Json;

[System.Serializable]
public class CurrencyDTO : DTOBase
{
    [JsonConstructor]
    public CurrencyDTO (string name, int amount) : base(name, amount)
    {
    }
}
