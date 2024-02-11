using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    [Serializable]
    public abstract class ComponentData
    {
        // Name of the total component
        [SerializeField, HideInInspector] private string componentName;

        // Setting name function
        public void SetCompomentName() => componentName = GetType().Name;

        // Variable to check for each to see what component script matches the data
        public Type ComponentDependeny { get; protected set; }

        // Set the names function on initialization
        public ComponentData() {
            SetCompomentName();
            SetCompomentDependencies();
        }

        // Inheritance function just to allow for a connetion to the weapon editor and generic child
        public virtual void SetAttackDataNames() { }

        public virtual void InitializeAttackData(int numberOfAttack) { }

        // Made to model the rough same functions as a interface to force a required override
        protected abstract void SetCompomentDependencies();
    }

    [Serializable]
    // Same reason for having to have the same class name
    // Usage in the weapon component will requrie a generic specification in which will render this generics too messy 
    // Inherit a none class and then state the generics 
    public abstract class ComponentData<TYPE_ONE> : ComponentData where TYPE_ONE : AttackData {
        // Allows for any properties to require it to be called
        [SerializeField] private TYPE_ONE[] attackData;

        // General array of weaponry data that will be used to store all component data of a weapon
        // Get the seperate array to prevent the case of recieving the functional field instead
        public TYPE_ONE[] AttackData { get => attackData; private set => attackData = value; }

        // Loop through each of the attack state datas are change their names to the correct via the function
        public override void SetAttackDataNames() {
            base.SetAttackDataNames();
            // This attack data passed through the generic set
            for (int i = 0; i < AttackData.Length; i++) {
                AttackData[i].SetAttackName(i + 1);
            }
        }

        // Function to make sure the length of the array for each componenent in the data is the same as the number of attacks
        // Remove some attack data if there is too much
        // Add and resize the array if there is too little 
        # region InitializeAttackData(int numberOfAttack) Function
        public override void InitializeAttackData(int numberOfAttack) {
            base.InitializeAttackData(numberOfAttack);
            
            // Storing the current length of the componennt length 
            var oldLength = attackData != null? attackData.Length : 0;
            
            // Return nothing if the length is already the same
            if (oldLength == numberOfAttack) return;

            // Attack data is an auto property with the use of get and set thus it is really a function
            // Perfectly okay if the lenth of the array is too big though not if it is too small
            Array.Resize(ref attackData, numberOfAttack);

            if (oldLength < numberOfAttack) {
                for (int i = oldLength; i < AttackData.Length; i++) {
                    // Create a new type of attack data component as factor type as a attack data and not instance
                    var newObject = Activator.CreateInstance(typeof(TYPE_ONE)) as TYPE_ONE;
                    attackData[i] = newObject;
                }
            }

            SetAttackDataNames();
        }
        # endregion
    }
}
