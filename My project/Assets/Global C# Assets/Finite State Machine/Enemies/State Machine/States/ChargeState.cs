using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : EnemyState
{
    protected EnemyChargeData stateData;
    protected bool isPlayerInMinAggroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;

    public ChargeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyChargeData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();

        if (Collision)
        {
            isDetectingLedge = Collision.LedgeVertical;
            isDetectingWall = Collision.WallFront;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;

        Movement?.SetVelocityX(stateData.ChargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.ChargeSpeed * Movement.FacingDirection);


        if (Time.time >= startTime + stateData.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
