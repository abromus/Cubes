namespace Cubes.Game.World
{
    internal interface IShapeView
    {
        public void Init(IShapePresenter presenter);

        public void UpdateConfig(in Configs.ShapeInfo info);
    }
}
