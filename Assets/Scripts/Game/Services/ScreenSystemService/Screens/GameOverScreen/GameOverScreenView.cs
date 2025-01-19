using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class GameOverScreenView : BaseScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        [Zenject.Inject] private readonly AudioService _audioService;

        private GameOverScreenPresenter _presenter;

        private readonly CompositeDisposable _subscriptions = new();

        internal override Configs.ScreenType ScreenType => Configs.ScreenType.GameOver;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(IScreenPresenter presenter)
        {
            _presenter = presenter as GameOverScreenPresenter;
        }

        internal override void Show()
        {
            base.Show();

            PlayBackgroundMusic();
            Subscribe();
        }

        internal override void Hide()
        {
            base.Hide();

            StopBackgroundMusic();
            Unsubscribe();
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
