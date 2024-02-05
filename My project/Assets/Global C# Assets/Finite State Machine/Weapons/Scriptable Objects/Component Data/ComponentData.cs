using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    [Serializable]
    public class ComponentData
    {
        // Name of the total component
        [SerializeField] private string componentName;

        // Setting name function
        public void SetCompomentName() => componentName = GetType().Name;
    }

    [Serializable]
    // Same reason for having to have the same class name
    // Usage in the weapon component will requrie a generic specification in which will render this generics too messy 
    // Inherit a none class and then state the generics 
    public class ComponentData<TYPE_ONE> : ComponentData where TYPE_ONE : AttackData {
        // General array of weaponry data that will be used to store all component data of a weapon
        [field: SerializeField] public TYPE_ONE[] AttackData { get; private set; }
    }
}
