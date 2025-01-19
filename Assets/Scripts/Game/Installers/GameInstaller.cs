namespace Cubes.Game.Installers
{
    internal sealed class GameInstaller : Zenject.MonoInstaller
    {
        [UnityEngine.SerializeField] private GameSceneController _gameSceneController;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameSceneController).AsSingle().NonLazy();
        }
    }
}
