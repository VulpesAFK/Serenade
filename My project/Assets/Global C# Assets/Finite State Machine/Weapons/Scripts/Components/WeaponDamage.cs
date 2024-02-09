using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponDamage : WeaponComponent<DamageData, AttackDamage>
    {
        private WeaponActionHitBox hitBox;
        private void HandleDetectCollider2D(Collider2D[] colliders) {
            foreach (var item in colliders) {
                if (item.TryGetComponent(out IDamageable damageable)) {
                    damageable.Damage(currentAttackData.Amount);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<WeaponActionHitBox>();
        }

        protected override void OnEnable() {
            base.OnEnable();
            hitBox.onDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable() {
            base.OnDisable();

            hitBox.onDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}
