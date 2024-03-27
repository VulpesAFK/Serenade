using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : EnemyState
{
    protected EnemyMoveData stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAggroRange;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;


    public MoveState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyMoveData stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (Collision)
        {
            isDetectingLedge = Collision.LedgeVertical;
            isDetectingWall = Collision.WallFront;
        }
        
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter() {
        base.Enter();
        Movement?.SetVelocityX(stateData.MovementVelocity * Movement.FacingDirection);

    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.MovementVelocity * Movement.FacingDirection);

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }
}
