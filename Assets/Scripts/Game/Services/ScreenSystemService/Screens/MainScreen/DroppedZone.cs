namespace Cubes.Game.Services
{
    internal sealed class DroppedZone : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IDropHandler
    {
        private readonly UniRx.Subject<DroppedZone> _dropped = new();

        internal UniRx.Subject<DroppedZone> Dropped => _dropped;

        public void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
        {
            _dropped.OnNext(this);
        }
    }
}
