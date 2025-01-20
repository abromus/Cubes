using Zenject;

namespace Cubes.Game.Installers
{
    [UnityEngine.CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    internal sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [UnityEngine.SerializeField] private Configs.ShapesConfig _shapesConfig;
        [UnityEngine.SerializeField] private Configs.ScreensConfig _screensConfig;

        public override void InstallBindings()
        {
            Container.Bind<Configs.IShapesConfig>().FromInstance(_shapesConfig).NonLazy();
            Container.BindInstance(_screensConfig).NonLazy();
        }
    }
}
