namespace Cubes.Game.UI.MainScreen.Shapes
{
    internal interface IShapeView
    {
        public void Init(IShapePresenter presenter);

        public void UpdateConfig(in Configs.ShapeInfo info);
    }
}
