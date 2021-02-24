using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Code.Gameplay.Units.Enemies
{
    public class SpecialEnemy : Unit, IPoolable<SpecialEnemy.SpawnContext, SpecialEnemyPool>
    {
        [Inject] private PlayableArea playableArea;

        [SerializeField]
        private EnemyStats stats;

        protected EnemyStats enemyStats => (EnemyStats)Stats;

        private SpecialEnemyPool specialEnemyPool;
        private Vector3 direction;
        private float maxDistance;
        private float traveledDistance;
        private Vector3 startingPointInGameBounds;
        private SpawnContext context;

        private bool IsMovingRight => direction.x > 0;

        public override bool CanShoot => false;

        protected override void Initialize()
        {
            base.Initialize();
            maxDistance = playableArea.Width;
            traveledDistance = 0;
        }

        public void OnDespawned()
        {
            ClearEvents();
        }

        protected override void Death()
        {
            base.Death();
            context.DeathFxPool.Spawn(new EnemyDeathFx.SpawnContext()
            {
                Position = transform.position,
                ColorHD = enemyStats.Color
            });
            specialEnemyPool.Despawn(this);
        }

        public void OnSpawned(SpawnContext context, SpecialEnemyPool pool)
        {
            this.context = context;
            transform.position = startingPointInGameBounds = context.Position;
            direction = context.Direction;
            faction = EFaction.Enemy;
            Stats = stats;
            specialEnemyPool = pool;

            if (IsMovingRight)
            {
                startingPointInGameBounds.WithX(playableArea.Left);
            }
            else
            {
                startingPointInGameBounds.WithX(playableArea.Right);
            }

            context.DeathFxPool.Spawn(new EnemyDeathFx.SpawnContext()
            {
                Position = transform.position,
                ColorHD = enemyStats.Color
            });

            Initialize();
        }

        protected override void MovementFunction()
        {
            if (playableArea.GameBounds.Contains(transform.position))
            {
                traveledDistance = Vector3.Distance(startingPointInGameBounds, transform.position);
                transform.position += direction * enemyStats.MoveSpeed * Time.deltaTime;
            }
            else
            {
                traveledDistance = 0;
                transform.position += direction * enemyStats.FlySpeed * Time.deltaTime;
            }

            if (Vector3.Distance(startingPointInGameBounds, transform.position) > maxDistance * 1.2f)
            {
                Death();
            }
        }

        protected override void Shoot()
        {
        }

        public override int GetPointsForDestruction()
        {
            float percentage = traveledDistance / maxDistance;
            var points = Mathf.CeilToInt(Stats.PointsForDestroying * percentage);
            ConditionalLogger.Log($"points for special enemy: {points}");
            return points;
        }

        public struct SpawnContext
        {
            private Vector3 direction;
            private Vector3 position;
            private EnemyDeathFxPool deathFxPool;

            public Vector3 Direction => direction;
            public Vector3 Position => position;
            public EnemyDeathFxPool DeathFxPool => deathFxPool;

            public SpawnContext(Vector3 position, Vector3 direction, EnemyDeathFxPool deathFxPool)
            {
                this.position = position;
                this.direction = direction;
                this.deathFxPool = deathFxPool;
            }
        }
    }
}
