using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    protected string animBoolName;

    public EnemyState(Entity entity, EnemyStateMachine stateMachine, string animBoolName) {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }


}
