using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : EnemyState
{
    protected EnemyDodgeData stateData;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAggroRange;

    protected bool isGrounded;
    protected bool isDodgeOver;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;

    public DodgeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDodgeData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        if (Collision)
        {
            isGrounded = Collision.Ground;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;

        Movement?.SetVelocity(stateData.DodgeVelocity, stateData.DodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.DodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
