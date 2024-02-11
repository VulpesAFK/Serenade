using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponKnockBack : WeaponComponent<KnockBackData, AttackKnockBack> {
        private WeaponActionHitBox hitBox;
        private Movement movement;
        private void HandleDetectedCollider2D(Collider2D[] colliders) {
            foreach (var item in colliders) {
                if (item.TryGetComponent(out IKnockBackable knockBackable)) {
                    knockBackable.KnockBack(currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection);
                }
            }
        }

        # region Start() & OnDestroy() Functions
        protected override void Start() {
            base.Start();

            hitBox = GetComponent<WeaponActionHitBox>();
            hitBox.onDetectedCollider2D += HandleDetectedCollider2D;

            movement = Core.GetCoreComponent<Movement>();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            hitBox.onDetectedCollider2D -= HandleDetectedCollider2D;
        }
        # endregion
    }
}
