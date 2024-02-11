using System;
using UnityEngine;

namespace FoxTail 
{
    public class WeaponPoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private WeaponActionHitBox hitBox;

        private void HandleDetectedCollider2D(Collider2D[] colliders) {
            foreach (var item in colliders) {
                if (item.TryGetComponent(out IPoiseDamageable poiseDamgeable)) {
                    poiseDamgeable.DamagePoise(currentAttackData.Amount); 
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<WeaponActionHitBox>();
            hitBox.onDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            hitBox.onDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}