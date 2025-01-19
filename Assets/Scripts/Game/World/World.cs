using Cubes.Core.Services;
using Cubes.Game.Services;

namespace Cubes.Game.World
{
    internal sealed class World : IUpdatable, IPausable, System.IDisposable
    {
        [Zenject.Inject] private readonly StateMachine _stateMachine;
        [Zenject.Inject] private readonly UpdaterService _updaterService;
        [Zenject.Inject] private readonly ScreenSystemService _screenSystemService;

        public void Tick(float deltaTime)
        {
        }

        public void SetPause(bool isPaused)
        {
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Run()
        {
            Subscribe();
        }

        internal void Restart()
        {
            ShowMainScreen();
            SubscribeOnUpdaterService();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ShowMainScreen()
        {
            _screenSystemService.Show(Configs.ScreenType.Main);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void GameOver()
        {
            _stateMachine.Enter<GameOverState>();
        }

        private void Subscribe()
        {
        }

        private void Unsubscribe()
        {
        }

        private void SubscribeOnUpdaterService()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void UnubscribeOnUpdaterService()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemovePausable(this);
            }
        }
    }
}
