﻿using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class MainScreenPresenter : BaseScreenPresenter
    {
        private MainScreenModel _model;
        private MainScreenView _view;
        private ShapeResolver _resolver;

        private ScreenSystemService _screenSystemService;
        private Factories.ShapeFactory _factory;
        private Configs.ShapesConfig _config;

        private readonly CompositeDisposable _subscriptions = new();

        public override bool IsShown => _model.IsShown.Value;

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct(ScreenSystemService screenSystemService, Factories.ShapeFactory factory, Configs.ShapesConfig config)
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

        internal void ResolveShape(DroppedZone zone)
        {
            var draggableShape = _model.DraggableShape;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(draggableShape);
#endif

            if (_resolver.TryAddShape(draggableShape) == false)
                return;

            _view.AddShapeToTower(_model.DraggableShape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowSettingsScreen()
        {
            _screenSystemService.Show(Configs.ScreenType.Settings);
        }

        private void InitializeShapes()
        {
            var shapeInfos = _config.ShapeInfos;

            for (int i = 0; i < shapeInfos.Length; i++)
            {
                var info = shapeInfos[i];
                var type = info.Type;

                if (_factory.TryCreate(in info, _view.ShapesContainer, _view.RectTransform, out var shapePresenter) == false)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError($"[ScreenSystemService]: Can't create shape {type}");
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
    }
}
