namespace Cubes.Game.World
{
    internal interface IShapeView
    {
        public void Init(IShapePresenter presenter, in Configs.ShapeInfo info);
    }
}
