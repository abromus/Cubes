using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class GameOverScreenPresenter : BaseScreenPresenter
    {
        private GameOverScreenModel _model;
        private GameOverScreenView _view;

        private readonly CompositeDisposable _subscriptions = new();

        public override bool IsShown => _model.IsShown.Value;

        public override void Init(IScreenModel model, BaseScreenView view)
        {
            _model = model as GameOverScreenModel;
            _view = view as GameOverScreenView;
        }

        public override void Show()
        {
            Subscribe();

            _model.UpdateIsShown(true);
        }

        public override void Hide()
        {
            _model.UpdateIsShown(false);

            Unsubscribe();
        }

        public override void Destroy()
        {
            _view.Destroy();
        }

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
