namespace Cubes.Game.Installers
{
    internal sealed class GameInstaller : Zenject.MonoInstaller
    {
        [UnityEngine.SerializeField] private GameSceneController _gameSceneController;

        [Zenject.Inject] private readonly Configs.IShapesConfig _shapesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameSceneController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Game.Game>().AsSingle();

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
            Container.BindInterfacesAndSelfTo<UI.MainScreen.Shapes.CubeModel>().AsTransient();
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
            Container.BindInterfacesAndSelfTo<UI.MainScreen.Shapes.CubePresenter>().AsTransient();
        }
    }
}
