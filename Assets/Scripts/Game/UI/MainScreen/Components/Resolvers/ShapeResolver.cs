using Cubes.Game.UI.MainScreen.Restrictions;
using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class ShapeResolver
    {
        private readonly System.Collections.Generic.List<IRestriction> _restrictions = new(4);
        private readonly System.Collections.Generic.List<IShapePresenter> _shapes = new(32);

        internal ShapeResolver(in UnityEngine.Vector2 containerSize)
        {
            _restrictions.Add(new IntersectionRestriction());
            _restrictions.Add(new ContainerSizeRestriction(in containerSize));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool Check(IShapePresenter shape, out ResolverStatus status)
        {
            status = ResolverStatus.None;

            return _shapes.Count == 0 || CheckRestrictions(shape, out status);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddShape(IShapePresenter shape)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(_shapes.Contains(shape), $"[ShapeResolver]: Already contains shape {shape}");
#endif

            _shapes.Add(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void RemoveShape(IShapePresenter shape)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_shapes.Contains(shape), $"[ShapeResolver]: Shape {shape} isn't contained");
#endif

            _shapes.Remove(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Clear()
        {
            _shapes.Clear();
        }

        private bool CheckRestrictions(IShapePresenter shape, out ResolverStatus status)
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
