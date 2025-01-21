namespace Cubes.Game.Services
{
    internal interface IRestriction
    {
        public bool Check(World.IShapePresenter lastShape, World.IShapePresenter shape, out ResolverStatus status);
    }
}
