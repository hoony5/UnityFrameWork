using Newtonsoft.Json;

[System.Serializable]
public class ItemDTO : DTOBase
{
    [JsonConstructor]
    public ItemDTO(string name, int count) : base(name, count)
    {
    }
}
    