namespace FoxTail {
    public class DrawToProjectileData : ComponentData
    {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponProjectileDrawToProjectile);
        }
    }
}