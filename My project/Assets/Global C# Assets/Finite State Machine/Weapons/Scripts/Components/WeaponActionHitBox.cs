using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private CoreComp<Movement> movement;

        protected override void Start()
        {
            base.Start();

            movement = new CoreComp<Movement>(Core);
        }

        private void HandleAttackAction() {
            Debug.Log($"!! {movement.Component.FacingDirection}");
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
