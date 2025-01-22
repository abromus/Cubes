using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen.Restrictions
{
    internal interface IRestriction
    {
        public bool Check(IShapePresenter lastShape, IShapePresenter shape, out ResolverStatus status);
    }
}
