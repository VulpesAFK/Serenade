namespace FoxTail {
    public class WeaponProjectileDrawToProjectile : WeaponComponent
    {
        private WeaponProjectileDraw draw;
        private WeaponProjectileSpawner projectileSpawner;
        private readonly DrawModifierDataPackage drawModifierDataPackage = new DrawModifierDataPackage();
        
        private void HandleEvaluateCurve(float value)
        {
            drawModifierDataPackage.DrawPercentage = value;
        }

        private void HandleSpawnProjectile(WeaponProjectile projectile)
        {
            projectile.SendDataPackage(drawModifierDataPackage);
        }

        protected override void HandleEnter()
        {
            // base.HandleEnter();

            drawModifierDataPackage.DrawPercentage = 0f;
        }

        protected override void Start()
        {
            base.Start();

            draw = GetComponent<WeaponProjectileDraw>();
            projectileSpawner = GetComponent<WeaponProjectileSpawner>();

            draw.OnEvaluateCurve += HandleEvaluateCurve;
            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }
    }
}