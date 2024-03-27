using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    public class PlayerCrouchMoveState : PlayerGroundedState {
        public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }

        public override void Enter() {
            base.Enter();

            player.SetColliderHeight(playerData.CrouchColliderHeight);
        }

        public override void Exit() {
            base.Exit();

            player.SetColliderHeight(playerData.StandColliderHeight);
        }


        public override void LogicUpdate() {
            base.LogicUpdate();

            if (!isExitingState) {
                Movement?.CheckIfShouldFlip(xInput);
                Movement?.SetVelocityX(playerData.CrouchMovementVelocity * Movement.FacingDirection);
            }
        }
    }
}
