using UnityEngine;

public class EnemyState
{
    protected Core core;
    protected EnemyStateMachine stateMachine;
    protected Entity entity;
    public float startTime { get; protected set;}
    protected string animBoolName;

    public EnemyState(Entity entity, EnemyStateMachine stateMachine, string animBoolName) {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    public virtual void Enter() {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit() {
        entity.Anim.SetBool(animBoolName, false); 
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { 
        DoChecks();
    }

    public virtual void DoChecks() { }


}
