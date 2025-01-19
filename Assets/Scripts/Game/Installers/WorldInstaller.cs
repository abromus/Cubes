namespace Cubes.Game.Installers
{
    internal sealed class WorldInstaller : Zenject.MonoInstaller
    {
        [Zenject.Inject] private readonly Configs.ShapesConfig _shapesConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<World.World>().AsSingle();

            InstallEntities();
        }

        private void InstallEntities()
        {
            InstallModels();
            InstallViews();
            InstallPresenters();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InstallModels()
        {
            Container.BindInterfacesAndSelfTo<World.CubeModel>().AsTransient();
        }

        private void InstallViews()
        {
            var shapes = _shapesConfig.ShapeTypeInfos;

            for (int i = 0; i < shapes.Length; i++)
            {
                var shape = shapes[i].Prefab;

                Container.BindInterfacesAndSelfTo(shape.GetType()).FromComponentInNewPrefab(shape).AsSingle();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InstallPresenters()
        {
            Container.BindInterfacesAndSelfTo<World.CubePresenter>().AsTransient();
        }
    }
}
