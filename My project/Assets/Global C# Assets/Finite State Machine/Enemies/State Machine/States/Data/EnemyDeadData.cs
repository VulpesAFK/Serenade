using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class EnemyDeadData : ScriptableObject
{
    public GameObject DeathChunkParticles;
    public GameObject DeathBloodParticles;
}
