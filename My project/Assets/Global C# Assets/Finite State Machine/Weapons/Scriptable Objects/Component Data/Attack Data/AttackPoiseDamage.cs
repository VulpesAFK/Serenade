using System;
using UnityEngine;

namespace FoxTail 
{
    [Serializable]
    public class AttackPoiseDamage : AttackData 
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}