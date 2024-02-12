using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectile : MonoBehaviour
    {
        public event Action OnInIt;
        public Rigidbody2D RigidBody2D { get; private set; }

        private void Awake() {
            RigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            InIt();
        }

        public void InIt() {
            OnInIt?.Invoke();
        }
    }
}
