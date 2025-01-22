namespace Cubes.Game.Services
{
    internal sealed class AudioService : UnityEngine.MonoBehaviour, Core.Services.IPausable, System.IDisposable
    {
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _backgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _sounds;

        [Zenject.Inject] private readonly Core.Services.PauseService _pauseService;

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            Subscribe();
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
                _backgroundMusic.Pause();
            else
                _backgroundMusic.UnPause();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            Unsubscribe();
        }

        internal void PlayBackgroundMusic(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _backgroundMusic.Stop();
            _backgroundMusic.clip = clip;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }

        internal void PlaySound(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _sounds.PlayOneShot(clip);
        }

        internal void StopBackgroundMusic()
        {
            if (this == null)
                return;

            _backgroundMusic.clip = null;
            _backgroundMusic.Stop();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void SetActiveBackgroundMusic(bool isActive)
        {
            _backgroundMusic.enabled = isActive;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void SetActiveSounds(bool isActive)
        {
            _sounds.enabled = isActive;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _pauseService.AddPausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _pauseService?.RemovePausable(this);
        }
    }
}
