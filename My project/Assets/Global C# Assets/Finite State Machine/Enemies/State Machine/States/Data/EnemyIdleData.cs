using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Idle State Data", menuName = "Data/State Data/Idle State")]
public class EnemyIdleData : ScriptableObject
{
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 2f;

}
