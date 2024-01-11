using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player State")]
public class EnemyLookForPlayerData : ScriptableObject
{
    public int AmountOfTurns = 2;
    public float TimeBetweenTurns = 0.75f;
}
