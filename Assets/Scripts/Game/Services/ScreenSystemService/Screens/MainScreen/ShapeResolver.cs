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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool Check(World.IShapePresenter shape, out ResolverStatus status)
        {
            status = ResolverStatus.None;

            return _shapes.Count == 0 || CheckRestrictions(shape, out status);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddShape(World.IShapePresenter shape)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(_shapes.Contains(shape), $"[ShapeResolver]: Already contains shape {shape}");
#endif

            _shapes.Add(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void RemoveShape(World.IShapePresenter shape)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_shapes.Contains(shape), $"[ShapeResolver]: Shape {shape} isn't contained");
#endif

            _shapes.Remove(shape);
        }

        private bool CheckRestrictions(World.IShapePresenter shape, out ResolverStatus status)
        {
            status = ResolverStatus.Successful;

            var minShapes = 1;

            if (_shapes.Count < minShapes)
                return false;

            var lastShape = _shapes[^1];

            for (int i = 0; i < _restrictions.Count; i++)
            {
                var restriction = _restrictions[i];

                if (restriction.Check(lastShape, shape, out status) == false)
                    return false;
            }

            return true;
        }
    }
}
