using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    // Main controller to signal to all over tied weapons when to start and a connection between entities spawning it
    public class WeaponProjectile : MonoBehaviour
    {
        // Used to notify all projectile componenets that InIt has been called
        public event Action OnInIt;

        public Rigidbody2D Rigidbody2D { get; private set; }

        public void InIt()
        {
            OnInIt?.Invoke();
        }

        
        
        private void Awake() 
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InIt();
        }


    }
}