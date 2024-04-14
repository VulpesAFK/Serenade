using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using FoxTail.Serenade.Experimental.ScreenplaySystem.Template;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates {
    public class PlayerInteractState : PlayerAbilitiesState {
        /*
            * Interact input 
            * Fetch the inter-class function 
        */
        private Screenplay screenplay;
        public PlayerInteractState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData) { }


        public override void Enter()
        {
            base.Enter();

            Collider2D interactive = Physics2D.OverlapCircle(Collision.InteractCheck.position, playerData.InteractiveCheckRadius, playerData.WhatIsInteractive);
            screenplay = interactive.GetComponent<Screenplay>();
            screenplay.ResetIsScreenplayFinished();

            Movement?.SetVelocityZero();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (player.InputHandler.InteractInput) {
                player.InputHandler.UseInteractInput();
                screenplay.TEST();
            }

            isAbilityDone = screenplay.IsScreenplayFinished;
        }
    }
}
