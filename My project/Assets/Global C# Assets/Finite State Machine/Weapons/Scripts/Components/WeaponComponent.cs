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
    public abstract class WeaponComponent<TYPE> : WeaponComponent where TYPE : ComponentData {
        // Save the specific data type as data
        protected TYPE data;

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<TYPE>();
        }

    }
}
