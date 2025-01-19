namespace Cubes.Game.Services
{
    internal abstract class BaseScreenPresenter : IScreenPresenter
    {
        public abstract bool IsShown { get; }

        public abstract void Init(IScreenModel model, BaseScreenView view);

        public abstract void Show();

        public abstract void Hide();

        public abstract void Destroy();
    }
}
