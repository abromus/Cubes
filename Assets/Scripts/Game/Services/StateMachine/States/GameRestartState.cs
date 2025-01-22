namespace Cubes.Game.Services
{
    internal sealed class GameRestartState : Core.Services.IState
    {
        private readonly Core.Services.StateMachine _stateMachine;
        private readonly Game.Game _game;

        internal GameRestartState(Core.Services.StateMachine stateMachine, Game.Game game)
        {
            _stateMachine = stateMachine;
            _game = game;
        }

        public void Enter()
        {
            _game.Restart();

            _stateMachine.Enter<GameLoopState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
