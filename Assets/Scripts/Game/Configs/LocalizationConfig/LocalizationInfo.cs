namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct LocalizationInfo
    {
        [UnityEngine.SerializeField] private string _key;
        [UnityEngine.SerializeField] private string _ru;
        [UnityEngine.SerializeField] private string _en;

        internal readonly string Key => _key;

        internal readonly string Ru => _ru;

        internal readonly string En => _en;
    }
}
