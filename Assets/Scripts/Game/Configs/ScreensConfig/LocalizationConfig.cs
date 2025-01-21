namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = SettingsKeys.GamePathKey + nameof(LocalizationConfig))]
    internal sealed class LocalizationConfig : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] private LocalizationInfo[] _localizationInfos;

        internal LocalizationInfo[] LocalizationInfos => _localizationInfos;
    }
}
