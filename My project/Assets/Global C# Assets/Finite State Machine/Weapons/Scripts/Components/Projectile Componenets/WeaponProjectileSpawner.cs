using UnityEngine;

namespace FoxTail {
    public class WeaponProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        private Vector2 spawnPos;
        private Vector2 spawnDir;

        private Movement movement;

        private readonly ObjectPools objectPools = new ObjectPools();

        private void HandleAttackAction()
        {
            foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
            {
                spawnPos = transform.position;
                spawnPos.Set(spawnPos.x + projectileSpawnInfo.Offset.x * movement.FacingDirection, spawnPos.y + projectileSpawnInfo.Offset.y);

                spawnDir.Set(projectileSpawnInfo.Direction.x * movement.FacingDirection, projectileSpawnInfo.Direction.y);

                var currentProjectile = objectPools.GetObject(projectileSpawnInfo.ProjectilePrefab);

                currentProjectile.transform.position = spawnPos;

                var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
                currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                currentProjectile.Reset();

                currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);
                currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackData);

                currentProjectile.InIt();
            }
        }

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<Movement>();

            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmos() {
            if (data == null || !Application.isPlaying)
                return;

            foreach (var item in data.AttackData)
            {
                foreach (var point in item.SpawnInfos)
                {
                    var pos = transform.position + (Vector3)point.Offset;

                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(pos, pos + (Vector3)point.Direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }
    }
}