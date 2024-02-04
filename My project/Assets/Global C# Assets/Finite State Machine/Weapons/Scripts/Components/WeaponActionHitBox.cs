using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private event Action<Collider2D[]> onDetectedCollider2D;

        // Core method to fetch all necessary core componenets with generics
        private CoreComp<Movement> movement;

        // Information in regards to the rect variable properties
        // X => The offset x * the facing direction
        // Y => The offset Y
        private Vector2 offset;

        // Collider of all the entities hit
        private Collider2D[] detected;

        private void HandleAttackAction() {
            // Handle the offset with the needed information
            offset.Set (
                transform.position.x + (currentAttackData.HitBox.x * movement.Component.FacingDirection),
                transform.position.y + currentAttackData.HitBox.y
            );

            // Links to the current attack data and data can be found in the main weaponry components
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0) return;
            // Pass into the broadcasted the list of all entities hit to all listeners to handle
            onDetectedCollider2D?.Invoke(detected);

            foreach (var item in detected) {
                print(item.name);
            }
        }


        // Unity starting functions to ftech all necessary components and inheritance
        # region Start() Function
        protected override void Start()
        {
            base.Start();
            // Link with the main core
            movement = new CoreComp<Movement>(Core);
        }
        # endregion

        # region OnEnable() & OnDisable() Functions
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
    
        // Gizmos to view found the size of the attack area
        # region OnDrawGizmos() Function
        private void OnDrawGizmos() {
            // Safety check incase null 
            if (data == null) return;

            foreach (var item in data.AttackData) {
                if (!item.Debug) continue;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
        # endregion
    }
}
