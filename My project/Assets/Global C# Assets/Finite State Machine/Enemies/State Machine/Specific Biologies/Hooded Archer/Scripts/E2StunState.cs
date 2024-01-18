using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2StunState : StunState
{
    private E2 enemy;

    public E2StunState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyStunData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
}
