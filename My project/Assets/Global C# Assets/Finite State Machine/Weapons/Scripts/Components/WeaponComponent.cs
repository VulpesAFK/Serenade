using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    // Cannot be referenced via attachment & must be via inheritants
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected virtual void Awake() {
            weapon = GetComponent<Weapon>();
        }
    }
}
