namespace Cubes.Game.Services
{
    internal sealed class ContainerSizeRestriction : IRestriction
    {
        private readonly UnityEngine.Vector2 _size;

        internal ContainerSizeRestriction(in UnityEngine.Vector2 size)
        {
            _size = size;
        }

        public bool Check(World.IShapePresenter lastShape, World.IShapePresenter shape)
        {
            var shapeRectTransform = shape.DraggableRectTransform;
            var shapeHeight = shapeRectTransform.rect.height;
            var shapePositionY = lastShape.RectTransform.anchoredPosition.y;
            var containerHeight = _size.y;
            shapePositionY += shapeHeight + containerHeight * Constants.Half;

            return shapePositionY < containerHeight;
        }
    }
}
