using Assets.Code.Gameplay;
using Assets.Code.Gameplay.Units.Enemies;
using Assets.Code.Gameplay.Waves;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayController gameplayController = null;
        [SerializeField] private InputController inputController = null;
        [SerializeField] private UnitDefinitionsProvider unitDefinitionsProvider = null;
        [SerializeField] private PlayableArea playableArea = null;
        [SerializeField] private WaveManagerBase waveManager = null;
        [SerializeField] private GameplaySettings gameplaySettings;


        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().AsSingle();

            Container.Bind<InputController>().FromInstance(inputController);
            Container.Bind<PlayableArea>().FromInstance(playableArea);
            Container.Bind<UnitDefinitionsProvider>().FromInstance(unitDefinitionsProvider);
            Container.Bind<GameplaySettings>().FromInstance(gameplaySettings);

            var enemySpawner = new EnemyGridSpawner();
            Container.BindInterfacesAndSelfTo<EnemySpawnerBase>().FromInstance(enemySpawner);
            Container.QueueForInject(enemySpawner);

            Container.BindInterfacesAndSelfTo<GlobalShootingAI>().AsSingle();
            Container.BindInterfacesAndSelfTo<PointsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<PoolsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaveProgressController>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaveManagerBase>().FromInstance(waveManager);
            Container.BindInterfacesAndSelfTo<GameplayController>().FromInstance(gameplayController);

        }
    }
}