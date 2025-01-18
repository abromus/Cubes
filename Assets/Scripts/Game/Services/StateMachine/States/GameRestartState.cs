namespace Cubes.Game.Services
{
    internal sealed class GameRestartState : Core.Services.IState
    {
        private readonly Core.Services.StateMachine _stateMachine;
        private readonly World.World _world;

        internal GameRestartState(Core.Services.StateMachine stateMachine, World.World world)
        {
            _stateMachine = stateMachine;
            _world = world;
        }

        public void Enter()
        {
            _world.Restart();

            _stateMachine.Enter<GameLoopState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
