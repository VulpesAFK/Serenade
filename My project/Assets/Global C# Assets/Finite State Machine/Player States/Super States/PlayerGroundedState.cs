using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;

public class PlayerGroundedState : PlayerState
{
    // Variables to store inputs from the player input handler
    protected int xInput;
    protected int yInput;
    private bool grabInput;
    private bool jumpInput;
    private bool dashInput;

    // Surrounding check variables to store the player surroundings
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    protected bool isTouchingCeiling;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {

    }

    public override void DoChecks() {
        base.DoChecks();

        if (Collision)
        {
            isGrounded = Collision.Ground;
            isTouchingWall = Collision.WallFront;
            isTouchingLedge = Collision.LedgeHorizontal;
            isTouchingCeiling = Collision.Ceiling;
        }
    }

    public override void Enter() {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // Checking the inputs from the player input handler
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        // Conditions
        bool isAbleToJumpFromGround = jumpInput && player.JumpState.CanJump() && !isTouchingCeiling;
        bool isAbleToGrabWallFromGround = isTouchingWall && grabInput && isTouchingLedge;
        bool isAbleToDashFromGround = dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling;

        // TODO: Fix this dumb stuff
        // if (player.InputHandler.AttackInput[(int)CombatInputs.primary] && !isTouchingCeiling)
        // {
        //     stateMachine.ChangeState(player.PrimaryAttackState);
        // }

        // else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary] && !isTouchingCeiling)
        // {
        //     stateMachine.ChangeState(player.SecondaryAttackState);
        // }


        // else if (isAbleToJumpFromGround) { stateMachine.ChangeState(player.JumpState); }

        // else if (!isGrounded) {
        //     // Coyote time for a more lenient jump
        //     player.InAirState.StartCoyoteTime();
        //     stateMachine.ChangeState(player.InAirState);
        // }

        // else if (isAbleToGrabWallFromGround) {
        //     // Prevent jumping after wall grabbing
        //     player.JumpState.DecreaseAmountOfJumpsLeft();
        //     stateMachine.ChangeState(player.WallGrabState);
        // }

        // else if (isAbleToDashFromGround) { stateMachine.ChangeState(player.DashState); }
        
    }

}
