using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1MeleeAttackState : MeleeAttackState
{
    private E1 enemy;
    public E1MeleeAttackState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, Transform attackPosition, EnemyMeleeAttackData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        if (isAnimationFinish)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
