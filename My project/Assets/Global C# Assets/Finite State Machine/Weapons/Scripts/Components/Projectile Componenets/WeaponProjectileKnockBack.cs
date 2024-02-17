using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxTail {
    /*
        * Knock back responsible for using the information from the hitbox event to knock back any entity on the same layer that was interested and specified from the inspector
        * Strength and angle will come from a package system 
    */
    public class WeaponProjectileKnockBack : WeaponProjectileComponent {
        public UnityEvent OnKnockBack;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        private WeaponProjectileHitBox hitBox;

        private int direction;
        private float strength;
        private Vector2 angle;

        private void HandleRaycastHit2D(RaycastHit2D[] hits) {
            if (!Active)
                return;

            direction = (int)Mathf.Sign(transform.right.x);

            foreach (var hit in hits)
            {
                // * Check if this object is an object that we can damage
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                // * Check if there is a possible component interface to damage
                if (!hit.collider.transform.gameObject.TryGetComponent(out IKnockBackable knockBackable))
                    continue;

                knockBackable.KnockBack(angle, strength, direction);

                OnKnockBack?.Invoke();
            }
        }

        // * Handle checking the data if relevant and extract if it is related 
        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not KnockBackDataPackage knockBackDataPackage)
                return;

            strength = knockBackDataPackage.Strength;
            angle = knockBackDataPackage.Angle;
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
