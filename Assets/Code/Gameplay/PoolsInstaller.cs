using Assets.Code.Gameplay.Units.Enemies;
using Assets.Code.Gameplay.Units.Shield;
using UnityEngine;

namespace Game.Gameplay
{
    public class PoolsInstaller : Zenject.MonoInstaller
    {
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private ProjectileHitFx projectileHitPrefab = null;
        [SerializeField] private Enemy enemyPrefab = null;
        [SerializeField] private SpecialEnemy specialEnemyPrefab = null;
        [SerializeField] private MiniBoss miniBossPrefab = null;
        [SerializeField] private EnemyDeathFx enemyDeathFxPrefab = null;
        [SerializeField] private ShieldBrick shieldBrickPrefab = null;

        private static readonly int projectilesPoolSize = 200;
        private static readonly int projectilesHitPoolSize = projectilesPoolSize;
        private static readonly int enemyPoolSize = 40;
        private static readonly int specialEnemyPoolSize = 2;
        private static readonly int enemyDeathFxPoolSize = enemyPoolSize;
        private static readonly int minibossPoolSize = 4;
        private static readonly int shieldBrickPoolSize = 50;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
               .WithInitialSize(projectilesPoolSize)
               .FromComponentInNewPrefab(projectilePrefab)
               .UnderTransformGroup("Projectiles");

            Container.BindMemoryPool<ProjectileHitFx, ProjectileHitFxPool>()
               .WithInitialSize(projectilesHitPoolSize)
               .FromComponentInNewPrefab(projectileHitPrefab)
               .UnderTransformGroup("ProjectilesHit");

            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithInitialSize(enemyPoolSize)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.BindMemoryPool<SpecialEnemy, SpecialEnemyPool>()
                .WithInitialSize(specialEnemyPoolSize)
                .FromComponentInNewPrefab(specialEnemyPrefab)
                .UnderTransformGroup("Special Enemies");

            Container.BindMemoryPool<MiniBoss, MiniBossPool>()
                .WithInitialSize(minibossPoolSize)
                .FromComponentInNewPrefab(miniBossPrefab)
                .UnderTransformGroup("MiniBoss");

            Container.BindMemoryPool<EnemyDeathFx, EnemyDeathFxPool>()
                .WithInitialSize(enemyDeathFxPoolSize)
                .FromComponentInNewPrefab(enemyDeathFxPrefab)
                .UnderTransformGroup("EnemyDeathFx");

            Container.BindMemoryPool<ShieldBrick, ShieldBricksPool>()
                .WithInitialSize(shieldBrickPoolSize)
                .FromComponentInNewPrefab(shieldBrickPrefab)
                .UnderTransformGroup("ShieldBricks");
        }
    }
}