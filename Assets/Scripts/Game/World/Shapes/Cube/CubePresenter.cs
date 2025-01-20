using UniRx;

namespace Cubes.Game.World
{
    internal sealed class CubePresenter : BaseShapePresenter
    {
        private CubeModel _model;
        private CubeView _view;
        private UnityEngine.RectTransform _screenRectTransform;
        private Configs.ShapeInfo _config;
        private DraggableShapeInfo _draggableShapeInfo;

        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<DraggableShapeInfo> _dragging = new();

        public override Configs.ShapeInfo Config => _config;

        public override Configs.ShapeType ShapeType => _view.ShapeType;

        public override UnityEngine.Vector2 Position => _model.Position.Value;

        public override UnityEngine.RectTransform RectTransform => _view.RectTransform;

        public override UnityEngine.RectTransform DraggableRectTransform => _view.DraggableRectTransform;

        public override UnityEngine.RectTransform ScreenRectTransform => _screenRectTransform;

        public override Subject<DraggableShapeInfo> Dragging => _dragging;

        public override void Init(IShapeModel model, BaseShapeView view, UnityEngine.RectTransform screenRectTransform, in Configs.ShapeInfo info)
        {
            _model = model as CubeModel;
            _view = view as CubeView;
            _screenRectTransform = screenRectTransform;
            _config = info;

            _draggableShapeInfo = new DraggableShapeInfo(this, false);

            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Clone(IShapePresenter clone)
        {
            _config = clone.Config;

            var position = clone.DraggableRectTransform.anchoredPosition;

            _model.UpdatePosition(in position);
            _view.UpdateConfig(_config);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdatePosition(in UnityEngine.Vector2 position)
        {
            _model.UpdatePosition(in position);
        }

        public override void UpdateDraggablePosition(UnityEngine.EventSystems.PointerEventData eventData)
        {
            UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _screenRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out UnityEngine.Vector2 position);

            _model.UpdateDraggablePosition(in position);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateDraggablePosition(in UnityEngine.Vector2 position)
        {
            _model.UpdateDraggablePosition(in position);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateParent(UnityEngine.Transform parent)
        {
            _view.UpdateParent(parent);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateDraggableParent(UnityEngine.Transform parent)
        {
            _view.UpdateDraggableParent(parent);
        }

        public override void Destroy()
        {
            _view.Destroy();

            Unsubscribe();
        }

        private void Subscribe()
        {
            _model.Position.Subscribe(OnUpdatePosition).AddTo(_subscriptions);
            _model.DraggablePosition.Subscribe(OnUpdateDraggablePosition).AddTo(_subscriptions);
            _view.Dragging.Subscribe(OnDragging).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnUpdatePosition(UnityEngine.Vector2 position)
        {
            _view.UpdatePosition(in position);
        }

        private void OnUpdateDraggablePosition(UnityEngine.Vector2 position)
        {
            _view.UpdateDraggablePosition(in position);
        }

        private void OnDragging(bool isDragging)
        {
            _draggableShapeInfo.SetDragging(isDragging);
            _dragging.OnNext(_draggableShapeInfo);
        }
    }
}
