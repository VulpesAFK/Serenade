using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    // v Reference for all player methods
    protected Player player;
    // v Reference to the state machine to play animations
    protected PlayerStateMachine stateMachine;
    // v Reference to the player data for state information
    protected PlayerData playerData;
    // v Reference to the start of the animation in reference to the start of the game
    protected float startTime;

    // v Reference to whether the state has finished when the cycle has finished
    protected bool isAnimationFinished;
    // v Reference to when the state has ended when switched
    protected bool isExitingState;

    // v Name of the bool condition
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        // t Assign all variables to the constructor and store to the protected and private variables
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() 
    {
        // t Holds all checks at the start of the frame state
        DoChecks();
        // t Set the animation of this bool to be true
        player.Anim.SetBool(animBoolName, true);

        // t Set the start of the time to the start of the state
        startTime = Time.time;

        // t Set the start of the animation bool to be false
        isAnimationFinished = false;
        // t Set the exit bool to be false at hte start of the animation
        isExitingState = false;

        // t DEBUG
        Debug.Log(animBoolName);
    }

    public virtual void Exit() 
    {
        // t Set the animation playing to be false and state that we are exiting the running state
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    // f A holder for the update function
    public virtual void LogicUpdate()
    {

    }

    // f A holder for the fixed update function
    public virtual void PhysicsUpdate()
    {
        // t Continue checking surroundings in the physics fixed update
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    // f Used to trigger something mid animation
    public virtual void AnimationTrigger()
    {

    }

    // f Used to trigger the end of the animation
    public virtual void AnimationFinishTrigger()
    {
        // t Animation has finished
        isAnimationFinished = true;
    }
}
