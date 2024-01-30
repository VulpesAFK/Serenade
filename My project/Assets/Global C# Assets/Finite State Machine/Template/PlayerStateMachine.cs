using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.Construct
{
    public class PlayerStateMachine {
        // A variable for global access to see the current set
        public PlayerState CurrentState { get; private set; }
        
        // TODO: Add the transitions table for the global
        // TODO: Add the transitions table for the local state that will be tied locally 

        public void Initialize(PlayerState startState) {
            // Set the start state to the current
            CurrentState = startState;
            // Start all the logic to the current running state
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState) {
            // Exit and remove the current running state
            CurrentState.Exit();
            // Assign the new state to global variable
            CurrentState = newState;
            // Start all the logic to the now new start
            CurrentState.Enter();
        }
    }
}
