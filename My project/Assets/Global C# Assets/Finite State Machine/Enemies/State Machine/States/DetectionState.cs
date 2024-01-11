 using System.Collections.Generic;
using UnityEngine;

public class DetectionState : EnemyState
{
    protected EnemyDetectionData stateData;
    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange;
    protected bool performLongRangedAction;
    public DetectionState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDetectionData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangedAction = false;
        entity.SetVelocity(0f);

    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.LongRangeActionTime)
        {
            performLongRangedAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
