using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2RangeState : RangeAttackState
{
    private E2 enemy;
    public E2RangeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, Transform attackPosition, EnemyRangeAttackData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinish)
        {
            if(isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.E2DetectionState);
            }
            else 
            {
                stateMachine.ChangeState(enemy.E2LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}