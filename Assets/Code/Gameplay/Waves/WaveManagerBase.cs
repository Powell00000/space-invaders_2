using Game.Gameplay;
using System;
using UnityEngine;

namespace Assets.Code.Gameplay.Waves
{
    public abstract class WaveManagerBase : MonoBehaviour, Zenject.IInitializable, IDisposable
    {
        [Serializable]
        public class Wave
        {
            private GridParent gridParent;
            private WaveHolder waveSettings;

            public GridParent SpawnedGridParent => gridParent;
            public WaveHolder WaveSettings => waveSettings;

            public Wave(GridParent spawnedGridParent, WaveHolder waveSettings)
            {
                this.gridParent = spawnedGridParent;
                this.waveSettings = waveSettings;
            }
        }

        public Action<Wave> OnWaveTriggered;
        public Action OnAllWavesFinished;

        public abstract void Dispose();

        public abstract void Initialize();
    }
}
