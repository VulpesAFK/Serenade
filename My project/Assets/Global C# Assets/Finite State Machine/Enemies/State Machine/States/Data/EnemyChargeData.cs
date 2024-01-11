using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class EnemyChargeData : ScriptableObject
{
    public float ChargeSpeed  = 8f;
    public float ChargeTime = 1;
}
