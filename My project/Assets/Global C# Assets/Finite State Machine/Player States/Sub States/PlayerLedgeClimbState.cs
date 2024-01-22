using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    // v Position of the player when the state is active
    private Vector2 detectedPosition;
    // v Position of the corner that will be grabbed
    private Vector2 cornerPosition;

    // v Variables holding the start positions and the stop when we start and finish the ledge climb
    private Vector2 startPosition;
    private Vector2 stopPosition;

    // v Variable to hold whether the player is hanging off
    private bool isHanging;
    // v Variable to state which acting after handing off
    private bool isClimbing;


    private bool isTouchingCeiling;

    // v Store the x and y axis input
    private int xInput;
    private int yInput;
    private bool jumpInput;

    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;

    private Vector2 workSpace;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        // t Make sure that we end the vlimb when the animation is finished
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        // t Set true when the player hits that hold state
        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        // t Freeze the player to its position
        Movement?.SetVelocityZero();
        // t Full updated version of the climb position
        player.transform.position = detectedPosition;

        // t Set the corner position
        cornerPosition = DetermineCornerPosition();

        // t Positions to start and stop the whole climb animation
        startPosition.Set(cornerPosition.x - (Movement.FacingDirection * playerData.StartOffset.x), cornerPosition.y - playerData.StartOffset.y);
        stopPosition.Set(cornerPosition.x + (Movement.FacingDirection * playerData.StopOffset.x), cornerPosition.y + playerData.StopOffset.y);
    
        // t Set the player transofmr to the start
        player.transform.position = startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        // t Reset all the important bools
        isHanging = false;

        // t If the player chose to climb up
        if (isClimbing)
        {
            // t Make sure the player is at the final stopping position
            player.transform.position = stopPosition;
            // t Reset the boolean after the player has finished climbing up
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t When the player is finished climbing up the wall
        if (isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }

            else 
            {
            // t Set the new animation to idle
            stateMachine.ChangeState(player.IdleState);
            }
        }

        // t Player is still midst holding 
        else 
        {
            // t Fetching the x and y iput from the input handler
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;

            // t Freeze the player
            Movement?.SetVelocityZero();

            // t Makre sure the player is at the correct ledge climb position
            player.transform.position = startPosition;
            
            // t Walk jump from holding the ledge
            if (jumpInput && !isClimbing)
            {
                // t Check for if there is a wall in front however the player is climbing so there is going to be 
                player.WallJumpState.DetermineWallJumpDirection(true);
                // t Switch to the wall jump state
                stateMachine.ChangeState(player.WallJumpState);
            }

            // t Check if the player wants to climb
            else if (xInput == Movement?.FacingDirection && isHanging && !isClimbing)
            {

                CheckForSpace();

                // t Player is now climbing 
                isClimbing = true;
                // t Set active command to the animator
                player.Anim.SetBool("climbLedge", true);
            }

            // t Player wants to drop
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                // t Switch to free fall state
                stateMachine.ChangeState(player.InAirState);
            }   

        }
    }

    // f Used to set the position the player is at to start the ledge climb
    public void SetDetectedPosition(Vector2 position)
    {
        // t Set arguement to the private vector2
        detectedPosition = position;
    }

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * Movement.FacingDirection * 0.015f), Vector2.up, playerData.StandColliderHeight, Collision.WhatIsGround);
        player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }

    private Vector2 DetermineCornerPosition()
    {
        // t Find the location of the wall in front of the player
        RaycastHit2D xHit = Physics2D.Raycast(Collision.WallCheck.position, Vector2.right * Movement.FacingDirection, Collision.WallCheckDistance, Collision.WhatIsGround);
        // t Determine the x distance from the player to the wall
        float xDistance = xHit.distance;

        // t Set a workspace to make a variable to signla the amount to add on to the position
        workSpace.Set((xDistance + 0.015f) * Movement.FacingDirection, 0f);
        // t Raycast to note the position of the grounf that the player will appear to 
        RaycastHit2D yHit = Physics2D.Raycast(Collision.LedgeCheckHorizontal.position + (Vector3)(workSpace), Vector2.down, Collision.LedgeCheckHorizontal.position.y - Collision.WallCheck.position.y + 0.015f,  Collision.WhatIsGround);
        // t Determine the y distance
        float yDistance = yHit.distance;

        // t Formulate the final vector2 to be returned
        workSpace.Set(Collision.WallCheck.position.x + (xDistance * Movement.FacingDirection), Collision.LedgeCheckHorizontal.position.y - yDistance);

        // t return the final result
        return workSpace;

    }
}
