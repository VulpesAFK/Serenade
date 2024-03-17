using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates
{
    public class PlayerGroundedState : PlayerState
    {
        public int xInput { get; protected set; }

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;

        public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter() {
            base.Enter();
            //TODO - MOVE ELSEWHERE
            player.JumpState?.ResetAmountOfJumpsLeft();
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

    }
}
