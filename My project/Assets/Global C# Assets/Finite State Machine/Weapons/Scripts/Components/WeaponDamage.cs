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

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<WeaponActionHitBox>();
            hitBox.onDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            hitBox.onDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}
