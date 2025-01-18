namespace Cubes.Core.Services
{
    internal sealed class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        internal BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            var info = new SceneInfo(SceneNames.Core, UnityEngine.SceneManagement.LoadSceneMode.Single, true, OnSceneLoad);

            _sceneLoader.LoadAsync(in info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameState>();
        }
    }
}
