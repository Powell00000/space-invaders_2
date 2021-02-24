using Assets.Code.Gameplay.Waves;
using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Code.Gameplay.Units.Enemies
{
    internal class EnemyGridSpawner : EnemySpawnerBase, ITickable
    {
        [Inject] private WaveManagerBase waveManager = null;
        [Inject] private EnemyPool enemyPool = null;
        [Inject] private SpecialEnemyPool specialEnemyPool = null;
        [Inject] private PlayableArea playableArea = null;
        [Inject] private GameplayController gameplayCtrl = null;
        [Inject] private EnemyDeathFxPool enemyDeathFxPool = null;

        private float timeElapsed;

        public override void Initialize()
        {
            waveManager.OnWaveTriggered += SpawnEnemies;
        }

        private void SpawnEnemies(WaveManagerBase.Wave currentWave)
        {
            //we take grids setup from Grid parent
            foreach (var gridArea in currentWave.SpawnedGridParent.Grids)
            {
                EnemyStats enemyStats = gridArea.OverrideEnemyStats;

                //for every cell in GridArea in GridParent
                for (int i = 0; i < gridArea.CellsCount; i++)
                {
                    var cell = gridArea.Cells[i];
                    var spawnContext = new Enemy.SpawnContext(cell.WorldTransform.position, cell, enemyDeathFxPool, enemyStats);
                    var spawnedEnemy = enemyPool.Spawn(spawnContext);

                    cell.OccupyCell(spawnedEnemy);

                    OnEnemySpawned?.Invoke(spawnedEnemy);
                }
            }
        }

        public override void Dispose()
        {
            waveManager.OnWaveTriggered -= SpawnEnemies;
        }

        public void Tick()
        {
            if (gameplayCtrl.CurrentGameplayState != EGameplayState.Playing)
            {
                return;
            }

            if (specialEnemyPool.NumActive != 0)
            {
                return;
            }

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= gameplayCtrl.GameplaySettings.SecondsToSpawnSpecialShip)
            {
                timeElapsed = 0;
                SpawnSpecialEnemy();
            }
        }

        private void SpawnSpecialEnemy()
        {
            var spawnPosition = new Vector3(playableArea.Left, playableArea.TopGridSpawnPosition.y + 1.5f, 0);
            var direction = Vector3.right;
            var specialEnemy = specialEnemyPool.Spawn(new SpecialEnemy.SpawnContext(spawnPosition, direction, enemyDeathFxPool));
            OnEnemySpawned?.Invoke(specialEnemy);
        }
    }
}
