using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(float amount)
        {
            print($"{gameObject.name} Damage: {amount}");
        }
    }
}
