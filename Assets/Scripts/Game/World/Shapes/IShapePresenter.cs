namespace Cubes.Game.World
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

        public void Init(IShapeModel model, BaseShapeView view, UnityEngine.RectTransform screenRectTransform, in Configs.ShapeInfo info);

        public void Clone(IShapePresenter clone);

        public void UpdatePosition(in UnityEngine.Vector2 position);

        public void UpdateDraggablePosition(UnityEngine.EventSystems.PointerEventData eventData);

        public void UpdateDraggablePosition(in UnityEngine.Vector2 position);

        public void UpdateParent(UnityEngine.Transform parent);

        public void UpdateDraggableParent(UnityEngine.Transform parent);

        public void Destroy();
    }
}
