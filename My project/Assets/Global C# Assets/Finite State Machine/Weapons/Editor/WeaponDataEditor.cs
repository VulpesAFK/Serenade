using System;
using System.Collections;
using System.Collections.Generic;
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
        private List<Type> dataComponentType = new List<Type>();

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
        }
    }
}
