using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agressive Weapon Data", menuName = "Data/Weapon Data/ Agressive Weapon")]
public class AgressiveWeaponData : WeaponData
{
    [SerializeField] private WeaponStruct[] weaponDetails;
    public WeaponStruct[] WeaponDetails { get => weaponDetails; private set => weaponDetails = value; } 

    private void OnEnable() {
        AmountOfAttacks = weaponDetails.Length;

        MovementSpeed = new float [AmountOfAttacks];

        for (int i = 0; i < AmountOfAttacks; i++)
        {
            MovementSpeed[i] = weaponDetails[i].MovementSpeed;
        }
    }
}
