using System;
using UnityEngine;

namespace FoxTail 
{
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponPoiseDamage);
        }
    }
}