namespace Cubes.Core.Services
{
    public sealed class PauseService : System.IDisposable
    {
        private readonly System.Collections.Generic.List<IPausable> _pausables = new(32);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            for (int i = 0; i < _pausables.Count; i++)
                _pausables[i].SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddPausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable) == false)
                _pausables.Add(pausable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemovePausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable))
                _pausables.Remove(pausable);
        }

        public void Dispose()
        {
            for (int i = _pausables.Count - 1; 0 <= i; i--)
                _pausables.Remove(_pausables[i]);
        }
    }
}
