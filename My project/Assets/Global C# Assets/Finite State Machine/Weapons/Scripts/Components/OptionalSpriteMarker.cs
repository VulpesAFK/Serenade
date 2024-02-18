using UnityEngine;

namespace FoxTail {
    public class OptionalSpriteMarker : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    }
}