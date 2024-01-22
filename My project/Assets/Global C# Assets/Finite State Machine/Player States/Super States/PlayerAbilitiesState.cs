public class PlayerAbilitiesState : PlayerState
{
    // Bools to help to save the abilties inheriting to be saved
    protected bool isAbilityDone;

    // Check the surrounding made specifically the made
    private bool isGrounded;

    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;

    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

    public override void DoChecks() {
        base.DoChecks();

        if (Collision)
        {
            isGrounded = Collision.Ground;
        }
    }

    public override void Enter() {
        base.Enter();

        // Return control to state when any ability inheriting is finish
        isAbilityDone = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // Conditions
        bool FinishedAbilityOnGround = isAbilityDone && isGrounded && Movement?.CurrentVelocity.y < 0.01f;
        bool FinishedAbilityInAir = isAbilityDone && !(isGrounded && Movement?.CurrentVelocity.y < 0.01f);

        if (FinishedAbilityOnGround) stateMachine.ChangeState(player.IdleState);
        else if (FinishedAbilityInAir) stateMachine.ChangeState(player.InAirState);

        // if (isAbilityDone)
        // {
        //     if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) { stateMachine.ChangeState(player.IdleState); }

        //     else { stateMachine.ChangeState(player.InAirState); }
        // }
    }
}
