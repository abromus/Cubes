namespace Cubes.Game.World
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ExplodeAnimationArgs
    {
        private readonly UnityEngine.Vector2 _startMovePosition;
        private readonly UnityEngine.Vector2 _targetMovePosition;
        private readonly UnityEngine.Vector2 _targetFallPosition;
        private readonly System.Action _callback;

        internal readonly UnityEngine.Vector2 StartMovePosition => _startMovePosition;

        internal readonly UnityEngine.Vector2 TargetMovePosition => _targetMovePosition;

        internal readonly UnityEngine.Vector2 TargetFallPosition => _targetFallPosition;

        internal readonly System.Action Callback => _callback;

        internal ExplodeAnimationArgs(
            in UnityEngine.Vector2 startMovePosition,
            in UnityEngine.Vector2 targetMovePosition,
            in UnityEngine.Vector2 targetFallPosition,
            System.Action callback)
        {
            _startMovePosition = startMovePosition;
            _targetMovePosition = targetMovePosition;
            _targetFallPosition = targetFallPosition;
            _callback = callback;
        }
    }
}
