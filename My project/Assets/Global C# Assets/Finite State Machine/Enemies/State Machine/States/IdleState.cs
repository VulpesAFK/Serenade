using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.Enemy
{
    public class IdleState : EnemyState
    {
        protected EnemyIdleData stateData;
        protected bool flipAfterIdle;
        protected bool isIdleTimeOver;
        protected float idleTime;
        protected bool isPlayerInMinAggroRange;

        private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;


        public IdleState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyIdleData stateData) : base(entity, stateMachine, animBoolName) {
            this.stateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        }


        public override void Enter()
        {
            base.Enter();
            Movement?.SetVelocityX(0f); 

            isIdleTimeOver = false;
            SetRandomIdleTime();

        }

        public override void Exit()
        {
            base.Exit();

            if (flipAfterIdle)
            {
                Movement?.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Movement?.SetVelocityX(0f);

            if (Time.time >= startTime + idleTime)
            {
                isIdleTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void SetFlipAfterIdle(bool flip)
        {
            flipAfterIdle = flip;
        }

        private void SetRandomIdleTime()
        {
            idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);
        }
    }
}
