using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class GameOverScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        private bool _isShown;

        [Zenject.Inject] private readonly AudioService _audioService;

        private readonly CompositeDisposable _subscriptions = new();

        public override Configs.ScreenType ScreenType => Configs.ScreenType.GameOver;

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

        private void Subscribe()
        {
            _buttonRestart.OnClickAsObservable().Subscribe(OnButtonRestartClicked).AddTo(_subscriptions);
            _buttonExit.OnClickAsObservable().Subscribe(OnButtonExitClicked).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnButtonRestartClicked(Unit _)
        {
            Hide();

            //_gameData.Restart();
        }

        private void OnButtonExitClicked(Unit _)
        {
            //_gameData.Exit();
        }
    }
}
