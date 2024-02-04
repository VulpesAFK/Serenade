using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilitiesState
{
    // Main weapon script link
    private Weapon weapon;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, Weapon weapon) : base(player, stateMachine, playerData, animBoolName) {
        // Base set weapon to the initialized
        this.weapon = weapon;

        weapon.OnExit += ExitHandler;
    }

    public override void Enter() {
        base.Enter();
        // Start weaponry logic equal to when the state enters
        weapon.Enter();
    }

    private void ExitHandler() {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
