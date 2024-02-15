using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail {
    /*
        * A tester development weapon system we need to develop and test
        * Allows for it to be more easier to test to make sure everything is working 
    */
    public class ProjectileTester : MonoBehaviour {
        public WeaponProjectile Projectile;
        public DamageDataPackage DamageDataPackage;

        private void Start() {
            if (!Projectile) {
                Debug.LogWarning("!! No Projectile To Test");
                return;
            }

            Projectile.SendDataPackage(DamageDataPackage);
            Projectile.InIt();
        }
    }
}
