using Assets.Code.Gameplay.Waves;
using Game.Gameplay;

namespace Assets.Code.Gameplay.Units.Enemies
{
    internal class EnemyGridSpawner : EnemySpawnerBase
    {
        [Zenject.Inject] private WaveManagerBase waveManager = null;
        [Zenject.Inject] private EnemyPool enemyPool = null;
        //[Zenject.Inject] private MiniBossPool miniBossPool = null;
        [Zenject.Inject] private PlayableArea playableArea = null;
        [Zenject.Inject] private EnemyDeathFxPool enemyDeathFxPool = null;

        public override void Initialize()
        {
            waveManager.OnWaveTriggered += SpawnEnemies;
        }

        private void SpawnEnemies(WaveManagerBase.Wave currentWave)
        {
            //we take grids setup from Grid parent
            foreach (var gridArea in currentWave.SpawnedGridParent.Grids)
            {
                //for every cell in GridArea in GridParent
                for (int i = 0; i < gridArea.CellsCount; i++)
                {
                    bool hasFreeCell = gridArea.TryGetFreeCell(out var cell);

                    if (hasFreeCell)
                    {
                        var spawnContext = new Enemy.SpawnContext(cell.WorldTransform.position, cell, enemyDeathFxPool);
                        var spawnedEnemy = enemyPool.Spawn(spawnContext);

                        cell.OccupyCell(spawnedEnemy);

                        if (OnEnemySpawned != null)
                        {
                            OnEnemySpawned(spawnedEnemy);
                        }
                    }
                }
            }
        }

        public override void Dispose()
        {
            waveManager.OnWaveTriggered -= SpawnEnemies;
        }
    }
}
