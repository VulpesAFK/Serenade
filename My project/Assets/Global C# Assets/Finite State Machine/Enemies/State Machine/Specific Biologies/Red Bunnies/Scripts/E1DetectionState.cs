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
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.E1MeleeAttackState);
        }

        else if (performLongRangedAction)
        {
            stateMachine.ChangeState(enemy.E1ChargeState);
        }
        
        else if (!isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.E1LookForPlayerState);
        }

        else if(!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.E1MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
