using Cubes.Game.Configs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cubes.Game.World
{
    [System.Serializable]
    internal abstract class ShapeView : BaseShapeView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEngine.UI.Image _draggableImage;
        [SerializeField] private RectTransform _draggableImageRectTransform;

        private bool _isDragging = false;
        private Vector2 _anchorMin;
        private Vector2 _anchorMax;

        private readonly UniRx.Subject<bool> _dragging = new();

        internal RectTransform DraggableShapeParent => _rectTransform;

        internal UniRx.Subject<bool> Dragging => _dragging;

        public override void Init(IShapePresenter presenter, in ShapeInfo info)
        {
            base.Init(presenter, info);

            _anchorMin = _rectTransform.anchorMin;
            _anchorMax = _rectTransform.anchorMax;

            _draggableImage.color = info.Color;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateDragPosition(in Vector2 position)
        {
            _draggableImageRectTransform.anchoredPosition = position;
        }

        public void UpdateParent(Transform parent)
        {
            _rectTransform.SetParent(parent);
            _rectTransform.anchorMin = _anchorMin;
            _rectTransform.anchorMax = _anchorMax;
            _rectTransform.anchoredPosition = Vector2.zero;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateDraggableParent(Transform parent)
        {
            _draggableImageRectTransform.SetParent(parent);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            _dragging.OnNext(_isDragging);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
                return;

            Presenter.UpdatePosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            _dragging.OnNext(_isDragging);

            Presenter.UpdatePosition(Vector2.zero);

            _draggableImageRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
