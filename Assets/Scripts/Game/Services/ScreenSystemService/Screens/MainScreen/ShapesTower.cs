namespace Cubes.Game.Services
{
    internal sealed class ShapesTower : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _startPosition;

        private float _startPositionY;

        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);

        private void Awake()
        {
            _startPositionY = _startPosition.anchoredPosition.y;
        }

        internal void Add(World.IShapePresenter shape)
        {
            shape.UpdateParent(_rectTransform);

            var position = GetShapePosition(shape);
            shape.UpdatePosition(in position);

            _shapes.Add(shape);
        }

        private UnityEngine.Vector2 GetShapePosition(World.IShapePresenter shape)
        {
            var shapeRectTransform = shape.DraggableRectTransform;
            var shapeHeight = shapeRectTransform.rect.height;

            if (_shapes.Count == 0)
            {
                var screenPoint = UnityEngine.RectTransformUtility.WorldToScreenPoint(null, shape.DraggableRectTransform.position);

                UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPoint, null, out var position);

                position.y = _startPositionY + shapeHeight * 0.5f;

                return position;
            }

            var lastShape = _shapes[^1];
            var shapePosition = lastShape.RectTransform.anchoredPosition;
            shapePosition.y += shapeHeight;

            return shapePosition;
        }
    }
}
