namespace Cubes.Core.Services
{
    public sealed class StateMachine : System.IDisposable
    {
        private IState _currentState;

        private readonly System.Collections.Generic.Dictionary<System.Type, IState> _states = new(8);

        public void Add<TState>(TState state) where TState : class, IState
        {
            var type = typeof(TState);

            if (_states.ContainsKey(type))
                _states[type] = state;
            else
                _states.Add(type, state);
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();

            state.Enter();
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            _currentState?.Exit();

            _states.Clear();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _currentState?.Exit();

            var state = GetState<TState>();

            _currentState = state;

            return state;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
