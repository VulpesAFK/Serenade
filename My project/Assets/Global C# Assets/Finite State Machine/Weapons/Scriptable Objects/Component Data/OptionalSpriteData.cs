namespace FoxTail {
    public class OptionalSpriteData : ComponentData<AttackOptionalSprite>
    {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponOptionalSprite);
        }
    }
}