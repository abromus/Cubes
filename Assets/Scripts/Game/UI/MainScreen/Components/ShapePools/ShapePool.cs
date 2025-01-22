using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class ShapePool
    {
        private readonly Factories.ShapeFactory _factory;
        private readonly Configs.IShapesConfig _config;
        private readonly UnityEngine.RectTransform _parent;
        private readonly UnityEngine.RectTransform _screenRectTransform;

        private readonly System.Collections.Generic.Dictionary<Configs.ShapeType, Core.IObjectPool<IShapePresenter>> _pools = new(8);
        private readonly System.Collections.Generic.Dictionary<Configs.ShapeType, System.Collections.Generic.List<IShapePresenter>> _shapes = new(8);

        internal ShapePool(in ShapePoolArgs args)
        {
            _factory = args.Factory;
            _config = args.Config;
            _parent = args.Parent;
            _screenRectTransform = args.ScreenRectTransform;

            InitPools();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal IShapePresenter Get(Configs.ShapeType shapeType)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_pools.ContainsKey(shapeType), $"[ShapeResolver]: Type {shapeType} not found");
#endif

            var shapePresenter = _pools[shapeType].Get();

            if (_shapes.ContainsKey(shapeType))
                _shapes[shapeType].Add(shapePresenter);
            else
                _shapes.Add(shapeType, new(32) { shapePresenter });

            return shapePresenter;
        }

        internal void Release(IShapePresenter shapePresenter)
        {
            var shapeType = shapePresenter.ShapeType;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_pools.ContainsKey(shapeType), $"[ShapeResolver]: Type {shapeType} not found");
#endif

            _pools[shapeType].Release(shapePresenter);
            _shapes[shapeType].Remove(shapePresenter);
        }

        internal void Destroy()
        {
            var shapes = _shapes.Values;

            foreach (var currentShapes in shapes)
            {
                var count = currentShapes.Count;

                for (int i = count - 1; 0 < i + 1; i--)
                {
                    var shape = currentShapes[i];
                    var shapeType = shape.ShapeType;
                    shape.Destroy();

                    _pools[shapeType].Release(shape);
                    _shapes[shapeType].Remove(shape);
                }
            }
        }

        private void InitPools()
        {
            var shapeTypeInfos = _config.ShapeTypeInfos;

            for (int i = 0; i < shapeTypeInfos.Length; i++)
            {
                var shapeTypeInfo = shapeTypeInfos[i];
                var prefab = shapeTypeInfo.Prefab;

                if (prefab == null)
                    continue;

                var shapeType = prefab.ShapeType;

                if (_pools.ContainsKey(shapeType))
                    continue;

                _pools.Add(shapeType, new Core.ObjectPool<IShapePresenter>(() => CreateShape(shapeType)));
            }
        }

        private IShapePresenter CreateShape(Configs.ShapeType shapeType)
        {
            var dragSource = DragSource.FromTower;

            if (_factory.TryCreate(_parent, _screenRectTransform, shapeType, dragSource, out var shapePresenter) == false)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"[ShapePool]: Can't create shape {shapeType}");
#endif
            }

            return shapePresenter;
        }
    }
}
