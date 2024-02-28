using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates;
using UnityEngine;

public class Player : MonoBehaviour
{
    // External properties from the core functions
    public Core Core { get; private set; }
    // v All references needed to components and external scripts for input and animation
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    // v Player data for fixed properties
    [SerializeField] private PlayerData playerData;

    // v Reference to a vector2 to hold a custom vector to be assigned to the rigidbody2d
    private Vector2 workSpace;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    // public PlayerJumpState JumpState { get; private set; }
    // public PlayerInAirState InAirState { get; private set; }
    // public PlayerLandState LandState { get; private set; }
    // public PlayerWallSlideState WallSlideState { get; private set; }
    // public PlayerWallGrabState WallGrabState { get; private set; }
    // public PlayerWallClimbState WallClimbState { get; private set; }
    // public PlayerWallJumpState WallJumpState { get; private set; }
    // public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    // public PlayerDashState DashState { get; private set; }
    // public PlayerAttackState PrimaryAttackState { get; private set; }
    // public PlayerAttackState SecondaryAttackState { get; private set; }

    private void Awake() {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");



        //REVIEW - FIX THIS HAYWIRE OF TRANSITIONS AND FUNCTION LAMBDA CONDITIONS

        // * Idle
        Func<bool> IdleToMove() => () => !IdleState.isExitingState && IdleState.xInput != 0;
        Func<bool> IdleToCrouchIdle() => () => !IdleState.isExitingState && IdleState.yInput == -1;

        StateMachine.AddTransition(IdleState, MoveState, IdleToMove());
        StateMachine.AddTransition(IdleState, CrouchIdleState, IdleToCrouchIdle());

        // * Move
        Func<bool> MoveToIdle() => () => !MoveState.isExitingState && MoveState.xInput == 0;
        Func<bool> MoveToCrouchMove() => () => !MoveState.isExitingState && MoveState.yInput == -1;

        StateMachine.AddTransition(MoveState, IdleState, MoveToIdle());
        StateMachine.AddTransition(MoveState, CrouchMoveState, MoveToCrouchMove());

        // * CrouchIdle
        Func<bool> CrouchIdleToIdle() => () => !CrouchIdleState.isExitingState && !CrouchIdleState.isTouchingCeiling && CrouchIdleState.yInput != -1;
        Func<bool> CrouchIdleToCrouchMove() => () => !CrouchIdleState.isExitingState && CrouchIdleState.xInput != 0;

        StateMachine.AddTransition(CrouchIdleState, IdleState, CrouchIdleToIdle());
        StateMachine.AddTransition(CrouchIdleState, CrouchMoveState, CrouchIdleToCrouchMove());

        // * CrouchMove
        Func<bool> CrouchMoveToCrouchIdle() => () => !CrouchMoveState.isExitingState && CrouchMoveState.xInput == 0;
        Func<bool> CrouchMoveToMove() => () => !CrouchMoveState.isExitingState && CrouchMoveState.yInput != -1 && !CrouchMoveState.isTouchingCeiling;

        StateMachine.AddTransition(CrouchMoveState, CrouchIdleState, CrouchMoveToCrouchIdle());
        StateMachine.AddTransition(CrouchMoveState, MoveState, CrouchMoveToMove());



        // JumpState  = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        // InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        // LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        // WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        // WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        // WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        // WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        // LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        // DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        // PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        // SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start() 
    {
        // t Fetching the animator from the player
        Anim = GetComponent<Animator>();
        // t Fetching the script handling input
        InputHandler = GetComponent<PlayerInputHandler>();
        // t Fetching the rigidbody2d
        RB = GetComponent<Rigidbody2D>();

        MovementCollider = GetComponent<BoxCollider2D>();

        DashDirectionIndicator = transform.Find("Dash Direction Indicator");

        // // t Set the default animation to the default idle state
        // StateMachine.Initialize(IdleState);

        StateMachine.ChangeState(IdleState);
    }

    private void Update() 
    {
        Core.LogicUpdate();
        // t Update all logic tied to the logic update in the current running state
        StateMachine.Tick();

    }

    private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }
}
