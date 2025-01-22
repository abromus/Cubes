namespace Cubes.Core.Installers
{
    internal sealed class CoreInstaller : Zenject.MonoInstaller
    {
        [UnityEngine.SerializeField] private CoreSceneController _coreSceneController;

        public override void InstallBindings()
        {
            InstallBootstrap();
            InstallServices();
        }

        private void InstallBootstrap()
        {
            Container.BindInstance(_coreSceneController);
            Container.BindInterfacesTo<Bootstrap>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<Services.SceneLoader>().AsSingle();

            Container.BindInterfacesAndSelfTo<Services.StateMachine>().AsSingle();
            Container.Bind<Services.BootstrapState>().AsSingle();
            Container.Bind<Services.GameState>().AsSingle();
            Container.Bind<Services.GameLoopState>().AsSingle();
        }
    }
}
