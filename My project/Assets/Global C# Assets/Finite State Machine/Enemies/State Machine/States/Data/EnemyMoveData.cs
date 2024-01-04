using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move State Data", menuName = "Data/State Data/Move State")]
public class EnemyMoveData : ScriptableObject
{
    public float MovementVelocity = 3f;
}
