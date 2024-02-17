using System.Collections;
using System.Collections.Generic;
using FoxTail.Unlinked;
using UnityEngine;

namespace FoxTail
{
    // Provides the ability to disable gravity for a projectile till a distance in which gravity reactivates
    // Requires some external material scripts
    public class WeaponProjectileDelayedGravity : WeaponProjectileComponent
    {
        [field: SerializeField] public float Distance { get; private set; } = 10f;

        private DistanceNotifier distanceNotifier = new DistanceNotifier();
        private float gravity;

        private void HandleNotify() {
            rb.gravityScale = gravity;
        }

        protected override void InIt()
        {
            base.InIt();

            rb.gravityScale = 0f;
            distanceNotifier.InIt(transform.position, Distance);
        }
        protected override void Awake()
        {
            base.Awake();

            gravity = rb.gravityScale;
            distanceNotifier.OnNotify += HandleNotify;
        }

        protected override void Update()
        {
            base.Update();

            distanceNotifier?.Tick(transform.position);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            distanceNotifier.OnNotify -= HandleNotify;
        }
    }
}
