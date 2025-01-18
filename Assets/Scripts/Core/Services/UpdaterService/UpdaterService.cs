namespace Cubes.Core.Services
{
    public sealed class UpdaterService : System.IDisposable
    {
        private readonly System.Collections.Generic.List<IUpdatable> _updatables = new(32);
        private readonly System.Collections.Generic.List<ILateUpdatable> _lateUpdatables = new(32);
        private readonly System.Collections.Generic.List<IPausable> _pausables = new(32);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Tick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void LateTick(float deltaTime)
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].LateTick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            for (int i = 0; i < _pausables.Count; i++)
                _pausables[i].SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddUpdatable(IUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _updatables.Contains(lateUpdatable) == false)
                _updatables.Add(lateUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable != null && _updatables.Contains(updatable))
                _updatables.Remove(updatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable) == false)
                _lateUpdatables.Add(lateUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemoveLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable))
                _lateUpdatables.Remove(lateUpdatable);
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
            for (int i = _updatables.Count - 1; 0 <= i; i--)
                _updatables.Remove(_updatables[i]);

            for (int i = _updatables.Count - 1; 0 <= i; i--)
                _lateUpdatables.Remove(_lateUpdatables[i]);

            for (int i = _updatables.Count - 1; 0 <= i; i--)
                _pausables.Remove(_pausables[i]);
        }
    }
}
