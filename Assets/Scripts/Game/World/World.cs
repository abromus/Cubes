using UniRx;

namespace Cubes.Game.World
{
    internal sealed class World : System.IDisposable
    {
        [Zenject.Inject] private readonly Data.GameData _gameData;
        [Zenject.Inject] private readonly Core.Services.StateMachine _stateMachine;
        [Zenject.Inject] private readonly Core.Services.UpdaterService _updaterService;
        [Zenject.Inject] private readonly Services.ScreenSystemService _screenSystemService;

        private readonly CompositeDisposable _subscriptions = new();

        internal Subject<Unit> Exited { get; } = new();

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
            _screenSystemService.HideAll();
            _screenSystemService.Show(Configs.ScreenType.Main);
        }

        private void Subscribe()
        {
            _gameData.Restarted.Subscribe(OnRestarted).AddTo(_subscriptions);
            _gameData.Exited.Subscribe(OnExited).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnRestarted(Unit _)
        {
            _stateMachine.Enter<Services.GameRestartState>();
        }

        private void OnExited(Unit _)
        {
            _stateMachine.Enter<Services.GameExitState>();

            Exited.OnNext(Unit.Default);
        }
    }
}
