using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerWallJumpState : PlayerAbilitiesState
    {
        // v Determine the direction of the facing direction similar to facing direction
        private int wallJumpDirection;

        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            
        }
        public override void Enter()
        {
            base.Enter();

            // t Use the input handler to signal that a jump has been used
            player.InputHandler.UseJumpInput();
            // t Reset the amount of jumps left just to make it equal again
            // player.JumpState.ResetAmountOfJumpsLeft();
            // t Set the velocity with a specific velocity and angle and facing direction
            Movement?.SetVelocity(playerData.WallJumpVelocity, playerData.WallJumpAngle, wallJumpDirection);

            // t Test the opposite facing dircetion compared to the facing direction
            Movement?.CheckIfShouldFlip(wallJumpDirection);
            // t Decrease the amount of jumps as the player is jumping
            // player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // t Update the animator with the x and y axis to update the fall animation when the animation ends
            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

            // t If the time increasing is larger then the start of the animation and the fixed wall time to prevent quick back trekking
            if (Time.time >= startTime + playerData.WallJumpTime)
            {
                // t Set to finish and hand back to the abilities states
                isAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall) {
            if (isTouchingWall) { wallJumpDirection = -Movement.FacingDirection; }

            else { wallJumpDirection = Movement.FacingDirection; }
        }
    }
}