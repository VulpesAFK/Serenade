using System.Collections;
using System.Collections.Generic;
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
    public PlayerInventory Inventory { get; private set; }

    // v Player data for fixed properties
    [SerializeField] private PlayerData playerData;

    // v Reference to a vector2 to hold a custom vector to be assigned to the rigidbody2d
    private Vector2 workSpace;
    
    // r Variables for all states
    # region Variable created to hold the state instantiations

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }


    # endregion



    private void Awake() 
    {
        // Fetching the core components from child scripts
        Core = GetComponentInChildren<Core>();

        // t Instantiation of the player sttae machine
        StateMachine = new PlayerStateMachine();

        // r State instantiation of all states tied to the player
        # region Instantiation of all states to the specific variable

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState  = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");

        # endregion
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

        Inventory = GetComponent<PlayerInventory>();

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);


        // t Set the default animation to the default idle state
        StateMachine.Initialize(IdleState);
    }

    // r Callback for all of the unity functions to run
    # region Unity functions for all callback functions

    private void Update() 
    {
        Core.LogicUpdate();
        // t Update all logic tied to the logic update in the current running state
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() 
    {
        // t Run the physics update tied to the current state with the  fixed update
        StateMachine.CurrentState.PhysicsUpdate();
    }

    # endregion



    // r Functions that connect the animation with a timer condition
    # region Animation triggers if there are events needed to be played after or mid

    // f Function to hold the trigger for all animation to be trigger mid
    private void AnimationTrigger() 
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    // f Function to gold and trigger the end of the animation
    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    # endregion

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }
}
