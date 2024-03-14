using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates
{
    public class PlayerGroundedState : PlayerState
    {
        public int xInput { get; protected set; }

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;
        private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
        private Collision collision;
        private bool isGrounded;

        public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void DoChecks() {
            base.DoChecks();

            if (Collision) isGrounded = Collision.Ground;
        }

        public override void Enter() {
            base.Enter();

            player.JumpState.ResetAmountOfJumpsLeft();
            // player.DashState.ResetCanDash();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            xInput = player.InputHandler.NormInputX;

            // Conditions
            // bool isAbleToJumpFromGround = jumpInput && player.JumpState.CanJump() && !isTouchingCeiling;
            // bool isAbleToGrabWallFromGround = isTouchingWall && grabInput && isTouchingLedge;
            // bool isAbleToDashFromGround = dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling;

            // TODO: Fix this dumb stuff
            // if (player.InputHandler.AttackInput[(int)CombatInputs.primary] && !isTouchingCeiling)
            // {
            //     stateMachine.ChangeState(player.PrimaryAttackState);
            // }

            // else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary] && !isTouchingCeiling)
            // {
            //     stateMachine.ChangeState(player.SecondaryAttackState);
            // }


            // else if (isAbleToJumpFromGround) { stateMachine.ChangeState(player.JumpState); }

            if (!isGrounded) player.InAirState.StartCoyoteTime();
            // if (!isGrounded) {
            //     player.InAirState.StartCoyoteTime();
            //     stateMachine.ChangeState(player.InAirState);
            // }

            // else if (isAbleToGrabWallFromGround) {
            //     // Prevent jumping after wall grabbing
            //     player.JumpState.DecreaseAmountOfJumpsLeft();
            //     stateMachine.ChangeState(player.WallGrabState);
            // }

            // else if (isAbleToDashFromGround) { stateMachine.ChangeState(player.DashState); }
            
        }

    }
}
