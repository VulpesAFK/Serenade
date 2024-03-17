using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerJumpState : PlayerAbilitiesState
    {
        private int amountOfJumpsLeft;

        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
            amountOfJumpsLeft = playerData.AmountOfJumps;
        }

        public override void Enter() {
            base.Enter();

            player.InputHandler.UseJumpInput();
     
            Movement?.SetVelocityY(playerData.JumpVelocity);

            isAbilityDone = true;
         
            DecreaseAmountOfJumpsLeft();

            player.InAirState.SetIsJumping();
        }

        public bool CanJump { get => amountOfJumpsLeft > 0 ? true : false; }
        public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;
        public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    }
}
