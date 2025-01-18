namespace Cubes.Game.Services
{
    internal sealed class EventSystemService : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.EventSystems.EventSystem _eventSystem;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool IsPointerOverGameObject()
        {
            return _eventSystem.IsPointerOverGameObject();
        }
    }
}
