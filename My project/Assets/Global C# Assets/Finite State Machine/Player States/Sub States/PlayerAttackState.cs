using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilitiesState
{
    // Main weapon script link
    private Weapon weapon;

    private int inputIndex;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, Weapon weapon, CombatInputs input ) : base(player, stateMachine, playerData, animBoolName) {
        // Base set weapon to the initialized
        this.weapon = weapon;

        inputIndex = (int)input;

        //NOTE - REMOVE weapon.OnExit += ExitHandler;
        /* weapon.OnExit += ExitHandler; */

        weapon.OnExit += HandleExit;
        weapon.OnUseInput += HandleUseInput;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        weapon.CurrentInput = player.InputHandler.AttackInput[inputIndex];
    }

    public override void Enter() {
        base.Enter();
        // Start weaponry logic equal to when the state enters
        weapon.Enter();
    }

    private void HandleUseInput() {

        player.InputHandler.UseAttackInput(inputIndex);
    }

    private void HandleExit()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
