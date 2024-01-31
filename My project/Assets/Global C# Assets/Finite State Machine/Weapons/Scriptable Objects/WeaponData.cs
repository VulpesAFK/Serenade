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
        [field: SerializeField] public int NumberOfAttacks { get; private set; }
        [field: SerializeReference] public List<ComponentData> componentData { get; private set; }

        public TYPE GetData<TYPE>() {
            return componentData.OfType<TYPE>().FirstOrDefault();
        }

        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => componentData.Add(new WeaponSpriteData());
    }
}
