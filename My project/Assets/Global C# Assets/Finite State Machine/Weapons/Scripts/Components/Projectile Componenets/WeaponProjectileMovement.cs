using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{

    // The movement projectile component is responsible for applying a velocity to the projectile
    public class WeaponProjectileMovement : WeaponProjectileComponent
    {
        // Allows for the option of continuous velocity like a self-powered entity
        [field: SerializeField] public bool ApplyContinuously { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        protected override void InIt()
        {
            base.InIt();
            SetVelocity();

        }

        // 
        private void SetVelocity() => rb.velocity = Speed * transform.right;
        
        protected override void FixedUpdate() {
            base.FixedUpdate();
            if (!ApplyContinuously) return;
            SetVelocity();
        }
    }
}
