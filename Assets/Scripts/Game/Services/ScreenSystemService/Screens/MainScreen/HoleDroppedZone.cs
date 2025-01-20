namespace Cubes.Game.Services
{
    internal sealed class HoleDroppedZone : BaseDroppedZone
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.Vector3 _offset;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;
        [UnityEngine.SerializeField] private int _segments;

        private UnityEngine.Vector3 _center;

        private readonly UniRx.Subject<BaseDroppedZone> _dropped = new();

        internal override UniRx.Subject<BaseDroppedZone> Dropped => _dropped;

        public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
        {
            var position = eventData.position;

            if (IsPointInsideHole(in position))
                _dropped.OnNext(this);
        }

        private void Awake()
        {
            _center = transform.position + _offset;
        }

        private bool IsPointInsideHole(in UnityEngine.Vector2 point)
        {
            var normalizedX = (point.x - _center.x) / _size.x;
            var normalizedY = (point.y - _center.y) / _size.y;

            return (normalizedX * normalizedX + normalizedY * normalizedY) <= 1f;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.red;

            var center = transform.position + _offset;
            var previousPoint = UnityEngine.Vector3.zero;
            var radians = UnityEngine.Mathf.PI * 2 / _segments;

            for (int i = 0; i <= _segments; i++)
            {
                var angle = i * radians;
                var x = UnityEngine.Mathf.Cos(angle) * _size.x;
                var y = UnityEngine.Mathf.Sin(angle) * _size.y;
                var currentPoint = center + new UnityEngine.Vector3(x, y, 0f);

                if (i > 0)
                    UnityEngine.Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }
        }
#endif
    }
}
