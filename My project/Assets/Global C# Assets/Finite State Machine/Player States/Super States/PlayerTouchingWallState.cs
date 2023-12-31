public class PlayerTouchingWallState : PlayerState
{
    // Condition to check all local surrounding
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    // Variables to store all input from the main input handler script
    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
        
    } 

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Collision.Ground;
        isTouchingWall = core.Collision.WallFront;
        isTouchingLedge = core.Collision.Ledge;

        if (isTouchingWall && !isTouchingLedge) { player.LedgeClimbState.SetDetectedPosition(player.transform.position); }
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // Store all the necessary inputs from the input handler script
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        // Conditions
        bool isGrabbingWall = !isGrounded || grabInput;
        bool isAvoidingWall = xInput != core.Movement.FacingDirection && !grabInput;
        bool isInAir = !isTouchingWall || isAvoidingWall;
        bool canLedgeClimbing = isTouchingWall && !isTouchingLedge;

        if (jumpInput) {
            // Check the direction the player is facing via what wall is being touched
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }

        else if (!isGrabbingWall) { stateMachine.ChangeState(player.IdleState); }

        else if (isInAir) { stateMachine.ChangeState(player.InAirState); }

        else if (canLedgeClimbing) { stateMachine.ChangeState(player.LedgeClimbState); }
    }
}
