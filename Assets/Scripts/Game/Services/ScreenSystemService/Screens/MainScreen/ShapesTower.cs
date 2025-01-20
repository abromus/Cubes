namespace Cubes.Game.Services
{
    internal sealed class ShapesTower : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _startPosition;

        private float _halfWidth;
        private float _startPositionY;
        private UnityEngine.Vector2 _shapePosition;

        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);

        internal UnityEngine.RectTransform ShapeContainer => _rectTransform;

        private void Awake()
        {
            _halfWidth = _rectTransform.rect.width * Constants.Half;
            _startPositionY = _startPosition.anchoredPosition.y;
            _shapePosition = UnityEngine.Vector2.zero;
        }

        internal void Add(World.IShapePresenter shape)
        {
            shape.UpdateParent(_rectTransform);

            var position = GetShapePosition(shape);
            shape.UpdatePosition(in position);

            _shapes.Add(shape);
        }

        internal UnityEngine.Vector2 GetAvailableSize()
        {
            var rect = _rectTransform.rect;
            var size = new UnityEngine.Vector2(rect.size.x, rect.height * Constants.Half + _rectTransform.anchoredPosition.y - _startPositionY);

            return size;
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
    }
}
