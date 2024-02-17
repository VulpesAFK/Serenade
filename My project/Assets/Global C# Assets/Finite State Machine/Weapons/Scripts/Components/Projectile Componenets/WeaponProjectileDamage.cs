using UnityEngine;

namespace FoxTail {
    /*
        * Uses the assist of the hitbox component provided to damage to any entity relevant layermask
        * Damage is provided from the damage projectile package 
    */
    public class WeaponProjectileDamage : WeaponProjectileComponent {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public bool SetInactiveAfterDamage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }

        private WeaponProjectileHitBox hitBox;
        private float amount;
        private float lastDamageTime;

        protected override void InIt()
        {
            base.InIt();
            
            SetActive(true);

            lastDamageTime = Mathf.NegativeInfinity;
        }

        private void HandleRaycastHit2D(RaycastHit2D[] hits) {
            if (!Active) return;

            if (Time.time < lastDamageTime + Cooldown) return;

            foreach (var hit in hits) {
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                if (!hit.transform.TryGetComponent(out IDamageable damageable)) 
                    continue;

                damageable.Damage(amount);

                lastDamageTime = Time.time;

                if (SetInactiveAfterDamage) {
                    SetActive(false);
                }

                return;
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