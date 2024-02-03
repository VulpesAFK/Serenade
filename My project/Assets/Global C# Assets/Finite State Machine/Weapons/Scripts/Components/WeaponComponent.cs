using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    // Cannot be referenced via attachment & must be via inheritants
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        //TODO - Fix later
        //protected WeaponAnimationEventHandler EventHandler => weapon.EventHandler;
        protected WeaponAnimationEventHandler eventHandler;
        protected Core Core => weapon.Core;
        protected bool isAttackActive;

        protected virtual void Awake() {
            weapon = GetComponent<Weapon>();
            eventHandler = GetComponentInChildren<WeaponAnimationEventHandler>();
        }

        protected virtual void HandleEnter() {
            isAttackActive = true;
        }

        protected virtual void HandleExit() {
            isAttackActive = false;
        }

        protected virtual void OnEnable() {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable() {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    // Abstract class that allows for data to be automatically inputted
    // Requires a seperate class due weapon data connected with component data and not this 
    // If this weapon component inherits the data then it is now allow with a correct parent 

    // Same reason with the usage of the attack data scripts inheritants 

    // TYPE_ONE => Stores all arrays of the single conponents of the attack
    // TYPE_TWO => Stores the data for a single animation attack
    public abstract class WeaponComponent<TYPE_ONE, TYPE_TWO> : WeaponComponent where TYPE_ONE : ComponentData<TYPE_TWO> where TYPE_TWO : AttackData {
        // Save the array of TYPE_TWO as an array for the whole total weaponry combonation
        protected TYPE_ONE data;
        // Save as the variable that will hold the properties for the individual animations
        protected TYPE_TWO currentAttackData;

        protected override void Awake() {
            base.Awake();
            // Assign the data to the variable with the generic data type assigned
            data = weapon.Data.GetData<TYPE_ONE>();
        }

        protected override void HandleEnter() {
            base.HandleEnter();
            // The specific data for the single weapon combo to be the whole component of the array with the specific attack being played
            currentAttackData = data.AttackData[weapon.CurrentAttackCounter]; 
        }

    }
}
