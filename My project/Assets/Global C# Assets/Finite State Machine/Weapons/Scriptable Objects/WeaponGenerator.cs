using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoxTail {
    public class WeaponGenerator : MonoBehaviour {

         // TODO - Switch this declaration to an awake function just to allow for better automation
        // Reference to the main weapon that will be manipulated with the correct component scripts
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponData data;

        // List holding important information on regards to the componenets active
        private List<WeaponComponent> componenetsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componenetDependencies = new List<Type>();

        // Reference to the animator for the specific weapon
        private Animator anim;

        private void Start() {
            anim = GetComponentInChildren<Animator>();
            GenerateWeapon(data);
        }
        public void GenerateWeapon(WeaponData data) {
            // Fetch the current selected weapon
            weapon.SetData(data);

            // Clear the list of any stray data left in the list
            componenetsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componenetDependencies.Clear();

            // Fetch all the componenets that are on the weapon now selected
            componenetsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            componenetDependencies = data.GetAllDependencies();

            // Loop through all to check whether all dependencies are active and present 
            foreach (var dependencies in componenetDependencies) {
                // Check if a dependency is already present from the previous to the now 
                if (componentsAddedToWeapon.FirstOrDefault(Component => Component.GetType() == dependencies)) continue;

                // Store the present dependency
                var WeaponComponent = componenetsAlreadyOnWeapon.FirstOrDefault(Component => Component.GetType() == dependencies);

                // IF there is not this dependcy present then add
                if (WeaponComponent == null) WeaponComponent = gameObject.AddComponent(dependencies) as WeaponComponent;

                WeaponComponent.InIt();

                // Add to list to make sure not added twice
                componentsAddedToWeapon.Add(WeaponComponent);
            }

            // Look at all of the items compared to those added to see what needs to be removed
            var componenetsToRemove = componenetsAlreadyOnWeapon.Except(componentsAddedToWeapon);
            // Loop through those components to destroy 
            foreach (var weaponComponent in componenetsToRemove) Destroy(weaponComponent);

            anim.runtimeAnimatorController = data.AnimationController;
        }
    }
}
