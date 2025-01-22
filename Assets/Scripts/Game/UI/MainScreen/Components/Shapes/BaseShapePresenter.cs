using Cubes.Game.UI.MainScreen.Shapes.Animations;

namespace Cubes.Game.UI.MainScreen.Shapes
{
    internal abstract class BaseShapePresenter : IShapePresenter
    {
        public abstract Configs.ShapeType ShapeType { get; }

        public abstract Configs.ShapeInfo Config { get; }

        public abstract UnityEngine.Vector2 Position { get; }

        public abstract UnityEngine.RectTransform RectTransform { get; }

        public abstract UnityEngine.RectTransform DraggableRectTransform { get; }

        public abstract UnityEngine.RectTransform ScreenRectTransform { get; }

        public abstract UniRx.Subject<DraggableShapeInfo> Dragging { get; }

        public abstract void Init(
            IShapeModel model,
            BaseShapeView view,
            UnityEngine.RectTransform screenRectTransform,
            DragSource dragSource,
            in Configs.ShapeInfo info);

        public abstract void Clone(IShapePresenter clone);

        public abstract void UpdatePosition(in UnityEngine.Vector2 position);

        public abstract void UpdateDraggablePosition(UnityEngine.EventSystems.PointerEventData eventData);

        public abstract void UpdateDraggablePosition(in UnityEngine.Vector2 position);

        public abstract void UpdateParent(UnityEngine.Transform parent);

        public abstract void UpdateDraggableParent(UnityEngine.Transform parent);

        public abstract void UpdateDragSource(DragSource dragSource);

        public abstract void Jump(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition);

        public abstract void Move(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition, float delay = 0f);

        public abstract void Explode(in ExplodeAnimationArgs args);

        public abstract void Show();

        public abstract void Hide(HideAnimationType type = HideAnimationType.Force);

        public abstract void Destroy();
    }
}
