using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E1StunState : StunState
{
    private E1 enemy;
    public E1StunState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyStunData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.E1MeleeAttackState);
            }
            else if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.E1ChargeState);
            }
            else 
            {
                enemy.E1LookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.E1LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
