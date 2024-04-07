using System;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerStateData StateData { get; private set; }
    [SerializeField] private PlayerData playerData;
    public Core Core { get; private set; }
    private Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= Core.GetCoreComponent<Collision>(); }
    private Collision collision;
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
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
    public PlayerInteractState InteractState { get; private set; }
    #endregion

    //TODO - REIMPLEMENT THESE SECTIONS OF THE STATES LATER AFTER LOOKING AT THE MECHANICS OF THE NEW ATTACKING STATE
    // public PlayerDashState DashState { get; private set; }
    // public PlayerAttackState PrimaryAttackState { get; private set; }
    // public PlayerAttackState SecondaryAttackState { get; private set; }

    private void Awake() {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();
        StateData = new PlayerStateData();


        #region Instantiation 
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle", StateData);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move", StateData);
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle", StateData);
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove", StateData);
        JumpState  = new PlayerJumpState(this, StateMachine, playerData, "inAir", StateData);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir", StateData);
        LandState = new PlayerLandState(this, StateMachine, playerData, "land", StateData);
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide", StateData);
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab", StateData);
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb", StateData);
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir", StateData);
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState", StateData);
        InteractState = new PlayerInteractState(this, StateMachine, playerData, "interact", StateData);
        #endregion

        //TODO - INSTANTIATE THE NEW STATES AFTER ALL IS FIXED WITH THE LAST TODO ITEM
        // DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        // PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        // SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");

        //REVIEW - FIX THIS HAYWIRE OF TRANSITIONS AND FUNCTION LAMBDA CONDITIONS
        //TODO - WATCH BARDENT VIDEO ON JUMP TO VERIFY THE FUNCTIONS ON HOW IT WORKS AT THE MAIN NECESSARY FUNCTION ON THE PRIME FUNCTIONS TRANSITION: IDLE/MOVE -> JUMP -> INAIR -> LAND -> MOVE/IDLE, COYOTE JUMPING METHOD AND !! INPUT REMEMBERANCE

        void At(string stateType, PlayerState from, PlayerState to, Func<bool> condition) => StateMachine.AddTransition((stateType == "super")? true : false, from, to, condition);

        #region Idle Complete Full
        Func<bool> IdleToMove() => () => !IdleState.isExitingState && InputHandler.NormInputX != 0;
        Func<bool> IdleToCrouchIdle() => () => !IdleState.isExitingState && InputHandler.NormInputY == -1;

        At("sub", IdleState, MoveState, IdleToMove());
        At("sub", IdleState, CrouchIdleState, IdleToCrouchIdle());
        #endregion

        #region Move Complete Full
        Func<bool> MoveToIdle() => () => !MoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> MoveToCrouchMove() => () => !MoveState.isExitingState && InputHandler.NormInputY == -1;

        At("sub", MoveState, IdleState, MoveToIdle());
        At("sub", MoveState, CrouchMoveState, MoveToCrouchMove());
        #endregion
        
        #region CrouchIdle Complete Sub-Full
        Func<bool> CrouchIdleToIdle() => () => !CrouchIdleState.isExitingState && !Collision.Ceiling && InputHandler.NormInputY != -1;
        Func<bool> CrouchIdleToCrouchMove() => () => !CrouchIdleState.isExitingState && InputHandler.NormInputX != 0;

        At("sub", CrouchIdleState, IdleState, CrouchIdleToIdle());
        At("sub", CrouchIdleState, CrouchMoveState, CrouchIdleToCrouchMove());
        #endregion

        #region CrouchMove Complete Sub-Full
        Func<bool> CrouchMoveToCrouchIdle() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputX == 0;
        Func<bool> CrouchMoveToMove() => () => !CrouchMoveState.isExitingState && InputHandler.NormInputY != -1 && !Collision.Ceiling;

        At("sub", CrouchMoveState, CrouchIdleState, CrouchMoveToCrouchIdle());
        At("sub", CrouchMoveState, MoveState, CrouchMoveToMove());
        #endregion

        #region Grounded
        Func<bool> GroundedStateToJump() => () => InputHandler.JumpInput && !Collision.Ceiling && JumpState.CanJump; 
        Func<bool> GroundedStateToInAir() => () => !Collision.Ground && !Collision.Ceiling; 
        Func<bool> GroundedStateToWallGrab() => () => Collision.WallFront && InputHandler.GrabInput && Collision.LedgeHorizontal;
        Func<bool> GroundedStateToInteract() => () => true;

        At("super", IdleState, JumpState, GroundedStateToJump());  
        At("super", IdleState, InAirState, GroundedStateToInAir());  
        At("super", IdleState, WallGrabState, GroundedStateToWallGrab());  
        At("super", IdleState, InteractState, GroundedStateToInteract());  
        #endregion

        #region Abilities Complete
        Func<bool> AbiltiesStateToInAir() => () => StateMachine.CurrentState.isAbilityDone && !(Collision.Ground && Movement?.CurrentVelocity.y < 0.01f);
        Func<bool> AbiltiesStateToIdle() => () => StateMachine.CurrentState.isAbilityDone && Collision.Ground && Movement?.CurrentVelocity.y < 0.01f;

        At("super", JumpState, InAirState, AbiltiesStateToInAir());  
        At("super", JumpState, IdleState, AbiltiesStateToIdle());  
        #endregion

        #region InAir 
        Func<bool> InAirToLand() => () => Collision.Ground && Movement?.CurrentVelocity.y < 0.01f;
        Func<bool> InAirToWallSlide() => () => Collision.WallFront && InputHandler.NormInputX == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0.01f;
        Func<bool> InAirToWallGrab() => () => Collision.WallFront && InputHandler.GrabInput && Collision.LedgeHorizontal;
        Func<bool> InAirToLedgeClimb() => () => Collision.WallFront && !Collision.LedgeHorizontal && !Collision.Ground;
        Func<bool> InAirToJump() => () => InputHandler.JumpInput && JumpState.CanJump;
        //FIXME - REMOVED CONDITION NEEDS TO BE RE_FITTED
        Func<bool> InAirToWallJump() => () => InputHandler.JumpInput && (Collision.WallFront || Collision.WallBack);

        At("sub", InAirState, LandState, InAirToLand());
        At("sub", InAirState, WallSlideState, InAirToWallSlide());
        At("sub", InAirState, WallJumpState, InAirToWallJump());
        At("sub", InAirState, JumpState, InAirToJump());
        At("sub", InAirState, LedgeClimbState, InAirToLedgeClimb());
        At("sub", InAirState, WallGrabState, InAirToWallGrab());
        #endregion

        #region Land Complete
        Func<bool> LandToIdle() => () => !LandState.isExitingState && InputHandler.NormInputX == 0 && LandState.isAnimationFinished;
        Func<bool> LandToMove() => () => !LandState.isExitingState && InputHandler.NormInputX != 0;

        At("sub", LandState, IdleState, LandToIdle());
        At("sub", LandState, MoveState, LandToMove());
        #endregion

        #region WallGrab Complete
        Func<bool> WallGrabToWallClimb() => () => !WallGrabState.isExitingState && InputHandler.NormInputY > 0.0f;
        Func<bool> WallGrabToWallSlide() => () => !WallGrabState.isExitingState && (InputHandler.NormInputY < 0.0f || !InputHandler.GrabInput);

        At("sub", WallGrabState, WallClimbState, WallGrabToWallClimb());
        At("sub", WallGrabState, WallSlideState, WallGrabToWallSlide());
        #endregion

        #region WallSlide Complete
        Func<bool> WallSlideToWallGrab() => () => !WallSlideState.isExitingState && InputHandler.GrabInput && InputHandler.NormInputY == 0;

        At("sub", WallSlideState, WallGrabState, WallSlideToWallGrab());
        #endregion

        #region WallClimb Complete
        Func<bool> WallClimbToWallGrab() => () => !WallClimbState.isExitingState && InputHandler.NormInputY != 1;

        At("sub", WallClimbState, WallGrabState, WallClimbToWallGrab());
        #endregion

        #region TouchingWall Complete
        Func<bool> TouchingWallStateToIdle() => () => Collision.Ground && !InputHandler.GrabInput;
        Func<bool> TouchingWallStateToInAir() => () => !Collision.WallFront || (InputHandler.NormInputX != Movement?.FacingDirection && !InputHandler.GrabInput);
        Func<bool> TouchingWallStateToLedgeClimb() => () => Collision.WallFront && !Collision.LedgeHorizontal;
        Func<bool> TouchingWallStateToWallJump() => () => InputHandler.JumpInput;

        At("super", WallSlideState, IdleState, TouchingWallStateToIdle());
        At("super", WallSlideState, InAirState, TouchingWallStateToInAir());
        At("super", WallSlideState, LedgeClimbState, TouchingWallStateToLedgeClimb());
        At("super", WallSlideState, WallJumpState, TouchingWallStateToWallJump());
        #endregion        
    
        # region Ledge Climb Complete
        Func<bool> LedgeClimbToIdle() => () => LedgeClimbState.isAnimationFinished && !LedgeClimbState.isTouchingCeiling;
        Func<bool> LedgeClimbToCrouchIdle() => () => LedgeClimbState.isAnimationFinished && LedgeClimbState.isTouchingCeiling;
        Func<bool> LedgeClimbToWallJump() => () => !LedgeClimbState.isAnimationFinished && InputHandler.JumpInput && !LedgeClimbState.isClimbing;
        Func<bool> LedgeClimbToInAir() => () => !LedgeClimbState.isAnimationFinished && InputHandler.NormInputY == -1 && LedgeClimbState.isHanging && !LedgeClimbState.isClimbing;

        At("sub", LedgeClimbState, IdleState, LedgeClimbToIdle());
        At("sub", LedgeClimbState, CrouchIdleState, LedgeClimbToCrouchIdle());
        At("sub", LedgeClimbState, WallJumpState, LedgeClimbToWallJump());
        At("sub", LedgeClimbState, InAirState, LedgeClimbToInAir());
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
