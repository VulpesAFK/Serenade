using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class KnockBackData : ComponentData<AttackKnockBack>
    {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponKnockBack);
        }
    }
}
