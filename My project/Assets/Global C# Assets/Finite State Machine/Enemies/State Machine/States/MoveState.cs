using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : EnemyState
{
    protected EnemyMoveData stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public MoveState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyMoveData stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void Enter() {
        base.Enter();
        entity.SetVelocity(stateData.MovementVelocity);

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
}