using UnityEngine.Events;
using UnityEngine;

namespace FoxTail {
    /*
        * Poise damage component is responsible for using information from the hitbox component used to damage poise from the necessary layer mask
        * The amount of poise comes from the package system
    */
    public class WeaponProjectilePoiseDamage : WeaponProjectileComponent {
        public UnityEvent OnPoiseDamage;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        private float amount;
        private WeaponProjectileHitBox hitBox;

        private void HandleRaycastHit2D(RaycastHit2D[] hits) {
            if (!Active)
                return;

            foreach (var hit in hits)
            {
                // * Check if the object considered is part of the layer mask interested
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                if (!hit.collider.transform.gameObject.TryGetComponent(out IPoiseDamageable poiseDamageable))
                    continue;

                poiseDamageable.DamagePoise(amount);
                OnPoiseDamage?.Invoke();

                return;

            }
        }

        // * Handles checking to see if the data is relevant or not and then extract the necessary information
        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not PoiseDamageDataPackage package)
                return;

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