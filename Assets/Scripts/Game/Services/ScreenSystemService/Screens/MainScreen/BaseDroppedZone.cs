namespace Cubes.Game.Services
{
    internal abstract class BaseDroppedZone : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IDropHandler
    {
        internal abstract UniRx.Subject<BaseDroppedZone> Dropped { get; }

        public abstract void OnDrop(UnityEngine.EventSystems.PointerEventData eventData);
    }
}
