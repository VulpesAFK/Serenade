using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerJumpState : PlayerAbilitiesState
    {

        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) {
            playerStateData.ResetAmountOfJumps(playerData.AmountOfJumps);
        }

        public override void Enter() {
            base.Enter();

            //TODO - Fix to stop an attachment to the jump input
            //TODO - Doesn't really work with 2+ jumps 
            player.InputHandler.UseJumpInput();
     
            Movement?.SetVelocityY(playerData.JumpVelocity);

            Debug.Log(playerStateData.amountOfJumpsLeft);

            isAbilityDone = true;
         
            playerStateData.DecreaseAmountOfJumpsLeft();

            player.InAirState.SetIsJumping();
        }

        public bool CanJump { get => playerStateData.amountOfJumpsLeft > 0 ? true : false; }
    }
}
