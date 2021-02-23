using System.Collections.Generic;
using Zenject;

namespace Assets.Code.Gameplay.Units.Shield
{
    public class ShieldBricksPool : MonoMemoryPool<ShieldBrick.SpawnContext, ShieldBrick>
    {
        private List<ShieldBrick> spawnedItems = new List<ShieldBrick>();

        protected override void Reinitialize(ShieldBrick.SpawnContext p1, ShieldBrick item)
        {
            item.OnSpawned(p1, this);
            base.Reinitialize(p1, item);
        }

        protected override void OnSpawned(ShieldBrick item)
        {
            base.OnSpawned(item);
            spawnedItems.Add(item);
        }

        protected override void OnDespawned(ShieldBrick item)
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
