namespace Cubes.Game.Services
{
    internal sealed class DroppedZone : BaseDroppedZone
    {
        private readonly UniRx.Subject<BaseDroppedZone> _dropped = new();

        internal override UniRx.Subject<BaseDroppedZone> Dropped => _dropped;

        public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
        {
            _dropped.OnNext(this);
        }
    }
}
