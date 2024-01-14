using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1DeadState : DeadState
{
    private E1 enemy;
    public E1DeadState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDeadData stateData, E1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
