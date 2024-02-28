using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates
{
    public class PlayerAbilitiesState : PlayerState
    {
        // Bools to help to save the abilties inheriting to be saved
        protected bool isAbilityDone;

        // Check the surrounding made specifically the made
        private bool isGrounded;
        private bool isTouchingWall;

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
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

        public override void LogicUpdate() {
            base.LogicUpdate();

            // Conditions
            bool FinishedAbilityOnGround = isAbilityDone && isGrounded && Movement?.CurrentVelocity.y < 0.01f;
            bool FinishedAbilityInAir = isAbilityDone && !(isGrounded && Movement?.CurrentVelocity.y < 0.01f);

            if (FinishedAbilityOnGround) stateMachine.ChangeState(player.IdleState);
            // else if (FinishedAbilityInAir) stateMachine.ChangeState(player.InAirState);
        }
    }
}
