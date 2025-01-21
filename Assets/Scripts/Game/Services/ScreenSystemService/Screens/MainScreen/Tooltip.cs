using DG.Tweening;

namespace Cubes.Game.Services
{
    internal sealed class Tooltip : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.SerializeField] private UnityEngine.CanvasGroup _canvasGroup;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _message;
        [UnityEngine.SerializeField] private float _fadeInDuration;
        [UnityEngine.SerializeField] private float _fadeOutDuration;
        [UnityEngine.SerializeField] private float _fadeOutDelay;

        private Sequence _sequence;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void SetText(string text)
        {
            _message.text = text;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void FadeIn()
        {
            Hide();

            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            _sequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, _fadeInDuration).SetEase(Ease.InQuad))
                .OnComplete(FadeOut);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Hide()
        {
            KillAnimation();

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        private void Awake()
        {
            Hide();
        }

        private void FadeOut()
        {
            KillAnimation();

            _sequence = DOTween.Sequence()
                .AppendInterval(_fadeOutDelay)
                .Append(_canvasGroup.DOFade(0f, _fadeOutDuration))
                .OnComplete(Hide);
        }

        private void KillAnimation()
        {
            if (_sequence != null && _sequence.IsActive())
            {
                _sequence.Kill();
                _sequence = null;
            }
        }
    }
}
