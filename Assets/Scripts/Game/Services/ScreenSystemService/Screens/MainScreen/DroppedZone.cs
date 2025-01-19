namespace Cubes.Game.Services
{
    internal sealed class DroppedZone : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IDropHandler
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;

        private readonly UniRx.Subject<DroppedZone> _dropped = new();

        internal UniRx.Subject<DroppedZone> Dropped => _dropped;

        public void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
        {
#if UNITY_EDITOR
            UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out UnityEngine.Vector2 position);

            UnityEngine.Debug.Log($"size = {_rectTransform.rect.size}, pos = {_rectTransform.anchoredPosition}, item.pos = {eventData.position}, position = {position}");
#endif

            _dropped.OnNext(this);
        }
    }
}
