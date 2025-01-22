namespace Cubes.Game.Services
{
    internal sealed class GameInitializationState : Core.Services.IState
    {
        private readonly Game.Game _game;
        private readonly Core.Services.StateMachine _stateMachine;

        internal GameInitializationState(Core.Services.StateMachine stateMachine, Game.Game game)
        {
            _stateMachine = stateMachine;
            _game = game;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter()
        {
            _game.Run();

            _stateMachine.Enter<GameRestartState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
