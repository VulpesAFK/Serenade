 using System.Collections.Generic;
using UnityEngine;

public class DetectionState : EnemyState
{
    protected EnemyDetectionData stateData;
    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange;
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
        entity.SetVelocity(0f);

    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
