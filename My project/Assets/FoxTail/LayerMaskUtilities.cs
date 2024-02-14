using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public static class LayerMaskUtilities
    {
        public static bool IsLayerInMask(int layer, LayerMask mask) => ((1 << layer) & mask) > 0;
        public static bool IsLayerInMask(RaycastHit2D hit, LayerMask mask) => IsLayerInMask(hit.collider.gameObject.layer, mask);
    }
}
