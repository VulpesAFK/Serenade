using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{   
    // Rotates the current game object such that transform.right points in the same towards the velocity acting on 
    public class WeaponProjectileRotateTowardsVelocity : WeaponProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var velocity = rb.velocity;

            if (velocity.Equals(Vector3.zero)) {
                return;
            }

            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
