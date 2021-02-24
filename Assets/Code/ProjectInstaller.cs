using Assets.Code;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LeaderboardController>().AsSingle();
    }
}
