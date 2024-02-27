using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    /*
        * For instantiated projectiles
        * Gets a list of all targets from the Targeter weapon component and passes that to the projectile in the targetDataPackage
    */

    public class TargeterToProjectile : WeaponComponent
    {
        private WeaponProjectileSpawner projectileSpawner;
        private Targeter targeter;

        private readonly TargetsDataPackage targetsDataPackage = new TargetsDataPackage();

        private void HandleSpawnProjectile(WeaponProjectile projectile)
        {
            targetsDataPackage.targets = targeter.GetTargets();

            projectile.SendDataPackage(targetsDataPackage);
        }

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<WeaponProjectileSpawner>();
            target = GetComponent<Targeter>();

            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
        }
    }
}
