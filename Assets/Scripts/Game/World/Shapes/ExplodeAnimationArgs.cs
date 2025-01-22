namespace Cubes.Game.World
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ExplodeAnimationArgs
    {
        private readonly UnityEngine.Vector2 _startMovePosition;
        private readonly UnityEngine.Vector2 _targetMovePosition;
        private readonly UnityEngine.Vector2 _targetFallPosition;
        private readonly System.Action _falledCallback;
        private readonly System.Action _explodedCallback;

        internal readonly UnityEngine.Vector2 StartMovePosition => _startMovePosition;

        internal readonly UnityEngine.Vector2 TargetMovePosition => _targetMovePosition;

        internal readonly UnityEngine.Vector2 TargetFallPosition => _targetFallPosition;

        internal readonly System.Action FalledCallback => _falledCallback;

        internal readonly System.Action ExplodedCallback => _explodedCallback;

        internal ExplodeAnimationArgs(
            in UnityEngine.Vector2 startMovePosition,
            in UnityEngine.Vector2 targetMovePosition,
            in UnityEngine.Vector2 targetFallPosition,
            System.Action falledCallback,
            System.Action explodedCallback)
        {
            _startMovePosition = startMovePosition;
            _targetMovePosition = targetMovePosition;
            _targetFallPosition = targetFallPosition;
            _falledCallback = falledCallback;
            _explodedCallback = explodedCallback;
        }
    }
}
