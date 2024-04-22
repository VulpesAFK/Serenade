using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
    # region A list of all datas

    [Header("Move State")]
    public float MovementVelocity = 10;

    [Header("Jump State")]
    public float JumpVelocity = 15;
    public int AmountOfJumps = 2;

    [Header("In Air State")]
    public float CoyoteTime = 0.1f;
    public float VariableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float WallSlideVelocity = 1.5f;

    [Header("Wall Climb State")]
    public float WallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 20;
    public float WallJumpTime = 0.4f;
    public Vector2 WallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;

    [Header("Dash State")]
    public float DashCooldown = 0.5f;
    public float MaxHoldTime = 1f;
    public float HoldTimeScale = 0.25f;
    public float DashTime = 0.2f;
    public float DashVelocity = 30f;
    public float Drag = 10f;
    public float DashEndYMultiplier = 0.2f;
    public float DistanceBetweenAfterImage = 0.5f;

    [Header("Crouch Idle State")]
    public float CrouchColliderHeight = 0.8f;
    public float StandColliderHeight = 1.6f;

    [Header("Crouch Move State")]
    public float CrouchMovementVelocity = 5f;
    

    # endregion



}
