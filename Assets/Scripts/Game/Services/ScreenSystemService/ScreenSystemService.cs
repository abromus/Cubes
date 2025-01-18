namespace Cubes.Game.Services
{
    internal sealed class ScreenSystemService : UnityEngine.MonoBehaviour, System.IDisposable
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _screensContainer;

        private Configs.ScreenInfo[] _screenInfos;

        [Zenject.Inject] private readonly Zenject.IInstantiator _diContainer;
        [Zenject.Inject] private readonly Configs.ScreensConfig _screensConfig;

        private readonly System.Collections.Generic.Dictionary<Configs.ScreenType, BaseScreen> _screens = new(4);

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            _screenInfos = _screensConfig.ScreenInfos;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            var screens = _screens.Values;

            foreach (var screen in screens)
                if (screen != null && screen.gameObject != null)
                    Destroy(screen.gameObject);

            _screens.Clear();
        }

        internal void Show(Configs.ScreenType screenType)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(screenType == Configs.ScreenType.None);
#endif

            if (TryShowFromCache(screenType))
                return;

            for (int i = 0; i < _screenInfos.Length; i++)
            {
                var screenInfo = _screenInfos[i];

                if (screenInfo.ScreenType != screenType)
                    continue;

                var screenPrefab = screenInfo.Screen;
                var screen = _diContainer.InstantiatePrefabForComponent<BaseScreen>(screenPrefab, _screensContainer);
                screen.Show();

                _screens.Add(screenType, screen);

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
