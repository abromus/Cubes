namespace Cubes.Game
{
    internal sealed class GameSceneController : UnityEngine.MonoBehaviour
    {
        [Zenject.Inject] private readonly Core.Services.StateMachine _stateMachine;
        [Zenject.Inject] private readonly Services.AudioService _audioService;
        [Zenject.Inject] private readonly Services.ScreenSystemService _screenSystemService;
        [Zenject.Inject] private readonly World.World _world;

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            _stateMachine.Add(new Services.GameInitializationState(_stateMachine, _world));
            _stateMachine.Add(new Services.GameRestartState(_stateMachine, _world));
            _stateMachine.Add(new Services.GameLoopState());
            _stateMachine.Add(new Services.GameExitState());
        }

        private void Awake()
        {
            _stateMachine.Enter<Services.GameInitializationState>();
        }
    }
}
