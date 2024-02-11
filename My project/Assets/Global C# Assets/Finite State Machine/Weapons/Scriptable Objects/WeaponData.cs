using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoxTail
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public RuntimeAnimatorController AnimationController { get; private set; }
        [field: SerializeField] public int NumberOfAttacks { get; private set; }
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public TYPE GetData<TYPE>() {
            return ComponentData.OfType<TYPE>().FirstOrDefault();
        }

        // Add passed in component into the list of component data to be used by the weapons
        // Check to prevent the occurance of duplilcation 
        public void AddDataToInspector(ComponentData data) {
            // Looking at all the data in the component data and check if the types of the component is the some and the data passes
            // T => shows each value type in the component data
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) {
                Debug.LogError($"{data} has already been added to this weaponry system");
                return;
            }

            ComponentData.Add(data);
        }

        // Fetch all the dependencies set by each componenet
        public List<Type> GetAllDependencies() {
            return ComponentData.Select(component => component.ComponentDependeny).ToList();
        }
    }
}
