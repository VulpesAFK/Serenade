using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        protected override void SetCompomentDependencies() {
            ComponentDependeny = typeof(WeaponSprite);
        }
    }
}
