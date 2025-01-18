namespace Cubes.Game.Services
{
    internal abstract class BaseScreen : UnityEngine.MonoBehaviour, IScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.CanvasGroup _canvasGroup;

        public abstract Configs.ScreenType ScreenType { get; }

        public abstract bool IsShown { get; }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public virtual void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public virtual void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}
