using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class E2LookForPlayerState : LookForPlayerState
{
    private E2 enemy;
    public E2LookForPlayerState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyLookForPlayerData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.E2DetectionState);
        }

        else if (isAllTurnsDone)
        {
            stateMachine.ChangeState(enemy.E2MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}