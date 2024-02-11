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

        protected override void Start()
        {
            base.Start();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnFinishMovement += HandleFinishMovement;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnFinishMovement -= HandleFinishMovement;
        }
    }
}
