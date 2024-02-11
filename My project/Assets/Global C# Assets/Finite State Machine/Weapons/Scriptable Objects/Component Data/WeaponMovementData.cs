using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponMovementData : ComponentData<AttackMovement>
    {
        protected override void SetCompomentDependencies() {
            ComponentDependeny = typeof(WeaponMovement);
        }
    }
}
