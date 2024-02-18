using UnityEngine;

namespace FoxTail {
    /*
        * Graphics component is responsible for changing the sprite on the sprite renderer on the graphics child object
        * Based on what is based on from its weapon
    */
    public class WeaponProjectileGraphics : WeaponProjectileComponent {
        private Sprite sprite;

        private SpriteRenderer spriteRenderer;

        protected override void InIt()
        {
            base.InIt();

            spriteRenderer.sprite = sprite;
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not SpriteDataPackage spriteDataPackage)
                return;

            sprite = spriteDataPackage.Sprite;
        }

        protected override void Awake()
        {
            base.Awake();

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}