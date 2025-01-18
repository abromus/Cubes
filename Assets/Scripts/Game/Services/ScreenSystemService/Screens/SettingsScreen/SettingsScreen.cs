using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class SettingsScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonApply;

        private bool _isShown;

        [Zenject.Inject] private readonly Core.Services.UpdaterService _updaterService;

        private readonly CompositeDisposable _subscriptions = new();

        public override Configs.ScreenType ScreenType => Configs.ScreenType.Settings;

        public override bool IsShown => _isShown;

        public override void Show()
        {
            base.Show();

            _updaterService.SetPause(true);

            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            Unsubscribe();

            _updaterService.SetPause(false);

            _isShown = false;
        }

        private void Subscribe()
        {
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

        private void OnButtonRestartClicked(Unit _)
        {
            //_gameData.Restart();

            Hide();
        }

        private void OnButtonExitClicked(Unit _)
        {
            //_gameData.Exit();
        }

        private void OnButtonApplyClicked(Unit _)
        {
            Hide();
        }
    }
}
