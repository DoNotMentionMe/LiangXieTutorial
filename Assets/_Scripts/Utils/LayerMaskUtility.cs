
using UnityEngine;

public static class LayerMaskUtility
{
    public static bool Contains(LayerMask layerMask, int layer)
    {
        return (layerMask & 1 << layer) > 0;
    }
}
