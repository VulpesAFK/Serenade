using System;
using UnityEngine;

namespace FoxTail {
    /*
        * Damage package is related to the amount of damage that a projectile 
        * If there is interest in regards to this component then it can be access through type DamageDataPackage
    */
    [Serializable]
    public class DamageDataPackage : ProjectileDataPackage {
        [field: SerializeField] public float Amount { get; private set; }
    }
}
