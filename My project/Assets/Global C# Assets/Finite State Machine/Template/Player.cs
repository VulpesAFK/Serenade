using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates;
using UnityEngine;
using Unity.VisualScripting;

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

    //REVIEW - Edit Me Later
    private Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= Core.GetCoreComponent<Collision>(); }
    private Collision collision;


    [SerializeField] private PlayerData playerData;

    // v Reference to a vector2 to hold a custom vector to be assigned to the rigidbody2d
    private Vector2 workSpace;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
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
        JumpState  = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        // DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        // PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        // SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");




        //REVIEW - FIX THIS HAYWIRE OF TRANSITIONS AND FUNCTION LAMBDA CONDITIONS
        //SECTION - Making Input handling central to the player and not state dependant

        //TODO - WATCH BARDENT VIDEO ON JUMP TO VERIFY THE FUNCTIONS ON HOW IT WORKS AT THE MAIN NECESSARY FUNCTION ON THE PRIME FUNCTIONS TRANSITION: IDLE/MOVE -> JUMP -> INAIR -> LAND -> MOVE/IDLE, COYOTE JUMPING METHOD AND !! INPUT REMEMBERANCE
        //LINK - 
        //!SECTION

        //ANCHOR - 

        #region Idle Complete
        Func<bool> IdleToMove() => () => !IdleState.isExitingState && InputHandler.NormInputX != 0;
        Func<bool> IdleToCrouchIdle() => () => !IdleState.isExitingState && InputHandler.NormInputY == -1;

        StateMachine.AddSubTransition(IdleState, MoveState, IdleToMove());
        StateMachine.AddSubTransition(IdleState, CrouchIdleState, IdleToCrouchIdle());
        #endregion

        #region Move Complete
        Func<bool> MoveToIdle() => () => !MoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> MoveToCrouchMove() => () => !MoveState.isExitingState && InputHandler.NormInputY == -1;

        StateMachine.AddSubTransition(MoveState, IdleState, MoveToIdle());
        StateMachine.AddSubTransition(MoveState, CrouchMoveState, MoveToCrouchMove());
        #endregion
        
        #region CrouchIdle Complete
        Func<bool> CrouchIdleToIdle() => () => !CrouchIdleState.isExitingState && !Collision.Ceiling && InputHandler.NormInputY != -1;
        Func<bool> CrouchIdleToCrouchMove() => () => !CrouchIdleState.isExitingState && InputHandler.NormInputX != 0;

        StateMachine.AddSubTransition(CrouchIdleState, IdleState, CrouchIdleToIdle());
        StateMachine.AddSubTransition(CrouchIdleState, CrouchMoveState, CrouchIdleToCrouchMove());
        #endregion

        #region CrouchMove Complete
        Func<bool> CrouchMoveToCrouchIdle() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> CrouchMoveToMove() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputY != -1 && !Collision.Ceiling;

        StateMachine.AddSubTransition(CrouchMoveState, CrouchIdleState, CrouchMoveToCrouchIdle());
        StateMachine.AddSubTransition(CrouchMoveState, MoveState, CrouchMoveToMove());
        #endregion

        /*
            * Grounded State 
            ? Incompleted 
        */
        Func<bool> GroundedStateToJump() => () => InputHandler.JumpInput && !Collision.Ceiling && JumpState.CanJump; 
        Func<bool> GroundedStateToInAir() => () => !Collision.Ground; 

        StateMachine.AddSuperTransition(IdleState, JumpState, GroundedStateToJump());  
        StateMachine.AddSuperTransition(IdleState, InAirState, GroundedStateToInAir());  

        #region Abilities Complete
        Func<bool> AbiltiesStateToInAir() => () => StateMachine.CurrentState.isAbilityDone && !(Collision.Ground && Movement?.CurrentVelocity.y < 0.01f);
        Func<bool> AbiltiesStateToIdle() => () => StateMachine.CurrentState.isAbilityDone && Collision.Ground && Movement?.CurrentVelocity.y < 0.01f;

        StateMachine.AddSuperTransition(JumpState, InAirState, AbiltiesStateToInAir());  
        StateMachine.AddSuperTransition(JumpState, IdleState, AbiltiesStateToIdle());  
        #endregion

        // ? Incomplete
        #region InAir 
        Func<bool> InAirToLand() => () => Collision.Ground && Movement?.CurrentVelocity.y < 0.01f;
        Func<bool> InAirToWallSlide() => () => Collision.WallFront && InputHandler.NormInputX == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0.01f;

        StateMachine.AddSubTransition(InAirState, LandState, InAirToLand());
        StateMachine.AddSubTransition(InAirState, WallSlideState, InAirToWallSlide());
        #endregion

        /* 
            * Land State 
            ! Completed
        */
        Func<bool> LandToIdle() => () => !LandState.isExitingState && InputHandler.NormInputX == 0 && LandState.isAnimationFinished;
        Func<bool> LandToMove() => () => !LandState.isExitingState && InputHandler.NormInputX != 0;

        StateMachine.AddSubTransition(LandState, IdleState, LandToIdle());
        StateMachine.AddSubTransition(LandState, MoveState, LandToMove());

        // ? Incomplete
        #region WallGrab
        Func<bool> WallGrabToWallSlide() => () => !WallGrabState.isExitingState && InputHandler.NormInputY > 0.0f;
        Func<bool> WallGrabToWallClimb() => () => !WallGrabState.isExitingState && (InputHandler.NormInputY < 0.0f || !InputHandler.GrabInput);

        #endregion

        // ? Incomplete
        #region TouchingWall
        Func<bool> TouchingWallToIdle() => () => Collision.Ground && !InputHandler.GrabInput;
        Func<bool> TouchingWallToInAir() => () => !Collision.WallFront || (InputHandler.NormInputX != Movement?.FacingDirection && !InputHandler.GrabInput);

        StateMachine.AddSuperTransition(WallSlideState, IdleState, TouchingWallToIdle());
        StateMachine.AddSuperTransition(WallSlideState, InAirState, TouchingWallToInAir());
        #endregion        
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
