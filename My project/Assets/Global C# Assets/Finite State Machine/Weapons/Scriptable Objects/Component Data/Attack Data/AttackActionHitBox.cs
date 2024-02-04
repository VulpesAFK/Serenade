using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    [Serializable]
    public class AttackActionHitBox : AttackData {
        // These are properties that will have to be defined for each weaponry attack
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}
