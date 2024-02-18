using System;
using UnityEngine;

namespace FoxTail {

    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        // * This is an array as each attack can spawn multiple projectiles
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo {
        [field: SerializeField] public Vector2 Offset { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public WeaponProjectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public DamageDataPackage DamageData { get; private set; }
        [field: SerializeField] public KnockBackDataPackage KnockBackData { get; private set; }
        [field: SerializeField] public PoiseDamageDataPackage PoiseDamageData { get; private set; }
        [field: SerializeField] public SpriteDataPackage SpriteDataPackage { get; private set; }
    }
    
}