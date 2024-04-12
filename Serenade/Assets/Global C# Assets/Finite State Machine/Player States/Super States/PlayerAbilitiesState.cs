using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates
{
    public class PlayerAbilitiesState : PlayerState
    {
        protected bool isTouchingWall;

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        protected Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
        private Collision collision;

        public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }

        public override void DoChecks() {
            base.DoChecks();
            if (Collision) {
                isTouchingWall = Collision.WallFront;
            }
        }

        public override void Enter() {
            base.Enter();

            isAbilityDone = false;
        }
    }
}
