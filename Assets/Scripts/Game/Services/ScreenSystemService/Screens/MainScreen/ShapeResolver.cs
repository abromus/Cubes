namespace Cubes.Game.Services
{
    internal sealed class ShapeResolver
    {
        private readonly System.Collections.Generic.List<IRestriction> _restrictions = new(4);
        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);

        internal ShapeResolver(in UnityEngine.Vector2 containerSize)
        {
            _restrictions.Add(new IntersectionRestriction());
            _restrictions.Add(new ContainerSizeRestriction(in containerSize));
        }

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

            for (int i = 0; i < _restrictions.Count; i++)
            {
                var restriction = _restrictions[i];
                
                if (restriction.Check(lastShape, shape) == false)
                    return false;
            }

            return true;
        }
    }
}
