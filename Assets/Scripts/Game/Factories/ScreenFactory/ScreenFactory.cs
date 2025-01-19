using Cubes.Game.Services;

namespace Cubes.Game.Factories
{
    internal sealed class ScreenFactory
    {
        [Zenject.Inject] private readonly Zenject.DiContainer _diContainer;
        [Zenject.Inject] private readonly Configs.ScreensConfig _screensConfig;

        private readonly System.Collections.Generic.Dictionary<Configs.ScreenType, BaseScreenView> _viewPrefabs = new(8);

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            var screens = _screensConfig.Screens;

            foreach (var screen in screens)
                _viewPrefabs.Add(screen.ScreenType, screen);
        }

        internal bool TryCreate(Configs.ScreenType screenType, UnityEngine.Transform parent, out IScreenPresenter presenter)
        {
            presenter = null;

            if (_viewPrefabs.ContainsKey(screenType) == false)
                return false;

            var viewPrefab = _viewPrefabs[screenType];

            switch (screenType)
            {
                case Configs.ScreenType.Main:
                    presenter = Create<MainScreenModel, MainScreenView, MainScreenPresenter>(viewPrefab, parent);

                    return true;
                case Configs.ScreenType.Settings:
                    presenter = Create<SettingsScreenModel, SettingsScreenView, SettingsScreenPresenter>(viewPrefab, parent);

                    return true;
                case Configs.ScreenType.GameOver:
                    presenter = Create<GameOverScreenModel, GameOverScreenView, GameOverScreenPresenter>(viewPrefab, parent);

                    return true;
            }

            return false;
        }

        private IScreenPresenter Create<TModel, TView, TPresenter>(BaseScreenView viewPrefab, UnityEngine.Transform parent)
            where TModel : IScreenModel, new()
            where TView : BaseScreenView
            where TPresenter : IScreenPresenter, new()
        {
            var model = new TModel();
            var view = _diContainer.InstantiatePrefabForComponent<BaseScreenView>(viewPrefab, parent);
            var presenter = _diContainer.Resolve<TPresenter>();

            presenter.Init(model, view);
            view.Init(presenter);

            return presenter;
        }
    }
}
