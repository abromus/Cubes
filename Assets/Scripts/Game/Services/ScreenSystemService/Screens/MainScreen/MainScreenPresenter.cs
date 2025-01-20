using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class MainScreenPresenter : BaseScreenPresenter
    {
        private MainScreenModel _model;
        private MainScreenView _view;
        private ShapeResolver _resolver;
        private ShapePool _pool;

        private ScreenSystemService _screenSystemService;
        private Factories.ShapeFactory _factory;
        private Configs.IShapesConfig _config;

        private readonly CompositeDisposable _subscriptions = new();

        public override bool IsShown => _model.IsShown.Value;

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct(ScreenSystemService screenSystemService, Factories.ShapeFactory factory, Configs.IShapesConfig config)
        {
            _screenSystemService = screenSystemService;
            _factory = factory;
            _config = config;
        }

        public override void Init(IScreenModel model, BaseScreenView view)
        {
            _model = model as MainScreenModel;
            _view = view as MainScreenView;

            InitializeShapes();
            InitializeResolver();
            InitializePool();
        }

        public override void Show()
        {
            Subscribe();

            _model.UpdateIsShown(true);
        }

        public override void Hide()
        {
            _model.UpdateIsShown(false);

            Unsubscribe();
        }

        public override void Destroy()
        {
            _view.Destroy();
        }

        internal void ResolveShape(BaseDroppedZone zone)
        {
            var draggableShape = _model.DraggableShape;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(draggableShape);
#endif

            if (_view.ContainsInTower(draggableShape) || _resolver.Check(draggableShape) == false)
                return;

            var newShape = _pool.Get(draggableShape.ShapeType);
            newShape.Clone(draggableShape);
            newShape.Show();

            _resolver.AddShape(newShape);
            _view.AddShapeToTower(newShape);
        }

        internal void CheckHole()
        {
            var draggableShape = _model.DraggableShape;

            _pool.Release(draggableShape);
            _resolver.RemoveShape(draggableShape);
            _view.RemoveShapeFromTower(draggableShape);

            draggableShape.Hide();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowSettingsScreen()
        {
            _screenSystemService.Show(Configs.ScreenType.Settings);
        }

        internal void CheckDraggingShape(in World.DraggableShapeInfo info)
        {
            var isDragging = info.IsDragging;
            var shapePresenter = info.ShapePresenter;

            if (isDragging)
            {
                _model.UpdateDraggableShape(shapePresenter);

                shapePresenter.UpdateDraggableParent(_view.DraggingShapeContainer);
            }
            else
            {
                _model.UpdateDraggableShape(null);

                shapePresenter.UpdateDraggableParent(shapePresenter.RectTransform);
            }
        }

        private void InitializeShapes()
        {
            var shapeInfos = _config.ShapeInfos;

            for (int i = 0; i < shapeInfos.Length; i++)
            {
                var info = shapeInfos[i];
                var type = info.Type;

                if (_factory.TryCreate(_view.ShapesContainer, _view.RectTransform, in info, out var shapePresenter) == false)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError($"[MainScreenPresenter]: Can't create shape {type}");
#endif
                }

                shapePresenter.Dragging.Subscribe(OnShapeDragging).AddTo(_subscriptions);

                _model.AddShape(shapePresenter);
            }
        }

        private void InitializeResolver()
        {
            var towerSize = _view.GetTowerSize();

            _resolver = new(in towerSize);
        }

        private void InitializePool()
        {
            var args = new ShapePoolArgs(
                _factory,
                _config,
                _view.TowerShapeContainer,
                _view.RectTransform);

            _pool = new(in args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _model.IsShown.Subscribe(OnUpdateIsShown).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnUpdateIsShown(bool isShown)
        {
            if (isShown)
                _view.Show();
            else
                _view.Hide();
        }

        private void OnShapeDragging(World.DraggableShapeInfo info)
        {
            CheckDraggingShape(in info);
        }
    }
}
