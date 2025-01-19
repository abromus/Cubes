namespace Cubes.Game.Services
{
    internal sealed class GameOverScreenModel : IScreenModel
    {
        internal UniRx.ReactiveProperty<bool> IsShown { get; private set; }

        [UnityEngine.Scripting.Preserve]
        public GameOverScreenModel()
        {
            IsShown = new UniRx.ReactiveProperty<bool>(false);
        }

        internal void UpdateIsShown(bool isShown)
        {
            IsShown.Value = isShown;
        }
    }
}
