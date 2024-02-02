using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponMovement : WeaponComponent
    {
        private Movement movement;
        private Movement Movement => movement ??= Core.GetCoreComponent<Movement>();
        private WeaponMovementData data;

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<WeaponMovementData>();
        }

        private void HandleStartMovement() {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
            Movement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, Movement.FacingDirection);
        }

        private void HandleFinishMovement() {
            Movement.SetVelocityZero();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnFinishMovement += HandleFinishMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnFinishMovement -= HandleFinishMovement;
        }
    }
}
