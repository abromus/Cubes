namespace Cubes.Core
{
    internal sealed class CoreSceneController : UnityEngine.MonoBehaviour
    {
        [Zenject.Inject] private readonly Services.PauseService _pauseService;

        private void OnApplicationFocus(bool focus)
        {
            _pauseService.SetPause(focus == false);
        }

        private void OnApplicationPause(bool pause)
        {
            _pauseService.SetPause(pause);
        }
    }
}
