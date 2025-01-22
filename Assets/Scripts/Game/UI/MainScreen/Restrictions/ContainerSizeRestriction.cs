using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen.Restrictions
{
    internal sealed class ContainerSizeRestriction : IRestriction
    {
        private readonly UnityEngine.Vector2 _size;

        internal ContainerSizeRestriction(in UnityEngine.Vector2 size)
        {
            _size = size;
        }

        public bool Check(IShapePresenter lastShape, IShapePresenter shape, out ResolverStatus status)
        {
            var shapeRectTransform = shape.DraggableRectTransform;
            var shapeHeight = shapeRectTransform.rect.height;
            var shapePositionY = lastShape.Position.y;
            var containerHeight = _size.y;
            shapePositionY += shapeHeight + containerHeight * Constants.Half;

            var check = shapePositionY < containerHeight;

            status = check ? ResolverStatus.Successful : ResolverStatus.ContainerSizeRestriction;

            return check;
        }
    }
}
