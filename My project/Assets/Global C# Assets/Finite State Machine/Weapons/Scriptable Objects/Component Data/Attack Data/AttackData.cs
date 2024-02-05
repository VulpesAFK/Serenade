using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class AttackData {
        // Name of the indiviual attack data name
        [SerializeField, HideInInspector] private string attackName;

        // Function to automatically set the name of the attack data name
        public void SetAttackName(int i) => attackName = $"List<{GetType().Name}> Value {i}";
    }   
}
