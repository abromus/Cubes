using Cubes.Game.World;

namespace Cubes.Game.Factories
{
    internal sealed class ShapeFactory
    {
        [Zenject.Inject] private readonly Zenject.DiContainer _diContainer;
        [Zenject.Inject] private readonly Configs.ShapesConfig _shapesConfig;

        private readonly System.Collections.Generic.Dictionary<Configs.ShapeType, BaseShapeView> _viewPrefabs = new(8);

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            var shapeTypeInfos = _shapesConfig.ShapeTypeInfos;

            for (int i = 0; i < shapeTypeInfos.Length; i++)
            {
                var shapeTypeInfo = shapeTypeInfos[i];

                _viewPrefabs.Add(shapeTypeInfo.Type, shapeTypeInfo.Prefab);
            }
        }

        internal bool TryCreate(
            UnityEngine.RectTransform parent,
            UnityEngine.RectTransform screenRectTransform,
            in Configs.ShapeInfo info,
            out IShapePresenter presenter)
        {
            presenter = null;

            var type = info.Type;

            if (_viewPrefabs.ContainsKey(type) == false)
                return false;

            var viewPrefab = _viewPrefabs[type];

            switch (type)
            {
                case Configs.ShapeType.Cube:
                    presenter = Create<CubeModel, CubeView, CubePresenter>(viewPrefab, parent, screenRectTransform, in info);

                    return true;
            }

            return false;
        }

        internal bool TryCreate(
            UnityEngine.RectTransform parent,
            UnityEngine.RectTransform screenRectTransform,
            Configs.ShapeType type,
            out IShapePresenter presenter)
        {
            presenter = null;

            if (_viewPrefabs.ContainsKey(type) == false)
                return false;

            var viewPrefab = _viewPrefabs[type];

            switch (type)
            {
                case Configs.ShapeType.Cube:
                    presenter = Create<CubeModel, CubeView, CubePresenter>(viewPrefab, parent, screenRectTransform);

                    return true;
            }

            return false;
        }

        private IShapePresenter Create<TModel, TView, TPresenter>(
            BaseShapeView viewPrefab,
            UnityEngine.Transform parent,
            UnityEngine.RectTransform screenRectTransform,
            in Configs.ShapeInfo info = default)
            where TModel : IShapeModel, new()
            where TView : BaseShapeView
            where TPresenter : IShapePresenter, new()
        {
            var model = new TModel();
            var view = _diContainer.InstantiatePrefabForComponent<BaseShapeView>(viewPrefab, parent);
            var presenter = _diContainer.Resolve<TPresenter>();

            presenter.Init(model, view, screenRectTransform, in info);
            view.Init(presenter);
            view.UpdateConfig(in info);

            return presenter;
        }
    }
}
