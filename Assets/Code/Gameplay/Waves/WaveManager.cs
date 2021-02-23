using Assets.Code.Gameplay.Waves;
using UnityEngine;

namespace Game.Gameplay
{
    public class WaveManager : WaveManagerBase
    {
        //here is our progression of waves
        [SerializeField] private WaveHolder[] waveStatsHolders = null;

        [Zenject.Inject] private GameplayController gameplayCtrl = null;
        [Zenject.Inject] private WaveProgressController waveProgress = null;
        [Zenject.Inject] private PlayableArea playableArea = null;
        [Zenject.Inject] private InputController inputCtrl = null;

        //for auto positioning
        private const float gridsYOffset = 2;
        private Wave currentWave;
        private GridParent spawnedGridParent;
        private int waveIndex = -1;

        public override void Initialize()
        {
            gameplayCtrl.OnLevelStarting += OnLevelStarting;
            waveProgress.OnWaveFinished += SpawnNextWave;
        }

        private void SpawnNextWave()
        {
            ClearSpawnedAreas();

            waveIndex++;

            if (waveIndex >= waveStatsHolders.Length)
            {
                //game over
                OnAllWavesFinished?.Invoke();
                return;
            }

            //get scriptable with grids definition
            WaveHolder waveStats = waveStatsHolders[waveIndex];

            //GridParent->GridArea[]
            var gridsCount = waveStats.GridParent.Grids.Length;

            //spawn grid parent with all grids inside
            spawnedGridParent = Instantiate(waveStats.GridParent) as GridParent;

            //now iterate through all grids
            for (int i = 0; i < gridsCount; i++)
            {
                GridArea area = spawnedGridParent.Grids[i];

                if (waveStats.AutoPositionRows)
                {
                    area.transform.position = playableArea.TopGridSpawnPosition - Vector3.up * i * gridsYOffset;
                }

                area.Initialize();
            }

            currentWave = new Wave(spawnedGridParent, waveStats);

            OnWaveTriggered?.Invoke(currentWave);
        }

        private void ClearSpawnedAreas()
        {
            //enemies does not despawn with grids
            if (spawnedGridParent != null)
            {
                Destroy(spawnedGridParent.gameObject);
                spawnedGridParent = null;
            }
        }

        private void OnLevelStarting()
        {
            waveIndex = -1;
            SpawnNextWave();
        }

        public override void Dispose()
        {
            if (gameplayCtrl)
            {
                gameplayCtrl.OnLevelStarting -= OnLevelStarting;
            }

            if (waveProgress != null)
            {
                waveProgress.OnWaveFinished -= SpawnNextWave;
            }

            if (inputCtrl != null)
            {
                inputCtrl.KillAllEnemies -= ClearSpawnedAreas;
            }
        }
    }
}