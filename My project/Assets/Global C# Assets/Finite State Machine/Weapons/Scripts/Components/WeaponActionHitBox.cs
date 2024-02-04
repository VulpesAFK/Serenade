using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private void HandleAttackAction() {
            Debug.Log("Handle attack");
        }

        # region Assignment from Weapon Animation Event Handler to the main component
        protected override void OnEnable() {
            base.OnEnable();
            // Enable the trigger from the animation to the main function listener
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable() {
            base.OnDisable();
            // Unsubscribe for safty reason incase this object was destroyed in some reason
            eventHandler.OnAttackAction -= HandleAttackAction;
        }
        # endregion
    }
}
