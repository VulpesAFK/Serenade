using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // v All references needed to components and external scripts for input and animation
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    // v Transforms need from the player for surrounding checks
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    // v Player data for fixed properties
    [SerializeField] private PlayerData playerData;

    // v Reference to a vector2 to hold a custom vector to be assigned to the rigidbody2d
    private Vector2 workSpace;
    // v Reference to the current resultant forces acting onto the player rigidbody2d
    public Vector2 CurrentVelocity { get; private set; }
    // v Reference to the current direction  the player is facing
    public int FacingDirection { get; private set; }
    
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

        // t Default direction to the player facing 
        FacingDirection = 1;

        // t Set the default animation to the default idle state
        StateMachine.Initialize(IdleState);
    }

    // r Callback for all of the unity functions to run
    # region Unity functions for all callback functions

    private void Update() 
    {
        // t Set the current velocity to the current tied rigidbody2d
        CurrentVelocity = RB.velocity;
        // t Update all logic tied to the logic update in the current running state
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() 
    {
        // t Run the physics update tied to the current state with the  fixed update
        StateMachine.CurrentState.PhysicsUpdate();
    }

    # endregion



    // r Functiosn for all notion to player movement 
    # region Velocity functions to alter the player position

    // f Function to help aid any state to force movement via the x axis
    public void SetVelocityX(float velocity)
    {
        // t Set the workspace velocity with an arguement and the current velocity y
        workSpace.Set(velocity, CurrentVelocity.y);
        // t Set the rigidbody2d with the set vector2 
        RB.velocity = workSpace;
        // t Set the control vector2 with the forced vector2 
        CurrentVelocity = workSpace;
    }

    // f Function to help aid any state to force movement via the y axis
    public void SetVelocityY(float velocity)
    {
        // t Set the workspace velocity with san arugement and the current velcoity x
        workSpace.Set(CurrentVelocity.x, velocity);
        // t Set the rigidbody2d with the set vector2
        RB.velocity = workSpace;
        // t Set the control vector2 with the forced vector2
        CurrentVelocity = workSpace;
    }

    // f Function to force the movement of the player with a force under a specific direction
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        // t Set the angle vector2 arguement to be in a fixed magitude of one
        angle.Normalize();
        // t Set the vector2 force to the with the of the angle and velocity under a specific x direction
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        // t Set the rigidbody with the new work vector2
        RB.velocity = workSpace;
        // t Change the current vector2 force to match with the added
        CurrentVelocity = workSpace;
    }

    // f Freeze the player two its current position flat
    public void SetVelocityZero()
    {
        // t Freeze the rigidbody2d at its place
        RB.velocity = Vector2.zero;
        // t Update the current velocity to these changes
        CurrentVelocity = Vector2.zero;
    }

    // f Another function that will support the velocity force with only a vector2 and float
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    # endregion



    // r Functions to help flip the player
    # region Checking to flip and then flipping

    // f Checks if there should be a sprite flip
    public void CheckIfShouldFlip(int xInput)
    {
        // t Checks if the input pressed is different to that of the variable holding the old direction
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    // f Flips the player via rotation or can be scale
    private void Flip()
    {
        // t Alters the facing direction to match the current
        FacingDirection *= -1;
        // t Rotation via the y axis
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    # endregion



    // r Functions that aid with checking all the surrounding objects
    # region Checking the player surroundings and returning 

    // f Check the surroundings of the ground
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
    }

    // f Check the surroundings of the walls in front of us
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistance, playerData.WhatIsGround);
    }

    // f Check the surroundings of the walls behind of us
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.WallCheckDistance, playerData.WhatIsGround);
    }

    // f Designed to check whether there is a ledge or not
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistance, playerData.WhatIsGround);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
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


    public Vector2 DetermineCornerPosition()
    {
        // t Find the location of the wall in front of the player
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistance, playerData.WhatIsGround);
        // t Determine the x distance from the player to the wall
        float xDistance = xHit.distance;

        // t Set a workspace to make a variable to signla the amount to add on to the position
        workSpace.Set((xDistance + 0.015f) * FacingDirection, 0f);
        // t Raycast to note the position of the grounf that the player will appear to 
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f,  playerData.WhatIsGround);
        // t Determine the y distance
        float yDistance = yHit.distance;

        // t Formulate the final vector2 to be returned
        workSpace.Set(wallCheck.position.x + (xDistance * FacingDirection), ledgeCheck.position.y - yDistance);

        // t return the final result
        return workSpace;

    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }
}
