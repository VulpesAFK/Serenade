using FoxTail.Unlinked;
using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectileDelayedGravity : WeaponProjectileComponent
    {
        [field: SerializeField] public float Gravity { get; private set; } = 4f;
        [field: SerializeField] public float Distance { get; private set; } = 10f;

        private DistanceNotifier distanceNotifier = new DistanceNotifier();

        private void HandleNotify() {
            rB.gravityScale = Gravity;
        }

        protected override void InIt()
        {
            base.InIt();

            distanceNotifier.Init(transform.position, Distance);
        }

        protected override void Awake()
        {
            base.Awake();

            distanceNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();

            rB.gravityScale = 0;
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