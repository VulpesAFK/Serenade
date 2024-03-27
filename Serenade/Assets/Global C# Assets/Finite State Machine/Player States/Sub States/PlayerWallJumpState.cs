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

        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
            isTouchingWall = Collision.WallFront;
                // Determine the direction of the jump depending on comparing a side

            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);

            player.InputHandler.UseJumpInput();

            // player.JumpState.ResetAmountOfJumpsLeft();

            Movement?.SetVelocity(playerData.WallJumpVelocity, playerData.WallJumpAngle, wallJumpDirection);

            Movement?.CheckIfShouldFlip(wallJumpDirection);
       
            // player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // t Update the animator with the x and y axis to update the fall animation when the animation ends
            player.Anim.SetFloat("yVelocity", Mathf.Max(Movement.CurrentVelocity.y, -10));
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