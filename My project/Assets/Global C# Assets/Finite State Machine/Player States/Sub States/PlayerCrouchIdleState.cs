using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            Movement?.SetVelocityZero();

            player.SetColliderHeight(playerData.CrouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();

            player.SetColliderHeight(playerData.StandColliderHeight);
        }
    }
}
