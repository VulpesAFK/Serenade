using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates {
    public class PlayerGroundedState : PlayerState {
        public int xInput { get; protected set; }

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        protected Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
        private Collision collision;

        public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }

        public override void Enter() {
            base.Enter();
            
            playerStateData.ResetAmountOfJumps(playerData.AmountOfJumps);

            // player.DashState.ResetCanDash();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            xInput = player.InputHandler.NormInputX;

            // if (player.InputHandler.AttackInput[(int)CombatInputs.primary] && !isTouchingCeiling)
            // {
            //     stateMachine.ChangeState(player.PrimaryAttackState);
            // }

            // else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary] && !isTouchingCeiling)
            // {
            //     stateMachine.ChangeState(player.SecondaryAttackState);
            // }

            // else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling) { stateMachine.ChangeState(player.DashState); }
        }

        public override void Exit() {
            base.Exit();

            playerStateData.coyoteTime = true;
        }

    }
}