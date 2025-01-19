namespace Cubes.Game.Services
{
    internal abstract class BaseScreenView : UnityEngine.MonoBehaviour, IScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.CanvasGroup _canvasGroup;

        private IScreenPresenter _presenter;

        internal abstract Configs.ScreenType ScreenType { get; }

        internal UnityEngine.RectTransform RectTransform => _rectTransform;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public abstract void Init(IScreenPresenter presenter);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal virtual void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal virtual void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        internal virtual void Destroy()
        {
            if (this != null && gameObject != null)
            {
                Hide();
                Destroy(gameObject);
            }
        }
    }
}
