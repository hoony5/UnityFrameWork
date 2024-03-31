using UnityEngine;

public static class LayerEx
{
    public static bool IncludesLayer(this LayerMask layerMask, int layer) 
    {
        return ((1 << layer) & layerMask.value) != 0;
    }
}
