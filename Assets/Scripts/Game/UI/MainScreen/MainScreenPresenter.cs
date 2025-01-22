using Cubes.Game.UI.MainScreen.Shapes;
using UniRx;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class MainScreenPresenter : Services.BaseScreenPresenter
    {
        private MainScreenModel _model;
        private MainScreenView _view;
        private ShapeResolver _resolver;
        private ShapePool _pool;

        private Services.ScreenSystemService _screenSystemService;
        private Factories.ShapeFactory _factory;
        private Configs.IShapesConfig _config;

        private readonly CompositeDisposable _subscriptions = new();
        private readonly CompositeDisposable _shapeSubscriptions = new();

        public override bool IsShown => _model.IsShown.Value;

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct(Services.ScreenSystemService screenSystemService, Factories.ShapeFactory factory, Configs.IShapesConfig config)
        {
            _screenSystemService = screenSystemService;
            _factory = factory;
            _config = config;
        }

        public override void Init(Services.IScreenModel model, Services.BaseScreenView view)
        {
            _model = model as MainScreenModel;
            _view = view as MainScreenView;

            InitShapes();
            InitResolver();
            InitPool();
        }

        public override void Show()
        {
            Subscribe();

            _model.UpdateIsShown(true);
        }

        public override void Hide()
        {
            if (_model.IsShown.Value == false)
                return;

            _model.UpdateIsShown(false);
            _resolver.Clear();
            _view.Clear();

            Unsubscribe();
        }

        public override void Destroy()
        {
            ClearSubscriptions(_shapeSubscriptions);

            _pool.Destroy();
            _view.Destroy();
        }

        internal ResolverStatus ResolveShape()
        {
            var status = ResolverStatus.None;
            var draggableShape = _model.DraggableShape;

            if (draggableShape == null || _view.ContainsInTower(draggableShape) || _resolver.Check(draggableShape, out status) == false)
            {
                if (status == ResolverStatus.IntersectionRestriction)
                    ExplodeShape(draggableShape);

                return status;
            }

            AddToTower(draggableShape);

            status = ResolverStatus.Successful;

            return status;
        }

        internal void RemoveShape(IShapePresenter shape)
        {
            _pool.Release(shape);

            shape.Hide();
        }

        internal bool CheckHole()
        {
            if (_model.DragSource != DragSource.FromTower)
                return false;

            var draggableShape = _model.DraggableShape;

            _pool.Release(draggableShape);
            _resolver.RemoveShape(draggableShape);
            _view.RemoveShapeFromTower(draggableShape);

            draggableShape.Hide();

            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowSettingsScreen()
        {
            _screenSystemService.Show(Configs.ScreenType.Settings);
        }

        internal void CheckDraggingShape(in DraggableShapeInfo info)
        {
            var isDragging = info.IsDragging;
            var shapePresenter = info.ShapePresenter;
            var dragSource = info.DragSource;

            if (isDragging)
            {
                _model.UpdateDraggableShape(shapePresenter, dragSource);

                shapePresenter.UpdateDraggableParent(_view.DraggingShapeContainer);
            }
            else
            {
                _model.UpdateDraggableShape(null, dragSource);

                shapePresenter.UpdateDraggableParent(shapePresenter.RectTransform);
            }
        }

        private void InitShapes()
        {
            var shapeInfos = _config.ShapeInfos;
            var dragSource = DragSource.FromStorage;

            for (int i = 0; i < shapeInfos.Length; i++)
            {
                var info = shapeInfos[i];
                var type = info.Type;

                if (_factory.TryCreate(_view.ShapesContainer, _view.RectTransform, in info, dragSource, out var shapePresenter) == false)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError($"[MainScreenPresenter]: Can't create shape {type}");
#endif
                }

                shapePresenter.Dragging.Subscribe(OnShapeDragging).AddTo(_shapeSubscriptions);

                _model.AddShape(shapePresenter);
            }
        }

        private void InitResolver()
        {
            var towerSize = _view.GetTowerSize();

            _resolver = new(in towerSize);
        }

        private void InitPool()
        {
            var args = new ShapePoolArgs(
                _factory,
                _config,
                _view.TowerShapeContainer,
                _view.RectTransform);

            _pool = new(in args);
        }

        private void AddToTower(IShapePresenter shape)
        {
            var startPosition = GetStartPosition(shape);
            var newShape = _pool.Get(shape.ShapeType);
            newShape.Clone(shape);
            newShape.Show();

            _resolver.AddShape(newShape);
            _view.AddShapeToTower(newShape, in startPosition);
        }

        private void ExplodeShape(IShapePresenter shape)
        {
            var startPosition = GetStartPosition(shape);
            var newShape = _pool.Get(shape.ShapeType);
            newShape.Clone(shape);
            newShape.Show();

            _view.ExplodeShape(newShape, in startPosition, AfterExplodeShape);

            void AfterExplodeShape()
            {
                RemoveShape(newShape);
            }
        }

        private UnityEngine.Vector2 GetStartPosition(IShapePresenter shape)
        {
            var screenPoint = UnityEngine.RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, shape.RectTransform.position);

            UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _view.TowerShapeContainer,
                screenPoint,
                UnityEngine.Camera.main,
                out var startPosition);

            return startPosition;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _model.IsShown.Subscribe(OnUpdateIsShown).AddTo(_subscriptions);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            ClearSubscriptions(_subscriptions);
        }

        private void ClearSubscriptions(CompositeDisposable subscriptions)
        {
            foreach (var subscription in subscriptions)
                subscription.Dispose();

            subscriptions.Clear();
        }

        private void OnUpdateIsShown(bool isShown)
        {
            if (isShown)
                _view.Show();
            else
                _view.Hide();
        }

        private void OnShapeDragging(DraggableShapeInfo info)
        {
            CheckDraggingShape(in info);
        }
    }
}
