using UnityEngine;

namespace FoxTail {
    /*
        * Uses the assist of the hitbox component provided to damage to any entity relevant layermask
        * Damage is provided from the damage projectile package 
    */
    public class WeaponProjectileDamage : WeaponProjectileComponent {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private WeaponProjectileHitBox hitBox;
        private float amount;

        private void HandleRaycastHit2D(RaycastHit2D[] hits) {
            foreach (var hit in hits) {
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) return;

                if (hit.transform.TryGetComponent(out IDamageable damageable)) {
                    damageable.Damage(amount);
                }
            }
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DamageDataPackage package) {
                return;
            }

            amount = package.Amount;
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<WeaponProjectileHitBox>();

            hitBox.OnRaycastHit2D += HandleRaycastHit2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D -= HandleRaycastHit2D;
        }
    }
}