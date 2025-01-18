namespace Cubes.Game.Services
{
    internal sealed class GameOverState : Core.Services.IState
    {
        private readonly AudioService _audioService;
        private readonly ScreenSystemService _screenSystemService;

        internal GameOverState(AudioService audioService, ScreenSystemService screenSystemService)
        {
            _audioService = audioService;
            _screenSystemService = screenSystemService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter()
        {
            _screenSystemService.HideAll();
            _screenSystemService.Show(Configs.ScreenType.GameOver);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
