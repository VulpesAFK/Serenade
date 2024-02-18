using UnityEngine;

namespace FoxTail {
    public class WeaponOptionalSprite : WeaponComponent<OptionalSpriteData, AttackOptionalSprite>
    {
        private SpriteRenderer spriteRenderer;

        private void HandleSetOptionalSpriteActive(bool value)
        {
            spriteRenderer.enabled = value;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            if (!currentAttackData.UseOptionalSprite)
                return;

            spriteRenderer.sprite = currentAttackData.Sprite;
        }

        protected override void Awake()
        {
            base.Awake();

            spriteRenderer = GetComponentInChildren<OptionalSpriteMarker>().SpriteRenderer;
            spriteRenderer.enabled = false;
        }

        protected override void Start()
        {
            base.Start();

            eventHandler.OnSetOptionalSpriteActive += HandleSetOptionalSpriteActive;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
        }
    }
}