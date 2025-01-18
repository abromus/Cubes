namespace Cubes.Core
{
    internal sealed class Bootstrap : Zenject.IInitializable
    {
        [Zenject.Inject] private readonly Services.StateMachine _stateMachine;
        [Zenject.Inject] private readonly Services.SceneLoader _sceneLoader;

        [UnityEngine.Scripting.Preserve]
        public void Initialize()
        {
            _stateMachine.Add(new Services.BootstrapState(_stateMachine, _sceneLoader));
            _stateMachine.Add(new Services.GameState(_stateMachine, _sceneLoader));
            _stateMachine.Add(new Services.GameLoopState());

            _stateMachine.Enter<Services.BootstrapState>();
        }
    }
}
