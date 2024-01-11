using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Data/Entity Data/Base Data")]
public class EnemyChargeData : ScriptableObject
{
    public float ChargeSpeed  = 8f;
    public float ChargeTime = 3;
}
