using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerInteractState : PlayerAbilitiesState {
        public PlayerInteractState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }
        /*
            ? Method 1
                ! State transition require the nedd to be able trandition from the grounded state to the interact state (isGrounded && object-interactive is true)
                * Freeze the player via set velocity 0
                * Fetch the type of object it is (for now its sign post || NPCs)
                * Set the object boolean event trigger towards the isAbilityFinished
                ! Switch states back to the idle state

            ? Method 1 EXT. 1
                ! State transition require the nedd to be able trandition from the grounded state to the interact state (isGrounded && object-interactive is true)
				! Freeze the player via set velocity 0 -> Enter()
				! Fetch the type of object it is
				! Set the object boolean event trigger towards the isAbilityFinished
				! Switch states back to the idle state
        */
        public override void Enter() {
            base.Enter();

			Movement?.SetVelocityZero();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();
        }
    }
}
