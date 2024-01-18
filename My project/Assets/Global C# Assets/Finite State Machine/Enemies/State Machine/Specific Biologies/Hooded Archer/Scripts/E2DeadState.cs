using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2DeadState : DeadState
{
    private E2 enemy;
    public E2DeadState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDeadData stateData, E2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
