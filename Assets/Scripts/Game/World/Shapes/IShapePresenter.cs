namespace Cubes.Game.World
{
    internal interface IShapePresenter
    {
        public UnityEngine.RectTransform ScreenRectTransform { get; }

        public UnityEngine.RectTransform DraggableShapeParent { get; }

        public UniRx.Subject<DraggableShapeInfo> Dragging { get; }

        public void Init(IShapeModel model, BaseShapeView view, UnityEngine.RectTransform screenRectTransform);

        public void UpdatePosition(UnityEngine.EventSystems.PointerEventData eventData);

        public void UpdatePosition(in UnityEngine.Vector2 position);

        public void UpdateParent(UnityEngine.Transform parent);

        public void UpdateDraggableParent(UnityEngine.Transform parent);

        public void Destroy();
    }
}
