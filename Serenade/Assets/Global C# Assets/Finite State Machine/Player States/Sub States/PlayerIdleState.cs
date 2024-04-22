using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    public class PlayerIdleState : PlayerGroundedState {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }
        public override void Enter() {
            base.Enter();

            Movement?.SetVelocityZero();
        }
    }
}
