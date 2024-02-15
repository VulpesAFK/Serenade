using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    /*
        * Interface and connection point between projectile components and any entity that spawns that projectile
    */
    public class WeaponProjectile : MonoBehaviour
    {
        // Used to notify all projectile componenets that InIt has been called
        public event Action OnInIt;
        public event Action<ProjectileDataPackage> OnReceiveDataPackage;
        public Rigidbody2D Rigidbody2D { get; private set; }

        public void InIt()
        {
            OnInIt?.Invoke();
        }

        public void SendDataPackage(ProjectileDataPackage dataPackage)
        {
            OnReceiveDataPackage?.Invoke(dataPackage);
        }
        
        private void Awake() 
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }


    }
}