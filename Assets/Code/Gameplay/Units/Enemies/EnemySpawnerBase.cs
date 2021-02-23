using Game.Gameplay;
using System;

namespace Assets.Code.Gameplay.Units.Enemies
{
    public abstract class EnemySpawnerBase : Zenject.IInitializable, IDisposable
    {
        public Action<Unit> OnEnemySpawned;

        public abstract void Dispose();
        public abstract void Initialize();
    }
}
