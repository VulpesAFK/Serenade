using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    // Reference to the core components
    protected Core core;

    // Reference for all player methods
    protected Player player;
    // Reference to the state machine to play animations
    protected PlayerStateMachine stateMachine;
    // Reference to the player data for state information
    protected PlayerData playerData;
    // Reference to the start of the animation in reference to the start of the game
    protected float startTime;

    // Reference to whether the state has finished when the cycle has finished
    protected bool isAnimationFinished;
    // Reference to when the state has ended when switched
    protected bool isExitingState;

    // Name of the bool condition
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        // Assign all variables to the constructor and store to the protected and private variables
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

        core = player.Core;
    }

    public virtual void Enter() 
    {
        // Holds all checks at the start of the frame state
        DoChecks();
        // Set the animation of this bool to be true
        player.Anim.SetBool(animBoolName, true);

        // Set the start of the time to the start of the state
        startTime = Time.time;

        // Set the start of the animation bool to be false
        isAnimationFinished = false;
        // Set the exit bool to be false at hte start of the animation
        isExitingState = false;

        // DEBUG
        Debug.Log(animBoolName);
    }

    public virtual void Exit() 
    {
        // Set the animation playing to be false and state that we are exiting the running state
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    // A holder for the update function
    public virtual void LogicUpdate()
    {

    }

    // A holder for the fixed update function
    public virtual void PhysicsUpdate()
    {
        // Continue checking surroundings in the physics fixed update
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    // Used to trigger something mid animation
    public virtual void AnimationTrigger()
    {

    }

    // Used to trigger the end of the animation
    public virtual void AnimationFinishTrigger()
    {
        // Animation has finished
        isAnimationFinished = true;
    }
}
