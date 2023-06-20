using UnityEngine;

namespace DefaultNamespace
{
    public static class Utils
    {
        public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
    }
}