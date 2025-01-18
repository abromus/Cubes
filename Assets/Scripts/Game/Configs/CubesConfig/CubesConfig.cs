namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(CubesConfig), menuName = SettingsKeys.GamePathKey + nameof(CubesConfig))]
    internal sealed class CubesConfig : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] private CubeInfo[] _cubeInfos;

        internal CubeInfo[] CubeInfos => _cubeInfos;
    }
}
