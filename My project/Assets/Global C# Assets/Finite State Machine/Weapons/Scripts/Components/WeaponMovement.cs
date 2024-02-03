using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponMovement : WeaponComponent<WeaponMovementData, AttackMovement>
    {
        private Movement movement;
        private Movement Movement => movement ??= Core.GetCoreComponent<Movement>();

        private void HandleStartMovement() {
            Movement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, Movement.FacingDirection);
        }

        private void HandleFinishMovement() {
            Movement.SetVelocityZero();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnFinishMovement += HandleFinishMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnFinishMovement -= HandleFinishMovement;
        }
    }
}
