namespace FoxTail {
    public class DrawData : ComponentData<AttackDraw>
    {
        protected override void SetCompomentDependencies()
        {
            ComponentDependeny = typeof(WeaponProjectileDraw);
        }
    }
}