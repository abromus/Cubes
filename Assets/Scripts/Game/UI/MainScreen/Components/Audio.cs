namespace Cubes.Game.UI.MainScreen
{
    internal sealed class Audio : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _jumpSound;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _holeSound;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _explodeSound;

        private Services.AudioService _audioService;

        internal void Init(Services.AudioService audioService)
        {
            _audioService = audioService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void PlayBackgroundMusic()
        {
            _audioService.PlayBackgroundMusic(_backgroundMusic);
        }

        internal void PlaySound(SoundType type)
        {
            var sound = GetSound(type);

            _audioService.PlaySound(sound);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void StopBackgroundMusic()
        {
            _audioService.StopBackgroundMusic();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private UnityEngine.AudioClip GetSound(SoundType type)
        {
            return type switch
            {
                SoundType.Jump => _jumpSound,
                SoundType.Hole => _holeSound,
                SoundType.Explode => _explodeSound,
                _ => null,
            };
        }
    }
}
