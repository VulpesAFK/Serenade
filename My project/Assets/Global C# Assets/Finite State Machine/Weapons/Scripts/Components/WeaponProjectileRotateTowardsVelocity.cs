using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectileRotateTowardsVelocity : WeaponProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var velocity = rB.velocity;

            if (velocity.Equals(Vector3.zero)) return;

            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}