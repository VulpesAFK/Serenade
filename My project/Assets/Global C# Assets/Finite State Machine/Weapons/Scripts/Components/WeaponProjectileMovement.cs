using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectileMovement : WeaponProjectileComponent
    {
        [field: SerializeField] public bool ApplyContinuosly { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        protected override void InIt()
        {
            base.InIt();

            SetVelocity();
            
        }

        private void SetVelocity() => rB.velocity = Speed * transform.right;
        protected override void FixedUpdate() {
            base.FixedUpdate();
            
            if (!ApplyContinuosly) return;
            SetVelocity();
        }
    }
}
