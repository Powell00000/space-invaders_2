using Assets.Code.Gameplay.Waves;
using Game.Gameplay;
using UnityEngine;

namespace Assets.Code.Gameplay.Units.Enemies
{
    internal class EnemyGridSpawner : EnemySpawnerBase
    {
        [Zenject.Inject] private WaveManagerBase waveManager = null;
        [Zenject.Inject] private EnemyPool enemyPool = null;
        [Zenject.Inject] private MiniBossPool miniBossPool = null;
        [Zenject.Inject] private PlayableArea playableArea = null;
        [Zenject.Inject] private EnemyDeathFxPool enemyDeathFxPool = null;

        private Vector3 targetPosOnArc;
        private Vector3 spawnOffset;

        public override void Initialize()
        {
            waveManager.OnWaveTriggered += SpawnEnemies;
            spawnOffset = Vector3.up * playableArea.Width / 1.5f;
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

                    if (OnEnemySpawned != null)
                    {
                        OnEnemySpawned(spawnedEnemy);
                    }
                }
            }

            for (int i = 0; i < currentWave.WaveSettings.MiniBossCount; i++)
            {
                //SpawnMiniBoss();
            }
        }

        private void SpawnMiniBoss()
        {
            //random position on half sphere outside screen view
            targetPosOnArc = Quaternion.AngleAxis(Random.Range(-90f, 90f), Vector3.forward) * spawnOffset;

            var miniBoss = miniBossPool.Spawn(new MiniBoss.SpawnContext(targetPosOnArc, enemyDeathFxPool));
            if (OnEnemySpawned != null)
            {
                OnEnemySpawned(miniBoss);
            }
        }

        public override void Dispose()
        {
            waveManager.OnWaveTriggered -= SpawnEnemies;
        }
    }
}
