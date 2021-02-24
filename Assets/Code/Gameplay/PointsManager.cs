using Assets.Code.Gameplay.Units.Enemies;
using System;

namespace Game.Gameplay
{
    //keeping track of points
    public class PointsManager : Zenject.IInitializable, IDisposable
    {
        [Zenject.Inject] private EnemySpawnerBase enemySpawner = null;
        [Zenject.Inject] private GameplayController gameplayCtrl = null;
        private int currentPoints;
        public Action<int> OnPointsChanged;

        public int CurrentPoints => currentPoints;

        public void Initialize()
        {
            enemySpawner.OnEnemySpawned += EnemySpawned;
            gameplayCtrl.OnLevelStarting += ClearPoints;
            ClearPoints();
        }

        private void EnemySpawned(Unit unit)
        {
            unit.OnDeath += OnUnitDeath;
        }

        private void OnUnitDeath(Unit deadUnit)
        {
            AddPoints(deadUnit.GetPointsForDestruction());
        }

        private void ClearPoints()
        {
            SetPoints(0);
        }

        private void SetPoints(int newValue)
        {
            currentPoints = newValue;
            RaisePointsChangedEvent();
        }

        public void AddPoints(int amount)
        {
            SetPoints(currentPoints + amount);
        }

        private void RaisePointsChangedEvent()
        {
            if (OnPointsChanged != null)
            {
                OnPointsChanged(currentPoints);
            }
        }

        void IDisposable.Dispose()
        {
            if (enemySpawner != null)
            {
                enemySpawner.OnEnemySpawned -= EnemySpawned;
            }

            if (gameplayCtrl != null)
            {
                gameplayCtrl.OnLevelStarting -= ClearPoints;
            }
        }
    }
}