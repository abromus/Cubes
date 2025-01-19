namespace Cubes.Game.World
{
    internal abstract class BaseShapePresenter : IShapePresenter
    {
        public abstract UnityEngine.Vector2 Position { get; }

        public abstract UnityEngine.RectTransform RectTransform { get; }

        public abstract UnityEngine.RectTransform DraggableRectTransform { get; }

        public abstract UnityEngine.RectTransform ScreenRectTransform { get; }

        public abstract UniRx.Subject<DraggableShapeInfo> Dragging { get; }

        public abstract void Init(IShapeModel model, BaseShapeView view, UnityEngine.RectTransform screenRectTransform);

        public abstract void UpdatePosition(in UnityEngine.Vector2 position);

        public abstract void UpdateDraggablePosition(UnityEngine.EventSystems.PointerEventData eventData);

        public abstract void UpdateDraggablePosition(in UnityEngine.Vector2 position);

        public abstract void UpdateParent(UnityEngine.Transform parent);

        public abstract void UpdateDraggableParent(UnityEngine.Transform parent);

        public abstract void Destroy();
    }
}
