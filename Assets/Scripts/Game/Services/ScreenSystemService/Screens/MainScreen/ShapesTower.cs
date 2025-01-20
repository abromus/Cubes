using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class ShapesTower : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _freeShapesContainer;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _startPosition;

        private UnityEngine.RectTransform _draggingShapeContainer;
        private float _halfWidth;
        private float _startPositionY;
        private UnityEngine.Vector2 _shapePosition;

        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);
        private readonly System.Collections.Generic.Dictionary<World.IShapePresenter, System.IDisposable> _subscriptions = new(32);
        private readonly Subject<World.DraggableShapeInfo> _dragging = new();

        internal UnityEngine.RectTransform ShapeContainer => _rectTransform;

        internal Subject<World.DraggableShapeInfo> Dragging => _dragging;

        internal void Init(UnityEngine.RectTransform draggingShapeContainer)
        {
            _draggingShapeContainer = draggingShapeContainer;

            _halfWidth = _rectTransform.rect.width * Constants.Half;
            _startPositionY = _startPosition.anchoredPosition.y;
            _shapePosition = UnityEngine.Vector2.zero;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool Contains(World.IShapePresenter shape)
        {
            return _shapes.Contains(shape);
        }

        internal void Add(World.IShapePresenter shape)
        {
            shape.UpdateParent(_rectTransform);

            var position = GetShapePosition(shape);
            shape.UpdatePosition(in position);

            _subscriptions[shape] = shape.Dragging.Subscribe(OnShapeDragging);
            _shapes.Add(shape);
        }

        internal void Remove(World.IShapePresenter shape)
        {
            shape.UpdateParent(_freeShapesContainer);
            shape.UpdateDraggableParent(shape.RectTransform);

            var position = UnityEngine.Vector2.zero;
            shape.UpdateDraggablePosition(in position);

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_subscriptions.ContainsKey(shape));
#endif
            var subscription = _subscriptions[shape];
            subscription.Dispose();

            _subscriptions.Remove(shape);

            MoveShapes(shape);

            _shapes.Remove(shape);
        }

        internal UnityEngine.Vector2 GetAvailableSize()
        {
            var rect = _rectTransform.rect;
            var size = new UnityEngine.Vector2(rect.size.x, rect.height - (rect.height * Constants.Half - UnityEngine.Mathf.Abs(_startPositionY)));

            return size;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Destroy()
        {
            Unsubscribe();
        }

        private UnityEngine.Vector2 GetShapePosition(World.IShapePresenter shape)
        {
            var shapeRect = shape.DraggableRectTransform.rect;
            var shapeHeight = shapeRect.height;
            var halfShapeWidth = shapeRect.width * Constants.Half;

            if (_shapes.Count == 0)
            {
                _shapePosition.x = shape.RectTransform.anchoredPosition.x - _halfWidth;
                _shapePosition.y = _startPositionY + shapeHeight * Constants.Half;

                return _shapePosition;
            }

            var lastShape = _shapes[^1];

            _shapePosition = lastShape.RectTransform.anchoredPosition;
            _shapePosition.x += UnityEngine.Random.Range(-halfShapeWidth, halfShapeWidth);
            _shapePosition.y += shapeHeight;

            return _shapePosition;
        }

        private void MoveShapes(World.IShapePresenter shape)
        {
            var index = GetShapeIndex(shape) + 1;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(-1 < index);
#endif

            for (int i = _shapes.Count - 1; i >= index; i--)
            {
                var currentShape = _shapes[i];
                var nextShape = _shapes[i - 1];
                var position = new UnityEngine.Vector2(currentShape.Position.x, nextShape.Position.y);

                currentShape.UpdatePosition(in position);
            }

            int GetShapeIndex(World.IShapePresenter shape)
            {
                for (int i = 0; i < _shapes.Count; i++)
                    if (_shapes[i] == shape)
                        return i;

                return -1;
            }
        }

        private void Unsubscribe()
        {
            var subscriptions = _subscriptions.Values;

            foreach (var subscription in subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnShapeDragging(World.DraggableShapeInfo info)
        {
            _dragging.OnNext(info);
        }
    }
}
