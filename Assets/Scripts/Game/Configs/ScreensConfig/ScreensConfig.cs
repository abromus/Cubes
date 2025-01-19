namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ScreensConfig), menuName = SettingsKeys.GamePathKey + nameof(ScreensConfig))]
    internal sealed class ScreensConfig : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] private Services.BaseScreenView[] _screens;

        internal Services.BaseScreenView[] Screens => _screens;
    }
}
