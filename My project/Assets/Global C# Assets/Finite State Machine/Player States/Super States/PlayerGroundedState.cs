using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // v Variables to store inputs from the player input handler
    protected int xInput;
    protected int yInput;
    private bool grabInput;
    private bool jumpInput;
    private bool dashInput;

    // v Surrounding check variables to store the player surroundings
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    protected bool isTouchingCeiling;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        // t Storing the near surroundings of the player
        isGrounded = core.Collision.Ground;
        isTouchingWall = core.Collision.WallFront;
        isTouchingLedge = core.Collision.Ledge;
        isTouchingCeiling = core.Collision.Ceiling;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t Checking the inputs from the player input handler
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (player.InputHandler.AttackInput[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }

        else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        // t If the input is allowed and there is still jump left
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            // t Now jumping
            stateMachine.ChangeState(player.JumpState);
        }

        // t If the player is now not grounded on the ground
        else if (!isGrounded)
        {
            // t Start the normal jump coyote jump
            player.InAirState.StartCoyoteTime();
            // t Change the state in the free fall state
            stateMachine.ChangeState(player.InAirState);
        }

        // t If the player is touching the wall and there is grab input
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            // t Decrease the amount of the jumps by one
            player.JumpState.DecreaseAmountOfJumpsLeft();
            // t Change the state to the wall grab state
            stateMachine.ChangeState(player.WallGrabState);
        }

        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
