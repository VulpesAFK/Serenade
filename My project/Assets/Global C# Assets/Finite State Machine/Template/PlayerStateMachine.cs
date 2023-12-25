using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    // v A variable for global access to see the current set
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startState)
    {
        // t Set the start state to the current
        CurrentState = startState;
        // t Start all the logic to the current running state
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        // t Exit and remove the current running state
        CurrentState.Exit();
        // t Assign the new state to global variable
        CurrentState = newState;
        // t Start all the logic to the now new start
        CurrentState.Enter();
    }
}
