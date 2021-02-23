using Assets.Code.Gameplay.Units.Enemies;
using System.Collections.Generic;
using Zenject;

namespace Game.Gameplay
{
    public class SpecialEnemyPool : MonoMemoryPool<SpecialEnemy.SpawnContext, SpecialEnemy>
    {
        private List<SpecialEnemy> spawnedItems = new List<SpecialEnemy>();

        protected override void Reinitialize(SpecialEnemy.SpawnContext p1, SpecialEnemy item)
        {
            item.OnSpawned(p1, this);
            base.Reinitialize(p1, item);
        }

        protected override void OnSpawned(SpecialEnemy item)
        {
            base.OnSpawned(item);
            spawnedItems.Add(item);
        }

        protected override void OnDespawned(SpecialEnemy item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
            spawnedItems.Remove(item);
            //ConditionalLogger.Log("enemy despawned");
        }

        public void DespawnAll()
        {
            for (int i = 0; i < spawnedItems.Count;)
            {
                this.Despawn(spawnedItems[i]);
            }
            spawnedItems.Clear();
        }
    }
}