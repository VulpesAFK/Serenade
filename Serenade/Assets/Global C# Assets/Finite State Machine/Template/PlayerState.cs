using UnityEditor;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.Construct
{
    public class PlayerState
    {
        protected Core core;

        public bool isAbilityDone { get; protected set; }

        protected Player player;
        protected PlayerStateMachine stateMachine;
        protected PlayerData playerData;
        protected float startTime;
        protected PlayerStateData playerStateData;

        public bool isAnimationFinished { get; protected set; }
        public bool isExitingState { get; protected set; }

        public string animBoolName { get; private set;}

        public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.animBoolName = animBoolName;
            this.playerStateData = playerStateData;

            core = player.Core;
        }

        public virtual void Enter()  {
            DoChecks();
            player.Anim.SetBool(animBoolName, true);

            startTime = Time.time;

            isAnimationFinished = false;
            isExitingState = false;
        }

        public virtual void Exit()  {
            player.Anim.SetBool(animBoolName, false);
            isExitingState = true;
        }

        // A holder for the update function
        public virtual void LogicUpdate() {}

        // A holder for the fixed update function
        public virtual void PhysicsUpdate() => DoChecks();

        public virtual void DoChecks() {}

        public virtual void AnimationTrigger() {}

        public virtual void AnimationFinishTrigger() { isAnimationFinished = true; }
    }
}
