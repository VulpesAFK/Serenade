using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1ChargeState : ChargeState
{
    private E1 enemy;

    public E1ChargeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyChargeData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            stateMachine.ChangeState(enemy.E1MeleeAttackState);
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.E1LookForPlayerState);
        }

        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.E1DetectionState);
            }
            else
            {
                stateMachine.ChangeState(enemy.E1LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
