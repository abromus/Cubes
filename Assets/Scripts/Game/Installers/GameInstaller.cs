namespace Cubes.Game.Installers
{
    internal sealed class GameInstaller : Zenject.MonoInstaller
    {
        [UnityEngine.SerializeField] private GameSceneController _gameSceneController;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private Services.AudioService _audioServicePrefab;
        [UnityEngine.SerializeField] private Services.ScreenSystemService _screenSystemServicePrefab;
        [UnityEngine.SerializeField] private Services.EventSystemService _eventSystemServicePrefab;

        [Zenject.Inject] private readonly Configs.ScreensConfig _screensConfig;

        public override void InstallBindings()
        {
            InstallBootstrap();
            InstallServices();
            InstallWorld();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InstallBootstrap()
        {
            Container.BindInstance(_gameSceneController).AsSingle().NonLazy();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<Services.AudioService>().FromComponentInNewPrefab(_audioServicePrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<Services.EventSystemService>().FromComponentInNewPrefab(_eventSystemServicePrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Services.ScreenSystemService>().FromComponentInNewPrefab(_screenSystemServicePrefab).AsSingle();

            var screenInfos = _screensConfig.ScreenInfos;

            for (int i = 0; i < screenInfos.Length; i++)
            {
                var screenInfo = screenInfos[i];
                var screen = screenInfo.Screen;

                Container.BindInterfacesAndSelfTo(screen.GetType()).FromComponentInNewPrefab(screen).AsSingle();
            }

            Container.BindInterfacesAndSelfTo<Core.Services.StateMachine>().AsSingle();
            Container.Bind<Services.GameInitializationState>().AsSingle();
            Container.Bind<Services.GameRestartState>().AsSingle();
            Container.Bind<Services.GameLoopState>().AsSingle();
            Container.Bind<Services.GameOverState>().AsSingle();
            Container.Bind<Services.GameExitState>().AsSingle();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InstallWorld()
        {
            Container.BindInterfacesAndSelfTo<World.World>().AsSingle();
        }
    }
}
