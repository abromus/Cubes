namespace Cubes.Game.Services
{
    internal sealed class ShapePool
    {
        private readonly Factories.ShapeFactory _factory;
        private readonly Configs.IShapesConfig _config;
        private readonly UnityEngine.RectTransform _parent;
        private readonly UnityEngine.RectTransform _screenRectTransform;

        private readonly System.Collections.Generic.Dictionary<Configs.ShapeType, Core.IObjectPool<World.IShapePresenter>> _pools = new(8);

        internal ShapePool(in ShapePoolArgs args)
        {
            _factory = args.Factory;
            _config = args.Config;
            _parent = args.Parent;
            _screenRectTransform = args.ScreenRectTransform;

            InitPools();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal World.IShapePresenter Get(Configs.ShapeType shapeType)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_pools.ContainsKey(shapeType), $"[ShapeResolver]: Type {shapeType} not found");
#endif

            return _pools[shapeType].Get();
        }

        internal void Release(World.IShapePresenter shapePresenter)
        {
            var shapeType = shapePresenter.ShapeType;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_pools.ContainsKey(shapeType), $"[ShapeResolver]: Type {shapeType} not found");
#endif

            _pools[shapeType].Release(shapePresenter);
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

                _pools.Add(shapeType, new Core.ObjectPool<World.IShapePresenter>(() => CreateShape(shapeType)));
            }
        }

        private World.IShapePresenter CreateShape(Configs.ShapeType shapeType)
        {
            if (_factory.TryCreate(_parent, _screenRectTransform, shapeType, out var shapePresenter) == false)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"[ShapePool]: Can't create shape {shapeType}");
#endif
            }

            return shapePresenter;
        }
    }
}
