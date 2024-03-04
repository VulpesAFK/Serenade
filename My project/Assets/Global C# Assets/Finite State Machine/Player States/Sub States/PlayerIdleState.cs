using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    public class PlayerIdleState : PlayerGroundedState {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter() {
            base.Enter();

            Movement?.SetVelocityZero();
        }
    }
}
