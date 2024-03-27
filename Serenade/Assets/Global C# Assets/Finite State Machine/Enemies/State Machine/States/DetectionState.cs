 using System.Collections.Generic;
using UnityEngine;

public class DetectionState : EnemyState
{
    protected EnemyDetectionData stateData;
    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange;
    protected bool performLongRangedAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;


    public DetectionState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDetectionData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        if (Collision)
        {
            isDetectingLedge = Collision.LedgeVertical;
        }
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangedAction = false;
        Movement?.SetVelocityX(0f);

    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    
        Movement?.SetVelocityX(0f);

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
