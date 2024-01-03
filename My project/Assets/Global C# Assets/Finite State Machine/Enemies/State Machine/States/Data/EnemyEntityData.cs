using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityData : ScriptableObject
{
    public float WallCheckDistance;
    public float LedgeCheckDistance;
    public LayerMask whatIsGround;
}
