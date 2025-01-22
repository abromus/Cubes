using UniRx;

namespace Cubes.Game.UI.SettingsScreen
{
    internal sealed class SettingsScreenPresenter : Services.BaseScreenPresenter
    {
        private SettingsScreenModel _model;
        private SettingsScreenView _view;

        private readonly CompositeDisposable _subscriptions = new();

        public override bool IsShown => _model.IsShown.Value;

        public override void Init(Services.IScreenModel model, Services.BaseScreenView view)
        {
            _model = model as SettingsScreenModel;
            _view = view as SettingsScreenView;
        }

        public override void Show()
        {
            Subscribe();

            _model.UpdateIsShown(true);
        }

        public override void Hide()
        {
            if (_model.IsShown.Value == false)
                return;

            _model.UpdateIsShown(false);

            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            _view.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _model.IsShown.Subscribe(OnUpdateIsShown).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnUpdateIsShown(bool isShown)
        {
            if (isShown)
                _view.Show();
            else
                _view.Hide();
        }
    }
}
