namespace Cubes.Game.Installers
{
    internal sealed class ServicesInstaller : Zenject.MonoInstaller
    {
        [UnityEngine.SerializeField] private Services.AudioService _audioServicePrefab;
        [UnityEngine.SerializeField] private Services.ScreenSystemService _screenSystemServicePrefab;
        [UnityEngine.SerializeField] private Services.EventSystemService _eventSystemServicePrefab;

        [Zenject.Inject] private readonly Configs.ScreensConfig _screensConfig;

        public override void InstallBindings()
        {
            InstallUiServices();
            InstallServices();
        }

        private void InstallUiServices()
        {
            InstallUiPrefabServices();
            InstallModels();
            InstallViews();
            InstallPresenters();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<Core.Services.StateMachine>().AsSingle();
            Container.Bind<Services.GameInitializationState>().AsSingle();
            Container.Bind<Services.GameRestartState>().AsSingle();
            Container.Bind<Services.GameLoopState>().AsSingle();
            Container.Bind<Services.GameExitState>().AsSingle();

            Container.Bind<Services.LocalizeService>().AsSingle();
        }

        private void InstallUiPrefabServices()
        {
            Container.BindInterfacesAndSelfTo<Services.AudioService>().FromComponentInNewPrefab(_audioServicePrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<Services.EventSystemService>().FromComponentInNewPrefab(_eventSystemServicePrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Services.ScreenSystemService>().FromComponentInNewPrefab(_screenSystemServicePrefab).AsSingle();
        }

        private void InstallModels()
        {
            Container.BindInterfacesAndSelfTo<Services.MainScreenModel>().AsTransient();
            Container.BindInterfacesAndSelfTo<Services.SettingsScreenModel>().AsTransient();
        }

        private void InstallViews()
        {
            var screens = _screensConfig.Screens;

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];

                Container.BindInterfacesAndSelfTo(screen.GetType()).FromComponentInNewPrefab(screen).AsSingle();
            }
        }

        private void InstallPresenters()
        {
            Container.BindInterfacesAndSelfTo<Services.MainScreenPresenter>().AsTransient();
            Container.BindInterfacesAndSelfTo<Services.SettingsScreenPresenter>().AsTransient();
        }
    }
}
