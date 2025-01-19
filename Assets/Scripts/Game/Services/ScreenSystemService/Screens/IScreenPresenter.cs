namespace Cubes.Game.Services
{
    internal interface IScreenPresenter
    {
        public bool IsShown { get; }

        public void Init(IScreenModel model, BaseScreenView view);

        public void Show();

        public void Hide();

        public void Destroy();
    }
}
