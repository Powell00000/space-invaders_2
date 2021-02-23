using Assets.Code.Gameplay.Units.Enemies;
using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Gameplay
{
    //here we manage the wave progression, so keeping track of spawned and destroyed enemies
    public class WaveProgressController : IInitializable, IDisposable
    {
        [Inject] private EnemySpawnerBase enemySpawner = null;
        [Inject] private InputController inputCtrl = null;
        [Inject] private GameplayController gameplayCtrl = null;

        public Action OnWaveFinished;
        private List<Unit> aliveEnemies;
        private List<Unit> enemiesAbleToShoot;

        //TODO: should it be public?
        public List<Unit> AliveEnemies => aliveEnemies;
        public List<Unit> EnemiesAbleToShoot => GetEnemiesAbleToShoot();

        private int aliveEnemiesCount => aliveEnemies.Count;

        private List<Unit> GetEnemiesAbleToShoot()
        {
            enemiesAbleToShoot.Clear();
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                if (aliveEnemies[i].CanShoot)
                {
                    enemiesAbleToShoot.Add(aliveEnemies[i]);
                }
            }
            return enemiesAbleToShoot;
        }

        void IInitializable.Initialize()
        {
            aliveEnemies = new List<Unit>(50);
            enemiesAbleToShoot = new List<Unit>(50);
            enemySpawner.OnEnemySpawned += OnEnemySpawned;
            //for progressing faster
            inputCtrl.KillAllEnemies += DespawnAliveEnemies;
            //for restarting -> cleanup
            gameplayCtrl.OnLevelRestarting += ClearList;
        }

        //TODO: memory management
        private void DespawnAliveEnemies()
        {
            //we cannot operate on aliveEnemies, because there are already spawning new enemies and collection changes
            List<Unit> tmp = new List<Unit>(aliveEnemies);
            for (int i = 0; i < tmp.Count; i++)
            {
                tmp[i].ForceDeath();
            }
            //we don't need to clear aliveEnemies, because ForceDeath() triggers OnEnemyDeath()
            tmp = null;
        }

        private void ClearList()
        {
            aliveEnemies.Clear();
        }

        private void OnEnemyDeath(Unit deadUnit)
        {
            var enemy = deadUnit;
            aliveEnemies.Remove(enemy);

            if (aliveEnemiesCount == 0)
            {
                if (OnWaveFinished != null)
                {
                    OnWaveFinished();
                }
            }
        }

        private void OnEnemySpawned(Unit spawnedUnit)
        {
            var enemy = spawnedUnit;
            aliveEnemies.Add(enemy);

            spawnedUnit.OnDeath += OnEnemyDeath;
        }

        void IDisposable.Dispose()
        {
            if (enemySpawner != null)
            {
                enemySpawner.OnEnemySpawned -= OnEnemySpawned;
            }

            if (inputCtrl != null)
            {
                inputCtrl.KillAllEnemies -= DespawnAliveEnemies;
            }

            if (gameplayCtrl != null)
            {
                gameplayCtrl.OnLevelRestarting -= ClearList;
            }
        }
    }
}