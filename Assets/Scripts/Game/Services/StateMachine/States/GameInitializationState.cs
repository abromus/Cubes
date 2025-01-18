namespace Cubes.Game.Services
{
    internal sealed class GameInitializationState : Core.Services.IState
    {
        private readonly World.World _world;
        private readonly Core.Services.StateMachine _stateMachine;

        internal GameInitializationState(Core.Services.StateMachine stateMachine, World.World world)
        {
            _stateMachine = stateMachine;
            _world = world;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter()
        {
            _world.Run();

            _stateMachine.Enter<GameRestartState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
