﻿using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class ShapesTower : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _freeShapesContainer;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _startPosition;
        [UnityEngine.SerializeField] private float _moveDelay;

        private float _halfWidth;
        private float _startPositionY;
        private UnityEngine.Vector2 _shapePosition;

        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);
        private readonly System.Collections.Generic.Dictionary<World.IShapePresenter, System.IDisposable> _subscriptions = new(32);
        private readonly Subject<World.DraggableShapeInfo> _dragging = new();

        internal UnityEngine.RectTransform ShapeContainer => _rectTransform;

        internal Subject<World.DraggableShapeInfo> Dragging => _dragging;

        internal void Init()
        {
            _halfWidth = _rectTransform.rect.width * Constants.Half;
            _startPositionY = _startPosition.anchoredPosition.y;
            _shapePosition = UnityEngine.Vector2.zero;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool Contains(World.IShapePresenter shape)
        {
            return _shapes.Contains(shape);
        }

        internal void Add(World.IShapePresenter shape, in UnityEngine.Vector2 startPosition)
        {
            shape.UpdateParent(_rectTransform);

            var position = GetShapePosition(shape);
            shape.UpdatePosition(in position);
            shape.Jump(in startPosition, in position);

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

        internal void Explode(World.IShapePresenter shape, in UnityEngine.Vector2 startPosition, System.Action callback)
        {
            shape.UpdateParent(_rectTransform);

            var targetMovePosition = GetTargetMovePosition(shape);
            var targetFallPosition = new UnityEngine.Vector2(targetMovePosition.x, _startPositionY);
            var args = new World.ExplodeAnimationArgs(
                in startPosition,
                in targetMovePosition,
                in targetFallPosition,
                callback);

            shape.UpdatePosition(in _shapePosition);
            shape.Explode(in args);

            UnityEngine.Vector2 GetTargetMovePosition(World.IShapePresenter shape)
            {
                var screenPoint = UnityEngine.RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, shape.RectTransform.position);

                UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform,
                    screenPoint,
                    UnityEngine.Camera.main,
                    out var targetMovePosition);

                targetMovePosition.x -= _halfWidth;

                return targetMovePosition;
            }
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
            var parentRect = _rectTransform.rect;
            var shapeRect = shape.DraggableRectTransform.rect;
            var shapeHeight = shapeRect.height;
            var halfShapeWidth = shapeRect.width * Constants.Half;

            if (_shapes.Count == 0)
            {
                _shapePosition.x = UnityEngine.Mathf.Clamp(
                    shape.Position.x - _halfWidth,
                    parentRect.xMin + halfShapeWidth,
                    parentRect.xMax - halfShapeWidth);
                _shapePosition.y = _startPositionY + shapeHeight * Constants.Half;

                return _shapePosition;
            }

            var lastShape = _shapes[^1];

            _shapePosition = lastShape.Position;
            _shapePosition.x = UnityEngine.Mathf.Clamp(
                _shapePosition.x + UnityEngine.Random.Range(-halfShapeWidth, halfShapeWidth),
                    parentRect.xMin + halfShapeWidth,
                    parentRect.xMax - halfShapeWidth);
            _shapePosition.y += shapeHeight;

            return _shapePosition;
        }

        private void MoveShapes(World.IShapePresenter shape)
        {
            var index = GetShapeIndex(shape) + 1;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(-1 < index);
#endif

            var shapesCount = _shapes.Count - 1;
            var shapesLeft = shapesCount - index;

            for (int i = shapesCount; i >= index; i--)
            {
                var currentShape = _shapes[i];
                var nextShape = _shapes[i - 1];
                var startPosition = currentShape.Position;
                var targetPosition = new UnityEngine.Vector2(currentShape.Position.x, nextShape.Position.y);
                var delay = shapesLeft * _moveDelay;
                --shapesLeft;

                currentShape.UpdatePosition(in targetPosition);
                currentShape.Move(in startPosition, in targetPosition, delay);
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
