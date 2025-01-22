namespace Cubes.Game.Data
{
    internal sealed class GameData
    {
        internal UniRx.Subject<UniRx.Unit> Restarted { get; } = new();

        internal UniRx.Subject<UniRx.Unit> Exited { get; } = new();

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Restart()
        {
            Restarted.OnNext(UniRx.Unit.Default);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Exit()
        {
            Exited.OnNext(UniRx.Unit.Default);
        }
    }
}
