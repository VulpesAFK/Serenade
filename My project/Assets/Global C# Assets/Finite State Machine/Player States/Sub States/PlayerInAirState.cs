using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerInAirState : PlayerState
    {
        // Input variables from the input handler script
        private int xInput;
        private bool jumpInput;
        private bool jumpInputStop;
        private bool grabInput;
        private bool dashInput;

        // Bools declared to store the surrounding checks
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isTouchingWallBack;
        private bool oldIsTouchingWall;
        private bool oldIsTouchingWallBack;
        private bool isTouchingLedge;

        // States whether the coyote should start or not
        private bool coyoteTime;
        // States whether the coyote time for the wall jump should start or not
        private bool wallJumpCoyoteTime;
        // The time the wall jump coyote time has started
        private float startWallJumpCoyoteTime;

        // Variable used to decalre whether the player is jumping from thia local state
        private bool isJumping;

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
        private Collision collision;

        public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {

        }

        public override void DoChecks()
        {
            base.DoChecks();

            // Set the previous frame properties to the current so they hold the properties from the state behind
            oldIsTouchingWall = isTouchingWall;
            oldIsTouchingWallBack = isTouchingWallBack;

            // Check the surrounds of the player with the ground and both forwards and backwards
            if (Collision)
            {
                isGrounded = Collision.Ground;
                isTouchingWall = Collision.WallFront;
                isTouchingWallBack = Collision.WallBack;
                isTouchingLedge = Collision.LedgeHorizontal;
            }

            // Checks if the player is touching a wall but not touching a ledge 
            if (isTouchingWall && !isTouchingLedge)
            {
                // Immediately set the position when true to prevent a miss calculation
                // player.LedgeClimbState.SetDetectedPosition(player.transform.position);
            }

            // All surroundings are clear and the previous frame was the last time a wall was encountered 
            if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
            {
                // Start the timer when conditions are met
                StartWallJumpCoyoteTime();
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            // Turn back all conditions to false to prevent a logic update store error
            oldIsTouchingWall = false;
            oldIsTouchingWallBack = false;
            isTouchingWall = false;
            isTouchingWallBack = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // Checks whether the coyote timer should start
            CheckCoyoteTime();
            // Chekcs whether the wall jump coyote timer should start 
            CheckWallJumpCoyoteTime();

            // Receieve all inputs for the player to be use in this current state
            xInput = player.InputHandler.NormInputX;
            jumpInput = player.InputHandler.JumpInput;
            jumpInputStop = player.InputHandler.JumpInputStop;
            grabInput = player.InputHandler.GrabInput;
            dashInput = player.InputHandler.DashInput;

            // Condition
            bool conditionToLand = isGrounded && Movement?.CurrentVelocity.y < 0.01f;
            bool conditionToWallJumpFromAir = jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime);

            // Test after the jump input is received whether it is a short jump or a long jump
            CheckJumpMultiplier();

            if (player.InputHandler.AttackInput[(int)CombatInputs.primary])
            {
                // stateMachine.ChangeState(player.PrimaryAttackState);
            }

            else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary])
            {
                // stateMachine.ChangeState(player.SecondaryAttackState);
            }


            //NOTE - COMPLETED
            else if (conditionToLand) {
                // stateMachine.ChangeState(player.LandState);
            }
            
            else if (conditionToWallJumpFromAir) {
                // Stop the timer from continuing 
                StopWallJumpCoyoteTime();
                // Check the ensure that it is up to date
                isTouchingWall = Collision.WallFront;
                // Determine the direction of the jump depending on comparing a side

                // player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);

                // Switch to new state

                // stateMachine.ChangeState(player.WallJumpState);
            }

            // If the player a by a corner ledge from the ground wall
            else if (isTouchingWall && !isTouchingLedge && !isGrounded)
            {
                // Switch the sate to ledge climbing
                // stateMachine.ChangeState(player.LedgeClimbState);
            }


            // Check if there is an input and there is available counter left to be able to jump
            // else if (jumpInput && player.JumpState.CanJump())
            // {
            //     // Switch to the new state
            //     stateMachine.ChangeState(player.JumpState);
            // }

            // If the player is wanting to grab the wall that should be in range
            else if(isTouchingWall && grabInput && isTouchingLedge)
            {   
                // Switch to the new state
                // stateMachine.ChangeState(player.WallGrabState);
            }

            //NOTE - COMPLETED
            else if (isTouchingWall && xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0.01f)
            {
                // Switch to a new state
                // stateMachine.ChangeState(player.WallSlideState);
            }

            // else if (dashInput && player.DashState.CheckIfCanDash())
            // {
            //     stateMachine.ChangeState(player.DashState);
            // }

            // If all conditions fail then we are in free fall 
            else 
            {
                // Check if we have to flip the player
                Movement?.CheckIfShouldFlip(xInput);
                // Move the player whilst in the air 
                Movement?.SetVelocityX(playerData.MovementVelocity * xInput);

                // Provide the animator with the current x and y values to provide the correct animation value a blend tree
                player.Anim.SetFloat("yVelocity", Math.Max(Movement.CurrentVelocity.y, -10));
                player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        // Determines the type of jump the player should have depending on the type of and timing of the hold
        private void CheckJumpMultiplier()
        {
            // If jumping in reference to the jump state
            if (isJumping)
            {
                // Shorter jump type 
                if (jumpInputStop)
                {
                    // Change the current playing jump velocity with a reduce amount via the data 
                    Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.VariableJumpHeightMultiplier);
                    // Stop all future effects till the next jump is made
                    isJumping = false;
                }

                // Checks if the player has landed then the jump has ended
                else if (Movement?.CurrentVelocity.y <= 0.0f)
                {
                    // Set the bool to false
                    isJumping = false;
                }
            }
        }

        // Condition to allow for a normal jump coyote time
        private void CheckCoyoteTime()
        {
            // If the conditions see that a timer is active and the time frame as based
            if (coyoteTime && Time.time > startTime + playerData.CoyoteTime)
            {
                // Set the timer to false
                coyoteTime = false;
                // Decrease the number of allowed jumps by one
                player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        // Conditions to check whether a wall jump coyote time should start 
        private void CheckWallJumpCoyoteTime()
        {
            // If the timer is active but the player has passed the time post to be allow to jump
            if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.CoyoteTime)
            {
                // Disable the effects of the timer
                wallJumpCoyoteTime = false;
            }
        }

        // Allow for the start of the coyote normal jump
        public void StartCoyoteTime()
        {
            // Set to be active
            coyoteTime = true;
        }

        // Allow for the start of the wall jump coyote jump
        public void StartWallJumpCoyoteTime()
        {
            // Active the time to start
            wallJumpCoyoteTime = true;
            // Set the time to the current time 
            startWallJumpCoyoteTime = Time.time;
        }

        // Stop continuing the wall jump state
        public void StopWallJumpCoyoteTime()
        {
            // Set the bool to false to disable
            wallJumpCoyoteTime = false;
        }

        // Allow for communication in the jump state back to this 
        public void SetIsJumping()
        {
            // Set to true when active in the jump state
            isJumping = true;
        }
    }
}
