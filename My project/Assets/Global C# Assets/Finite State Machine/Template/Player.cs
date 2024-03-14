using System;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine Instantiation Properties 
    public PlayerStateMachine StateMachine { get; private set; }
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Core Components
    public Core Core { get; private set; }
    private Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= Core.GetCoreComponent<Collision>(); }
    private Collision collision;
    #endregion

    #region Player Components 
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    private Vector2 workSpace;

    #region Player States
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
    #endregion

    //TODO - REIMPLEMENT THESE SECTIONS OF THE STATES LATER AFTER LOOKING AT THE MECHANICS OF THE NEW ATTACKING STATE
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

        //TODO - INSTANTIATE THE NEW STATES AFTER ALL IS FIXED WITH THE LAST TODO ITEM
        // DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        // PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        // SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");




        //REVIEW - FIX THIS HAYWIRE OF TRANSITIONS AND FUNCTION LAMBDA CONDITIONS
        //TODO - WATCH BARDENT VIDEO ON JUMP TO VERIFY THE FUNCTIONS ON HOW IT WORKS AT THE MAIN NECESSARY FUNCTION ON THE PRIME FUNCTIONS TRANSITION: IDLE/MOVE -> JUMP -> INAIR -> LAND -> MOVE/IDLE, COYOTE JUMPING METHOD AND !! INPUT REMEMBERANCE

        void At(PlayerState from, PlayerState to, Func<bool> transition) => StateMachine.AddSubTransition(from, to, transition); 

        #region Idle Complete
        Func<bool> IdleToMove() => () => !IdleState.isExitingState && InputHandler.NormInputX != 0;
        Func<bool> IdleToCrouchIdle() => () => !IdleState.isExitingState && InputHandler.NormInputY == -1;

        At(IdleState, MoveState, IdleToMove());
        At(IdleState, CrouchIdleState, IdleToCrouchIdle());
        #endregion

        #region Move Complete
        Func<bool> MoveToIdle() => () => !MoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> MoveToCrouchMove() => () => !MoveState.isExitingState && InputHandler.NormInputY == -1;

        At(MoveState, IdleState, MoveToIdle());
        At(MoveState, CrouchMoveState, MoveToCrouchMove());
        #endregion
        
        #region CrouchIdle Complete
        Func<bool> CrouchIdleToIdle() => () => !CrouchIdleState.isExitingState && !Collision.Ceiling && InputHandler.NormInputY != -1;
        Func<bool> CrouchIdleToCrouchMove() => () => !CrouchIdleState.isExitingState && InputHandler.NormInputX != 0;

        At(CrouchIdleState, IdleState, CrouchIdleToIdle());
        At(CrouchIdleState, CrouchMoveState, CrouchIdleToCrouchMove());
        #endregion

        #region CrouchMove Complete
        Func<bool> CrouchMoveToCrouchIdle() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> CrouchMoveToMove() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputY != -1 && !Collision.Ceiling;

        At(CrouchMoveState, CrouchIdleState, CrouchMoveToCrouchIdle());
        At(CrouchMoveState, MoveState, CrouchMoveToMove());
        #endregion

        /*
            * Grounded State 
            ? Incompleted 
        */
        Func<bool> GroundedStateToJump() => () => InputHandler.JumpInput && !Collision.Ceiling && JumpState.CanJump; 
        Func<bool> GroundedStateToInAir() => () => !Collision.Ground && !Collision.Ceiling; 

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
        Func<bool> InAirToWallGrab() => () => Collision.WallFront && InputHandler.GrabInput && Collision.LedgeHorizontal;
        Func<bool> InAirToLedgeClimb() => () => Collision.WallFront && !Collision.LedgeHorizontal && !Collision.Ground;
        //FIXME - REMOVED CONDITION NEEDS TO BE RE_FITTED
        Func<bool> InAirToJump() => () => InputHandler.JumpInput;
        //FIXME - REMOVED CONDITION NEEDS TO BE RE_FITTED
        Func<bool> InAirToWallJump() => () => InputHandler.JumpInput && (Collision.WallFront || Collision.WallBack);

        At(InAirState, LandState, InAirToLand());
        At(InAirState, WallSlideState, InAirToWallSlide());
        At(InAirState, WallJumpState, InAirToWallJump());
        At(InAirState, JumpState, InAirToJump());
        At(InAirState, LedgeClimbState, InAirToLedgeClimb());
        At(InAirState, WallGrabState, InAirToWallGrab());
        #endregion

        #region Land Complete
        Func<bool> LandToIdle() => () => !LandState.isExitingState && InputHandler.NormInputX == 0 && LandState.isAnimationFinished;
        Func<bool> LandToMove() => () => !LandState.isExitingState && InputHandler.NormInputX != 0;

        At(LandState, IdleState, LandToIdle());
        At(LandState, MoveState, LandToMove());
        #endregion


        #region WallGrab Complete
        Func<bool> WallGrabToWallClimb() => () => !WallGrabState.isExitingState && InputHandler.NormInputY > 0.0f;
        Func<bool> WallGrabToWallSlide() => () => !WallGrabState.isExitingState && (InputHandler.NormInputY < 0.0f || !InputHandler.GrabInput);

        At(WallGrabState, WallClimbState, WallGrabToWallClimb());
        At(WallGrabState, WallSlideState, WallGrabToWallSlide());
        #endregion

        #region WallSlide Complete
        Func<bool> WallSlideToWallGrab() => () => !WallSlideState.isExitingState && InputHandler.GrabInput && InputHandler.NormInputY == 0;
        At(WallSlideState, WallGrabState, WallSlideToWallGrab());
        #endregion

        #region WallClimb Complete
        Func<bool> WallClimbToWallGrab() => () => !WallClimbState.isExitingState && InputHandler.NormInputY != 1;
        At(WallClimbState, WallGrabState, WallClimbToWallGrab());
        #endregion

        #region TouchingWall Complete
        Func<bool> TouchingWallToIdle() => () => Collision.Ground && !InputHandler.GrabInput;
        Func<bool> TouchingWallToInAir() => () => !Collision.WallFront || (InputHandler.NormInputX != Movement?.FacingDirection && !InputHandler.GrabInput);
        Func<bool> TouchingWallToLedgeClimb() => () => Collision.WallFront && !Collision.LedgeHorizontal;
        Func<bool> TouchingWallToWallJump() => () => InputHandler.JumpInput;

        StateMachine.AddSuperTransition(WallSlideState, IdleState, TouchingWallToIdle());
        StateMachine.AddSuperTransition(WallSlideState, InAirState, TouchingWallToInAir());
        StateMachine.AddSuperTransition(WallSlideState, LedgeClimbState, TouchingWallToLedgeClimb());
        StateMachine.AddSuperTransition(WallSlideState, WallJumpState, TouchingWallToWallJump());
        #endregion        
    
        # region Ledge Climb Complete
        Func<bool> LedgeClimbToIdle() => () => LedgeClimbState.isAnimationFinished && !LedgeClimbState.isTouchingCeiling;
        Func<bool> LedgeClimbToCrouchIdle() => () => LedgeClimbState.isAnimationFinished && LedgeClimbState.isTouchingCeiling;
        Func<bool> LedgeClimbToWallJump() => () => !LedgeClimbState.isAnimationFinished && InputHandler.JumpInput && !LedgeClimbState.isClimbing;
        Func<bool> LedgeClimbToInAir() => () => !LedgeClimbState.isAnimationFinished && InputHandler.NormInputY == -1 && LedgeClimbState.isHanging && !LedgeClimbState.isClimbing;

        At(LedgeClimbState, IdleState, LedgeClimbToIdle());
        At(LedgeClimbState, CrouchIdleState, LedgeClimbToCrouchIdle());
        At(LedgeClimbState, WallJumpState, LedgeClimbToWallJump());
        At(LedgeClimbState, InAirState, LedgeClimbToInAir());
        #endregion
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        DashDirectionIndicator = transform.Find("Dash Direction Indicator");
        StateMachine.ChangeState(IdleState);
    }

    private void Update() {
        Core.LogicUpdate();
        StateMachine.Tick();
    }

    private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void SetColliderHeight(float height) {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }
}
