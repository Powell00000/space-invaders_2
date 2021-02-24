namespace Game.Gameplay
{
    //Managing clearing scene from poolable objects OnRestart
    public class PoolsController : Zenject.IInitializable, System.IDisposable
    {
        [Zenject.Inject] private ProjectilesPool projectilesPool = null;
        [Zenject.Inject] private EnemyPool enemyPool = null;
        [Zenject.Inject] private MiniBossPool miniBossPool = null;
        [Zenject.Inject] private SpecialEnemyPool specialEnemyPool = null;
        [Zenject.Inject] private GameplayController gameplayCtrl = null;


        public void Initialize()
        {
            gameplayCtrl.OnLevelRestarting += OnRestart;
        }

        public void Dispose()
        {
            gameplayCtrl.OnLevelRestarting -= OnRestart;
        }

        private void OnRestart()
        {
            projectilesPool.DespawnAll();
            enemyPool.DespawnAll();
            miniBossPool.DespawnAll();
            specialEnemyPool.DespawnAll();
        }
    }
}