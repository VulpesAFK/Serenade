using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1DetectionState : DetectionState
{
    private E1 enemy;
    public E1DetectionState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDetectionData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
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
        if (!isPlayerInMaxAggroRange)
        {
            enemy.E1IdleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.E1IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
