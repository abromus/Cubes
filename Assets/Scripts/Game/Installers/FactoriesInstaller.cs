namespace Cubes.Game.Installers
{
    internal sealed class FactoriesInstaller : Zenject.MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallFactories();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InstallFactories()
        {
            Container.BindInterfacesAndSelfTo<Factories.ScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Factories.ShapeFactory>().AsSingle();
        }
    }
}
