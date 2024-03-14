using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates
{
    public class PlayerAbilitiesState : PlayerState
    {

        // Check the surrounding made specifically the made
        private bool isGrounded;
        protected bool isTouchingWall;

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        protected Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
        private Collision collision;

        public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void DoChecks() {
            base.DoChecks();

            if (Collision) {
                isGrounded = Collision.Ground;
                isTouchingWall = Collision.WallFront;
            }
        }

        public override void Enter() {
            base.Enter();

            isAbilityDone = false;
        }
    }
}
