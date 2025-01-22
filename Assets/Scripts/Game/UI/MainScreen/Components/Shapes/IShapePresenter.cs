using Cubes.Game.UI.MainScreen.Shapes.Animations;

namespace Cubes.Game.UI.MainScreen.Shapes
{
    internal interface IShapePresenter
    {
        public Configs.ShapeType ShapeType { get; }

        public Configs.ShapeInfo Config { get; }

        public UnityEngine.Vector2 Position { get; }

        public UnityEngine.RectTransform RectTransform { get; }

        public UnityEngine.RectTransform DraggableRectTransform { get; }

        public UnityEngine.RectTransform ScreenRectTransform { get; }

        public UniRx.Subject<DraggableShapeInfo> Dragging { get; }

        public void Init(
            IShapeModel model,
            BaseShapeView view,
            UnityEngine.RectTransform screenRectTransform,
            DragSource dragSource,
            in Configs.ShapeInfo info);

        public void Clone(IShapePresenter clone);

        public void UpdateConfig(in Configs.ShapeInfo config);

        public void UpdatePosition(in UnityEngine.Vector2 position);

        public void UpdateDraggablePosition(UnityEngine.EventSystems.PointerEventData eventData);

        public void UpdateDraggablePosition(in UnityEngine.Vector2 position);

        public void UpdateParent(UnityEngine.Transform parent);

        public void UpdateDraggableParent(UnityEngine.Transform parent);

        public void UpdateDragSource(DragSource dragSource);

        public void Jump(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition);

        public void Move(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition, float delay = 0f);

        public void Explode(in ExplodeAnimationArgs args);

        public void Show();

        public void Hide(HideAnimationType type = HideAnimationType.Force);

        public void Destroy();
    }
}
