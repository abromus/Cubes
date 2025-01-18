using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class MainScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        private bool _isShown;

        [Zenject.Inject] private readonly AudioService _audioService;
        [Zenject.Inject] private readonly ScreenSystemService _screenSystemService;

        private readonly CompositeDisposable _subscriptions = new();

        public override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        public override bool IsShown => _isShown;

        public override void Show()
        {
            base.Show();

            PlayBackgroundMusic();
            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            StopBackgroundMusic();
            Unsubscribe();

            _isShown = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PlayBackgroundMusic()
        {
            _audioService.PlayBackgroundMusic(_backgroundMusic);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void StopBackgroundMusic()
        {
            _audioService.StopBackgroundMusic();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _buttonSettings.OnClickAsObservable().Subscribe(OnButtonSettingsClicked).AddTo(_subscriptions);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnButtonSettingsClicked(Unit _)
        {
            _screenSystemService.Show(Configs.ScreenType.Settings);
        }
    }
}
