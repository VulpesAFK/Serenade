using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox> {
        // Data that will be defined for all of the attack hitbox data assigned
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }
    }
}
