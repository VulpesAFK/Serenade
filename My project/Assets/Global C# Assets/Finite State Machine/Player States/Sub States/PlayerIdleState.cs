using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    /*
        * Inherits the ground state template
        * Set movement to zero

        * Idle -> Move
        * Idle -> CrouchMove
    */

    public class PlayerIdleState : PlayerGroundedState {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter() {
            base.Enter();

            Movement?.SetVelocityZero();
        }
    }
}
