using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerInteractState : PlayerAbilitiesState {
        public PlayerInteractState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }
        /*
            * Method 1
                 * State transition require the nedd to be able trandition from the grounded state to the interact state (isGrounded && object-interactive is true)
                 * Fetch the type of object it is (for now its sign post and NPCs)
                 * 
        */
    }
}
