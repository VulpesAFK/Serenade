using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace FoxTail
{
    // Initalize that this editor is used only on an type of data using the defined type of
    [CustomEditor(typeof(WeaponData))]
    public class WeaponDataEditor : Editor {

        // Used as a means of defining a universal type of data in the C# built-in system
        // Can store the information of other classes and not just the object that contain them 
        private static List<Type> dataComponentType = new List<Type>();

        // This Unity function that holds the basic standard value of the editor 
        // Manipulate to show the objects we want
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var dataComponent in dataComponentType) {
                if (GUILayout.Button(dataComponent.Name)) {

                } 
            }
        }

        // Usage of C# reflections to automatically add all componenets to be available on the weaponry component
        [DidReloadScripts]
        private static void OnRecompile() {
            // Search all the app domain and assemblies and retrieve 
            // Stores a list of all assemblies that are loaded in the current app doamin
            // Assembly => A unit of code that is compiled and ran in the .NET runtime thus the types of class and functions and interfaces and ect..
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Loops through the list and create a list that stores all assemblies that met these conditions
            // Looks at all the assemblies in assemblies and stores
            var types = assemblies.SelectMany(assemblies => assemblies.GetTypes());

            // Look for all types that inherit component data
            // Adds to var of types where the the type is a subclass/child of the component data
            // Additional filter to prevent the same component data generic class from being allowed in
            var filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);

            // Add finally to the final static main list
            dataComponentType = filteredTypes.ToList();
        }
    }
}
