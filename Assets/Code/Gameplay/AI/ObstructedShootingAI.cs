using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    //Hive mind for enemies to maintain shooting
    public class ObstructedShootingAI : IInitializable, ITickable
    {
        //for list of alive enemies
        [Inject] private WaveProgressController waveProgressCtrl = null;
        [Inject] private GameplayController gameplayCtrl = null;

        private AnimationCurve enemiesCountToShootTime;
        private float timer;
        private float shootTime = 1f;

        void IInitializable.Initialize()
        {
            //more enemies = faster shooting, because with constant value more enemies = slower shooting per group
            enemiesCountToShootTime = AnimationCurve.Linear(2f, 1f, 15f, 0.35f);
        }

        void ITickable.Tick()
        {
            if (gameplayCtrl.CurrentGameplayState == EGameplayState.Playing)
            {
                if (waveProgressCtrl.AliveEnemies.Count == 0)
                {
                    return;
                }

                shootTime = enemiesCountToShootTime.Evaluate(waveProgressCtrl.AliveEnemies.Count);

                timer += Time.deltaTime;
                if (timer >= shootTime)
                {
                    var randomEnemyIndex = Random.Range(0, waveProgressCtrl.AliveEnemies.Count);
                    waveProgressCtrl.AliveEnemies[randomEnemyIndex].ShootIfCan();
                    timer = Random.Range(0, shootTime);
                }
            }
        }
    }
}