namespace Cubes.Game.Services
{
    internal sealed class ShapeResolver
    {
        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);
        private readonly UnityEngine.Vector3[] _firstShapeCorners = new UnityEngine.Vector3[4];
        private readonly UnityEngine.Vector3[] _secondShapeCorners = new UnityEngine.Vector3[4];

        internal bool TryAddShape(World.IShapePresenter shape)
        {
            if (_shapes.Count == 0)
            {
                _shapes.Add(shape);

                return true;
            }

            if (_shapes.Contains(shape) || CanAddShape(shape) == false)
                return false;

            _shapes.Add(shape);

            return true;
        }

        private bool CanAddShape(World.IShapePresenter shape)
        {
            var minShapes = 1;

            if (_shapes.Count < minShapes)
                return false;

            var lastShape = _shapes[^1];

            return IsShapesIntersected(lastShape, shape);
        }

        private bool IsShapesIntersected(World.IShapePresenter firstShape, World.IShapePresenter secondShape)
        {
            firstShape.RectTransform.GetWorldCorners(_firstShapeCorners);
            secondShape.DraggableRectTransform.GetWorldCorners(_secondShapeCorners);

            return IsRectIntersecting(_firstShapeCorners, _secondShapeCorners);
        }

        private bool IsRectIntersecting(UnityEngine.Vector3[] firstShapeCorners, UnityEngine.Vector3[] secondShapeCorners)
        {
            var minXIndex = 0;
            var maxXIndex = 2;

            return (firstShapeCorners[maxXIndex].x < secondShapeCorners[minXIndex].x || secondShapeCorners[maxXIndex].x < firstShapeCorners[minXIndex].x) == false;
        }
    }
}
