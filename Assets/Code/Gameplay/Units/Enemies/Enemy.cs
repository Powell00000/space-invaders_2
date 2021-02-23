using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class Enemy : Unit, IPoolable<Enemy.SpawnContext, EnemyPool>
    {
        //TODO: helper?
        [Inject] private ProjectilesPool projectilePool = null;

        //TODO: STRONG TYPING
        private EnemyStats enemyStats => (EnemyStats)Stats;
        private EnemyPool enemyPool;
        private SpawnContext context;
        private int currentAnimationFrame;
        private const float animationDelay = 1f;
        private float animationTimeElapsed;

        public System.Action<Enemy> OnPooled;

        public void OnDespawned()
        {
            if (OnPooled != null)
            {
                OnPooled(this);
            }

            ClearEvents();
        }

        protected override void Update()
        {
            base.Update();
            Animate();
        }

        private void Animate()
        {
            if (enemyStats.AnimationFrames == null || enemyStats.AnimationFrames.Length == 0)
            {
                return;
            }

            if (animationTimeElapsed >= animationDelay)
            {
                spriteRenderer.sprite = enemyStats.AnimationFrames[currentAnimationFrame];
                animationTimeElapsed = 0;
                currentAnimationFrame++;
                if (currentAnimationFrame >= enemyStats.AnimationFrames.Length)
                {
                    currentAnimationFrame = 0;
                }
            }
            animationTimeElapsed += Time.deltaTime;
        }

        protected override void ClearEvents()
        {
            base.ClearEvents();
            OnPooled = null;
        }

        protected override void Shoot()
        {
            projectilePool.Spawn(new Projectile.SpawnContext(
                transform.position,
                this,
                enemyStats.BaseProjectileSpeed,
                -Vector3.up,
                enemyStats.Color
            ));
        }

        public void OnSpawned(SpawnContext spawnContext, EnemyPool enemyPool)
        {
            faction = EFaction.Enemy;

            context = spawnContext;
            if (context.EnemyStats != null)
            {
                Stats = context.EnemyStats;
            }
            else
            {
                Stats = unitDefinitionsProvider.BasicEnemy.Stats;
            }

            transform.position = spawnContext.Position;
            this.enemyPool = enemyPool;
            Initialize();
        }

        protected override void Death()
        {
            base.Death();
            context.DeathFxPool.Spawn(new EnemyDeathFx.SpawnContext()
            {
                Position = transform.position,
                ColorHD = enemyStats.Color
            });
            enemyPool.Despawn(this);
        }

        protected override void MovementFunction()
        {
            Vector3 targetPos = transform.position;
            if (context.Cell != null)
            {
                if (context.Cell.WorldTransform != null)
                {
                    targetPos = context.Cell.WorldTransform.position;
                }
            }

            Vector3 calculatedPosition;
            if (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                calculatedPosition = Vector3.MoveTowards(transform.position, targetPos, enemyStats.FlySpeed * Time.deltaTime);
            }
            else
            {
                calculatedPosition = Vector3.MoveTowards(transform.position, targetPos, enemyStats.MoveSpeed * Time.deltaTime);
            }
            transform.position = calculatedPosition;
        }

        public struct SpawnContext
        {
            private GridArea.Cell cell;
            private Vector3 position;
            private EnemyDeathFxPool deathFxPool;
            private EnemyStats enemyStats;

            public Vector3 Position => position;
            public GridArea.Cell Cell => cell;
            public EnemyDeathFxPool DeathFxPool => deathFxPool;
            public EnemyStats EnemyStats => enemyStats;

            public SpawnContext(Vector3 position, GridArea.Cell cell, EnemyDeathFxPool deathFxPool, EnemyStats enemyStats = null)
            {
                this.position = position;
                this.cell = cell;
                this.deathFxPool = deathFxPool;
                this.enemyStats = enemyStats;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            //TODO: this is NOT strong typing; create custom inspector or so
            if (Stats != null)
            {
                if (enemyStats == null)
                {
                    Stats = null;
                    UnityEditor.EditorUtility.SetDirty(this);
                }
            }
        }

#endif
    }
}