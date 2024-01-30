using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;
        private int currentWeaponSpriteIndex;
        [SerializeField] private WeaponSprites[] weaponSprites;

        protected override void HandleEnter() {
            base.HandleEnter();

            currentWeaponSpriteIndex = 0;
        }

        private void HandleBaseSpriteChange(SpriteRenderer SR) {
            if (!isAttackActive) {
                weaponSpriteRenderer.sprite = null;
                return;
            }
            var currentAttackSprites = weaponSprites[weapon.CurrentAttackCounter].Sprites;
            if (currentWeaponSpriteIndex >= currentAttackSprites.Length) {
                Debug.LogWarning($"{weapon.name} weapon sprite length mismatch");
                return;
            }

            weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
            currentWeaponSpriteIndex++;
        }
        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = transform.Find("Weapon Sprite").GetComponent<SpriteRenderer>();

            // TODO: Fix this when we create weapon data
            // baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable() {
            base.OnEnable();
            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable() {
            base.OnDisable();
            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.OnEnter -= HandleEnter;
        }
    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}