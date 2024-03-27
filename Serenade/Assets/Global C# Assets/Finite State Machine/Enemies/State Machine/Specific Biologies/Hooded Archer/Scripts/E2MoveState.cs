using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2MoveState : MoveState
{
    private E2 enemy;
    public E2MoveState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyMoveData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy; 
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.E2DetectionState);
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            enemy.E2IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.E2IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
