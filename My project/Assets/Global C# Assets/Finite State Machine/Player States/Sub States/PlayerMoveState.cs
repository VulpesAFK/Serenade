using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    public class PlayerMoveState : PlayerGroundedState {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            Movement?.CheckIfShouldFlip(xInput);
            Movement?.SetVelocityX(playerData.MovementVelocity * xInput);
            
        }
    }
}
