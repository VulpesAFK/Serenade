using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyState
{
    protected EnemyDeadData stateData;
    public DeadState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyDeadData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.DeathBloodParticles, entity.transform.position, stateData.DeathBloodParticles.transform.rotation);
        GameObject.Instantiate(stateData.DeathChunkParticles, entity.transform.position, stateData.DeathChunkParticles.transform.rotation);
        
        entity.gameObject.SetActive(false);
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
