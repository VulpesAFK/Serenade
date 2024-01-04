using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1MoveState : MoveState
{
    private E1 enemy;

    public E1MoveState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyMoveData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData) {
        this.enemy = enemy;
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

        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.E1IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.E1IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
