using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2DetectionState : DetectionState
{
    private E2 enemy;
    public E2DetectionState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDetectionData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            if (Time.time >= enemy.E2DodgeState.startTime + enemy.enemyDodgeData.DodgeTime)
            {
                stateMachine.ChangeState(enemy.E2DodgeState);
            }
              
            else
            {
            stateMachine.ChangeState(enemy.E2MeleeState);
            }
        }

        else if (isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.E2LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
