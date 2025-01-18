namespace Cubes.Core
{
    internal sealed class CoreSceneController : UnityEngine.MonoBehaviour
    {
        [Zenject.Inject] private readonly Services.UpdaterService _updaterService;

        private void Update()
        {
            _updaterService.Tick(UnityEngine.Time.deltaTime);
        }

        private void LateUpdate()
        {
            _updaterService.LateTick(UnityEngine.Time.deltaTime);
        }

        private void OnApplicationFocus(bool focus)
        {
            _updaterService.SetPause(focus == false);
        }

        private void OnApplicationPause(bool pause)
        {
            _updaterService.SetPause(pause);
        }
    }
}
