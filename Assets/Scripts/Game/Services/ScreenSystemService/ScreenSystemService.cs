namespace Cubes.Game.Services
{
    internal sealed class ScreenSystemService : UnityEngine.MonoBehaviour, System.IDisposable
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _screensContainer;

        private BaseScreenView[] _screenViews;

        [Zenject.Inject] private readonly Configs.ScreensConfig _screensConfig;
        [Zenject.Inject] private readonly Factories.ScreenFactory _screenFactory;

        private readonly System.Collections.Generic.Dictionary<Configs.ScreenType, IScreenPresenter> _screens = new(4);

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            _screenViews = _screensConfig.Screens;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            var screens = _screens.Values;

            foreach (var screen in screens)
                screen.Destroy();

            _screens.Clear();
        }

        internal void Show(Configs.ScreenType screenType)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(screenType == Configs.ScreenType.None);
#endif

            if (TryShowFromCache(screenType))
                return;

            for (int i = 0; i < _screenViews.Length; i++)
            {
                var screenViewPrefab = _screenViews[i];

                if (screenViewPrefab.ScreenType != screenType)
                    continue;

                if (_screenFactory.TryCreate(screenType, _screensContainer, out var presenter) == false)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError($"[ScreenSystemService]: Can't create screen {screenType}");
#endif
                }

                presenter.Show();

                _screens.Add(screenType, presenter);

                return;
            }

#if UNITY_EDITOR
            UnityEngine.Debug.LogError($"[ScreenSystemService]: Hasn't screen with type {screenType}");
#endif
        }

        internal void Hide(Configs.ScreenType screenType)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(screenType == Configs.ScreenType.None);
#endif

            if (_screens.ContainsKey(screenType))
                _screens[screenType].Hide();
        }

        internal void HideAll()
        {
            var screens = _screens.Values;

            foreach (var screen in screens)
                screen.Hide();
        }

        private bool TryShowFromCache(Configs.ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType) == false)
                return false;

            var screen = _screens[screenType];

            if (screen.IsShown)
                return true;

            screen.Show();

            return true;
        }
    }
}
