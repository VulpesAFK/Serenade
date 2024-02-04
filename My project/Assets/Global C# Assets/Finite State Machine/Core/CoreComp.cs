using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    // A script designed to reduce the need to having to continue rewriing the old method of core access and null checks
    public class CoreComp<TYPE_ONE> where TYPE_ONE : CoreComponent {
        // Private reference to the main refernec that will be checked
        private Core core;
        // Variable to store the private component that wiil be checked whether it exist and if it is not null
        private TYPE_ONE component;

        // Fetch and store the conponent with the spefic type assuming that that type is not null and exist
        public TYPE_ONE Component => component ??= core.GetCoreComponent<TYPE_ONE>();

        // Constructor function to set the core notion
        // Has to be to correct core reference for that specific entity
        public CoreComp(Core core) {
            if (core == null) Debug.LogWarning($"Core is Null for component {typeof(TYPE_ONE)}");
            this.core = core;
        }   
    }
}
