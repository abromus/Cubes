namespace Cubes.Game.Services
{
    internal sealed class AudioService : UnityEngine.MonoBehaviour, Core.Services.IPausable, System.IDisposable
    {
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _backgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _oneShotSound;
        [UnityEngine.SerializeField] private UnityEngine.Transform _loopSoundsContainer;

        [Zenject.Inject] private readonly Core.Services.UpdaterService _updaterService;

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

        internal void PlayOneShotSound(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _oneShotSound.PlayOneShot(clip);
        }

        internal void StopBackgroundMusic()
        {
            _backgroundMusic.clip = null;
            _backgroundMusic.Stop();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _updaterService.AddPausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _updaterService?.RemovePausable(this);
        }
    }
}
