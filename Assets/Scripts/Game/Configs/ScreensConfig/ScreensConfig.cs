namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ScreensConfig), menuName = SettingsKeys.GamePathKey + nameof(ScreensConfig))]
    internal sealed class ScreensConfig : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] private ScreenInfo[] _screenInfos;

        internal ScreenInfo[] ScreenInfos => _screenInfos;
    }
}
