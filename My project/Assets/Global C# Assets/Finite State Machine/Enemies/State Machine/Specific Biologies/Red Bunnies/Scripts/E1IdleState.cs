using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1IdleState : IdleState
{
    private E1 enemy;
    public E1IdleState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyIdleData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData) {
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
        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.E1DetectionState);
        }

        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.E1MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
