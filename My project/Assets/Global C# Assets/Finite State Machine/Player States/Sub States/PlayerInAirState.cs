using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // v Input variables from the input handler script
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    // v Bools declared to store the surrounding checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    // v States whether the coyote should start or not
    private bool coyoteTime;
    // v States whether the coyote time for the wall jump should start or not
    private bool wallJumpCoyoteTime;
    // v The time the wall jump coyote time has started
    private float startWallJumpCoyoteTime;

    // v Variable used to decalre whether the player is jumping from thia local state
    private bool isJumping;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        // t Set the previous frame properties to the current so they hold the properties from the state behind
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        // t Check the surrounds of the player with the ground and both forwards and backwards
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();

        // t Checks whether the player is gonna ledge
        isTouchingLedge = player.CheckIfTouchingLedge();

        // t Checks if the player is touching a wall but not touching a ledge 
        if (isTouchingWall && !isTouchingLedge)
        {
            // t Immediately set the position when true to prevent a miss calculation
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        // t All surroundings are clear and the previous frame was the last time a wall was encountered 
        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            // t Start the timer when conditions are met
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

        // t turn back all conditions to false to prevent a logic update store error
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t Checks whether the coyote timer should start
        CheckCoyoteTime();
        // t Chekcs whether the wall jump coyote timer should start 
        CheckWallJumpCoyoteTime();

        // t Receieve all inputs for the player to be use in this current state
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        // t Test after the jump input is received whether it is a short jump or a long jump
        CheckJumpMultiplier();

        if (player.InputHandler.AttackInput[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }

        else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        // t If player has landed and the player of the y is less than a small leeway to prevent a crash of floats
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            // t Switch to land state
            stateMachine.ChangeState(player.LandState);
        }
        
        // t If the player is going to jump sand there is a wall either in front or behind or the timer is on from the coyote time
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            // t Stop the timer from continuing 
            StopWallJumpCoyoteTime();
            // t Check the ensure that it is up to date
            isTouchingWall = player.CheckIfTouchingWall();
            // t Determine the direction of the jump depending on comparing a side
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            // t Switch to new state
            stateMachine.ChangeState(player.WallJumpState);
        }

        // t If the player a by a corner ledge from the ground wall
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            // t Switch the sate to ledge climbing
            stateMachine.ChangeState(player.LedgeClimbState);
        }


        // t Check if there is an input and there is available counter left to be able to jump
        else if (jumpInput && player.JumpState.CanJump())
        {
            // t Switch to the new state
            stateMachine.ChangeState(player.JumpState);
        }

        // t If the player is wanting to grab the wall that should be in range
        else if(isTouchingWall && grabInput && isTouchingLedge)
        {   
            // t Switch to the new state
            stateMachine.ChangeState(player.WallGrabState);
        }

        // t Check whether touching the wall and that the player is facing the wall at its peak height reach
        else if (isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0.0f)
        {
            // t Switch to a new state
            stateMachine.ChangeState(player.WallSlideState);
        }

        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }

        // t If all conditions fail then we are in free fall 
        else 
        {
            // t Check if we have to flip the player
            core.Movement.CheckIfShouldFlip(xInput);
            // t Move the player whilst in the air 
            core.Movement.SetVelocityX(playerData.MovementVelocity * xInput);

            // t Provide the animator with the current x and y values to provide the correct animation value a blend tree
            player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // f Determines the type of jump the player should have depending on the type of and timing of the hold
    private void CheckJumpMultiplier()
    {
        // t If jumping in reference to the jump state
        if (isJumping)
        {
            // t Shorter jump type 
            if (jumpInputStop)
            {
                // t Change the current playing jump velocity with a reduce amount via the data 
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.VariableJumpHeightMultiplier);
                // t Stop all future effects till the next jump is made
                isJumping = false;
            }

            // t Checks if the player has landed then the jump has ended
            else if (core.Movement.CurrentVelocity.y <= 0.0f)
            {
                // t Set the bool to false
                isJumping = false;
            }
        }
    }

    // f Condition to allow for a normal jump coyote time
    private void CheckCoyoteTime()
    {
        // t If the conditions see that a timer is active and the time frame as based
        if (coyoteTime && Time.time > startTime + playerData.CoyoteTime)
        {
            // t Set the timer to false
            coyoteTime = false;
            // t Decrease the number of allowed jumps by one
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    // f Conditions to check whether a wall jump coyote time should start 
    private void CheckWallJumpCoyoteTime()
    {
        // t If the timer is active but the player has passed the time post to be allow to jump
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.CoyoteTime)
        {
            // t Disable the effects of the timer
            wallJumpCoyoteTime = false;
        }
    }

    // f Allow for the start of the coyote normal jump
    public void StartCoyoteTime()
    {
        // t Set to be active
        coyoteTime = true;
    }

    // f Allow for the start of the wall jump coyote jump
    public void StartWallJumpCoyoteTime()
    {
        // t Active the time to start
        wallJumpCoyoteTime = true;
        // t Set the time to the current time 
        startWallJumpCoyoteTime = Time.time;
    }

    // f Stop continuing the wall jump state
    public void StopWallJumpCoyoteTime()
    {
        // t Set the bool to false to disable
        wallJumpCoyoteTime = false;
    }

    // f Allow for communication in the jump state back to this 
    public void SetIsJumping()
    {
        // t Set to true when active in the jump state
        isJumping = true;
    }
}
