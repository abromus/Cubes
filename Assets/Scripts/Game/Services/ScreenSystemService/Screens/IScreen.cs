namespace Cubes.Game.Services
{
    internal interface IScreen
    {
        public Configs.ScreenType ScreenType { get; }

        public bool IsShown { get; }

        public void Show();

        public void Hide();
    }
}
