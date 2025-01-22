using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cubes.Game.UI.MainScreen.Shapes.Animations;

namespace Cubes.Game.UI.MainScreen.Shapes
{
    [System.Serializable]
    internal abstract class ShapeView : BaseShapeView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UnityEngine.UI.Image _image;
        [SerializeField] private UnityEngine.UI.Image _draggableImage;
        [SerializeField] private RectTransform _draggableRectTransform;
        [SerializeField][Range(0f, 1f)] private float _beginDragAlpha;
        [SerializeField][Range(0f, 1f)] private float _endDragAlpha;
        [SerializeField] private float _fadeOutDuration;
        [SerializeField] private Animations.ShapeAnimator _animator;

        private bool _isDragging = false;
        private Vector2 _anchorMin;
        private Vector2 _anchorMax;

        private readonly UniRx.Subject<bool> _dragging = new();
        private readonly Vector2 _zero = Vector2.zero;

        internal RectTransform RectTransform => _rectTransform;

        internal RectTransform DraggableRectTransform => _draggableRectTransform;

        internal UniRx.Subject<bool> Dragging => _dragging;

        public override void Init(IShapePresenter presenter)
        {
            base.Init(presenter);

            _anchorMin = _rectTransform.anchorMin;
            _anchorMax = _rectTransform.anchorMax;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateConfig(in Configs.ShapeInfo info)
        {
            _image.color = info.Color;
            _draggableImage.color = info.Color;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdatePosition(in Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateDraggablePosition(in Vector2 position)
        {
            _draggableRectTransform.anchoredPosition = position;
        }

        public void UpdateParent(Transform parent)
        {
            _rectTransform.SetParent(parent);
            _rectTransform.anchorMin = _anchorMin;
            _rectTransform.anchorMax = _anchorMax;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateDraggableParent(Transform parent)
        {
            _draggableRectTransform.SetParent(parent);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Jump(in Vector2 startPosition, in Vector2 targetPosition)
        {
            _animator.Jump(in startPosition, in targetPosition);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Move(in Vector2 startPosition, in Vector2 targetPosition, float delay)
        {
            _animator.Move(in startPosition, in targetPosition, delay);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Explode(in ExplodeAnimationArgs args)
        {
            _animator.Explode(in args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Hide()
        {
            _animator.Stop();

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FadeOut()
        {
            _animator.Stop();

            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, _fadeOutDuration))
                .OnComplete(Hide);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            ChangeAlpha(_beginDragAlpha);

            _dragging.OnNext(_isDragging);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
                return;

            Presenter.UpdateDraggablePosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            ChangeAlpha(_endDragAlpha);

            _dragging.OnNext(_isDragging);

            Presenter.UpdateDraggablePosition(in _zero);
        }

        private void ChangeAlpha(float alpha)
        {
            var color = _draggableImage.color;
            color.a = alpha;
            _draggableImage.color = color;
        }
    }
}
