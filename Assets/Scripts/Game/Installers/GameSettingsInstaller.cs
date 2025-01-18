using Zenject;

namespace Cubes.Game.Installers
{
    [UnityEngine.CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    internal sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [UnityEngine.SerializeField] private Configs.CubesConfig _cubesConfig;
        [UnityEngine.SerializeField] private Configs.ScreensConfig _screensConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_cubesConfig).NonLazy();
            Container.BindInstance(_screensConfig).NonLazy();
        }
    }
}
