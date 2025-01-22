namespace Cubes.Game.Installers
{
    internal sealed class GameDataInstaller : Zenject.MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Data.GameData>().AsSingle();
        }
    }
}
