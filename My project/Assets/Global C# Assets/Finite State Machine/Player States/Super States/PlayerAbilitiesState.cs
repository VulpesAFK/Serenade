using UnityEngine;
public class PlayerAbilitiesState : PlayerState
{
    protected bool isAbilityDone;

    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }

    private Movement movement;
    private Collision collision;

    private bool isGrounded;

    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

    public override void DoChecks() {
        base.DoChecks();

        if (Collision) {
            isGrounded = Collision.Ground;
        }
    }

    public override void Enter() {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // Conditions
        bool FinishedAbilityOnGround = isAbilityDone && isGrounded && Movement?.CurrentVelocity.y < 0.01f;
        bool FinishedAbilityInAir = isAbilityDone && !(isGrounded && Movement?.CurrentVelocity.y < 0.01f);

        if (FinishedAbilityOnGround) stateMachine.ChangeState(player.IdleState);
        else if (FinishedAbilityInAir) stateMachine.ChangeState(player.InAirState);
    }
}
