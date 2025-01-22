using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class SettingsScreenView : BaseScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _toggleBackgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _togglesSound;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonApply;
        [UnityEngine.SerializeField] private SettingsScreenLocalizer _localizer;

        private SettingsScreenPresenter _presenter;

        [Zenject.Inject] private readonly Data.GameData _gameData;
        [Zenject.Inject] private readonly AudioService _audioService;
        [Zenject.Inject] private readonly LocalizeService _localizeService;

        private readonly CompositeDisposable _subscriptions = new();

        internal override Configs.ScreenType ScreenType => Configs.ScreenType.Settings;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(IScreenPresenter presenter)
        {
            _presenter = presenter as SettingsScreenPresenter;

            _localizer.Init(_localizeService);
        }

        internal override void Show()
        {
            base.Show();

            Subscribe();
        }

        internal override void Hide()
        {
            base.Hide();

            Unsubscribe();
        }

        private void Subscribe()
        {
            _toggleBackgroundMusic.OnValueChangedAsObservable().Subscribe(OnBackgroundMusicChanged).AddTo(_subscriptions);
            _togglesSound.OnValueChangedAsObservable().Subscribe(OnSoundsChanged).AddTo(_subscriptions);
            _buttonRestart.OnClickAsObservable().Subscribe(OnButtonRestartClicked).AddTo(_subscriptions);
            _buttonExit.OnClickAsObservable().Subscribe(OnButtonExitClicked).AddTo(_subscriptions);
            _buttonApply.OnClickAsObservable().Subscribe(OnButtonApplyClicked).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnBackgroundMusicChanged(bool isActive)
        {
            _audioService.SetActiveBackgroundMusic(isActive);
        }

        private void OnSoundsChanged(bool isActive)
        {
            _audioService.SetActiveSounds(isActive);
        }

        private void OnButtonRestartClicked(Unit _)
        {
            _gameData.Restart();

            _presenter.Hide();
        }

        private void OnButtonExitClicked(Unit _)
        {
            _gameData.Exit();
        }

        private void OnButtonApplyClicked(Unit _)
        {
            _presenter.Hide();
        }
    }
}
