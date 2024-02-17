using System;
using UnityEngine;

namespace FoxTail {
    [Serializable]
    public class PoiseDamageDataPackage : ProjectileDataPackage {
        [field: SerializeField] public float Amount { get; private set; }
    }
}