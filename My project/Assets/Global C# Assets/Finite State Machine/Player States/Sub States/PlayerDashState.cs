using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilitiesState
{
    public bool CanDash { get; private set; }

    private bool isHolding;
    private bool dashInputStop;
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;

    private Vector2 lastAIPosition;

    private float lastDashTime;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;

        Time.timeScale = playerData.HoldTimeScale;
        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.DashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (isHolding)
            {
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;

                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f); 

                if (dashInputStop || Time.unscaledTime >= startTime + playerData.MaxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1;
                    startTime = Time.time;

                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.Drag;

                    player.SetVelocity(playerData.DashVelocity, dashDirection);

                    player.DashDirectionIndicator.gameObject.SetActive(false);

                    PlaceAfterImage();
                }
            }

            else
            {
                player.SetVelocity(playerData.DashVelocity, dashDirection);
                CheckIfShouldPaceAfterImage();

                if (Time.time >= startTime + playerData.DashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }


    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.DashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPosition = player.transform.position;
    }

    private void CheckIfShouldPaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPosition) >= playerData.DistanceBetweenAfterImage)
        {
            PlaceAfterImage();
        }
    }
}
