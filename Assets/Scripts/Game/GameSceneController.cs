using UniRx;

namespace Cubes.Game
{
    internal sealed class GameSceneController : UnityEngine.MonoBehaviour
    {
        [Zenject.Inject] private readonly Core.Services.StateMachine _stateMachine;
        [Zenject.Inject] private readonly Game.Game _game;

        private readonly CompositeDisposable _subscriptions = new();

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            _stateMachine.Add(new Services.GameInitializationState(_stateMachine, _game));
            _stateMachine.Add(new Services.GameRestartState(_stateMachine, _game));
            _stateMachine.Add(new Services.GameLoopState());
            _stateMachine.Add(new Services.GameExitState());

            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Awake()
        {
            _stateMachine.Enter<Services.GameInitializationState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _game.Exited.Subscribe(OnExited).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnExited(Unit _)
        {
            Unsubscribe();

            _game.Dispose();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}
