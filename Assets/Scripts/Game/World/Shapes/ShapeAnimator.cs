using DG.Tweening;

namespace Cubes.Game.World
{
    internal sealed class ShapeAnimator : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _rectTransform;
        [UnityEngine.Header("Move")]
        [UnityEngine.SerializeField] private float _moveDuration;
        [UnityEngine.Header("Jump")]
        [UnityEngine.SerializeField] private float _jumpDuration;
        [UnityEngine.SerializeField] private float _jumpPower;
        [UnityEngine.SerializeField] private int _jumpCount;
        [UnityEngine.Header("Move")]
        [UnityEngine.SerializeField] private float _explodeMoveDuration;
        [UnityEngine.SerializeField] private float _explodeJumpPower;
        [UnityEngine.SerializeField] private int _explodeJumpCount;
        [UnityEngine.SerializeField] private float _explodeFallDuration;
        [UnityEngine.SerializeField] private float _explodeShakeDuration;
        [UnityEngine.SerializeField] private UnityEngine.Vector3 _explodeShakeStrength;
        [UnityEngine.SerializeField] private int _explodeShakeVibrato;

        private Sequence _sequence;

        internal void Jump(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition)
        {
            KillAnimation();

            _rectTransform.anchoredPosition = startPosition;

            _sequence = DOTween.Sequence()
                .Append(_rectTransform.DOJumpAnchorPos(targetPosition, _jumpPower, _jumpCount, _jumpDuration))
                .OnComplete(KillAnimation);
        }

        internal void Move(in UnityEngine.Vector2 startPosition, in UnityEngine.Vector2 targetPosition, float delay)
        {
            KillAnimation();

            _rectTransform.anchoredPosition = startPosition;

            _sequence = DOTween.Sequence()
                .Append(_rectTransform.DOLocalMove(targetPosition, _moveDuration + delay))
                .OnComplete(KillAnimation);
        }

        internal void Explode(in ExplodeAnimationArgs args)
        {
            KillAnimation();

            var callback = args.Callback;

            _rectTransform.anchoredPosition = args.StartMovePosition;

            _sequence = DOTween.Sequence()
                .Append(_rectTransform.DOJumpAnchorPos(args.TargetMovePosition, _explodeJumpPower, _explodeJumpCount, _explodeMoveDuration))
                .Append(_rectTransform.DOLocalMove(args.TargetFallPosition, _explodeFallDuration))
                .Append(_rectTransform.DOShakeRotation(_explodeShakeDuration, _explodeShakeStrength, _explodeShakeVibrato))
                .OnComplete(AfterExplode);

            void AfterExplode()
            {
                KillAnimation();

                callback?.Invoke();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Stop()
        {
            KillAnimation();
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
