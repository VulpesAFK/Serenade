namespace FoxTail {
    public class WeaponProjectileDrawModifyDelayedGravity : WeaponProjectileComponent {
        private WeaponProjectileDelayedGravity delayedGravity;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DrawModifierDataPackage drawModifierDataPackage)
                return;

            delayedGravity.DistanceMultiplier = drawModifierDataPackage.DrawPercentage;
        }

        protected override void Awake()
        {
            base.Awake();

            delayedGravity = GetComponent<WeaponProjectileDelayedGravity>();
        }
    }
}