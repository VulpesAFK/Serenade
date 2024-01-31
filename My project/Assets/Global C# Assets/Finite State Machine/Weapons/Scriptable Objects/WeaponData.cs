using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapo Data")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }
    }
}
