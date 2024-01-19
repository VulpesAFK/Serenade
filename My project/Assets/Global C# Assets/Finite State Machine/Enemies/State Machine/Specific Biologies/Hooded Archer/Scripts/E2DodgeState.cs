using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2DodgeState : DodgeState
{
    private E2 enemy;
    public E2DodgeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDodgeData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isDodgeOver)
        {
            enemy.SetVelocity(0f);

            if(isPlayerInMaxAggroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.E2MeleeState);
            }

            else if (isPlayerInMaxAggroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.E2RangeState);
            }

            else if(!isPlayerInMaxAggroRange)
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
