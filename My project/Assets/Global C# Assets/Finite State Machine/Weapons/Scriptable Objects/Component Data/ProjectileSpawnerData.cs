namespace FoxTail {
    public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner> {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponProjectileSpawner);
        }
    }
}