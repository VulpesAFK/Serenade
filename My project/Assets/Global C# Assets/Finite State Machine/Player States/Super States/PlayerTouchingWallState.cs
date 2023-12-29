using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    // v Condition to check all local surrounding
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    // v Variables to store all input from the main input handler script
    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        // t Store all bool results from the main player to be translated here
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();

        // t If the player detects a corner approaching
        if (isTouchingWall && !isTouchingLedge)
        {
            // t Note the position of deetection
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t Store all the necessary inputs from the input handler script
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        // t If there is a jump input detected though regardless as there is a natural wall touch
        if (jumpInput)
        {
            // t Check the direction the player is facing via what wall is being touched
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            // t Switch to the wall jump state
            stateMachine.ChangeState(player.WallJumpState);
        }

        // t Touching the wall and not grabbing the wall nearby
        else if (isGrounded && !grabInput)
        {
            // t Switch to the idle state
            stateMachine.ChangeState(player.IdleState);
        }

        // t Not touching the wall or the player is moving away from the wall without grabbing
        else if (!isTouchingWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            // t The player is current falling now
            stateMachine.ChangeState(player.InAirState);
        }

        // t Finding that a corner is coming up in the nearby surroundsings
        else if (isTouchingWall && !isTouchingLedge)
        {
            //t Switch states to be climbing
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
