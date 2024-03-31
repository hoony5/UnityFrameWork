using UnityEngine;

namespace Diagram
{
    [System.Serializable]
    public class CheckListInfo
    {
       [field:SerializeField] public int Id { get; set; }
       [field:SerializeField] public bool toggleValue { get; set; }
       [field:SerializeField] public string Description { get; set; }
    }

}