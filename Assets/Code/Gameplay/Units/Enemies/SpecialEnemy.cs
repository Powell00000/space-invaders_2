using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Code.Gameplay.Units.Enemies
{
    public class SpecialEnemy : Unit, IPoolable<SpecialEnemy.SpawnContext, SpecialEnemyPool>
    {
        protected EnemyStats enemyStats => (EnemyStats)Stats;

        private Vector3 direction;

        public void OnDespawned()
        {
        }

        public void OnSpawned(SpawnContext context, SpecialEnemyPool pool)
        {
        }

        protected override void MovementFunction()
        {
            transform.position += direction * enemyStats.FlySpeed * Time.deltaTime;
        }

        protected override void Shoot()
        {
        }

        public struct SpawnContext
        {

        }
    }
}
