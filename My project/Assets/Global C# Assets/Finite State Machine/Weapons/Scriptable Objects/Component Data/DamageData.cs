using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class DamageData : ComponentData<AttackDamage>
    {
        protected override void SetCompomentDependencies() {
            ComponentDependeny = typeof(WeaponDamage);
        }
    }
}
