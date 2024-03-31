using UnityEngine;

namespace Diagram
{
    [System.Serializable]
    public class CellInfo
    {
        [field:SerializeField] public int Row { get; set; }
        [field:SerializeField] public int Column { get; set; }
        [field:SerializeField] public string Value { get; set; }
    }

}