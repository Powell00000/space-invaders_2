using Assets.Code.Gameplay.Units.Enemies;
using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    //keeping track of points
    public class PointsManager : Zenject.IInitializable, IDisposable, ITickable
    {
        [Inject] private EnemySpawnerBase enemySpawner = null;
        [Inject] private GameplayController gameplayCtrl = null;
        private int currentPoints;
        public Action<int> OnPointsChanged;

        public int CurrentPoints => currentPoints;

        private float gameplayTime;

        public void Initialize()
        {
            enemySpawner.OnEnemySpawned += EnemySpawned;
            gameplayCtrl.OnLevelStarting += ClearPoints;
            gameplayCtrl.OnStateChanged += GameplayStateChanged;
            ClearPoints();
        }

        private void GameplayStateChanged(EGameplayState currentState)
        {
            var additionalPoints = gameplayCtrl.GameplaySettings.BasePointsAmountForFinishing * (gameplayTime / gameplayCtrl.GameplaySettings.MaxGameTimeInSeconds);
            AddPoints(Mathf.CeilToInt(additionalPoints));
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
            ConditionalLogger.Log($"added {amount} points");
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

        public void Tick()
        {
            if (gameplayCtrl.CurrentGameplayState != EGameplayState.Playing)
            {
                return;
            }

            gameplayTime += Time.deltaTime;
        }
    }
}