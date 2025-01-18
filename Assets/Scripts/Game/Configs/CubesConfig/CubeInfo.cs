namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct CubeInfo
    {
        [UnityEngine.SerializeField] private Cube _cube;
        [UnityEngine.SerializeField] private UnityEngine.Color _color;

        internal readonly Cube Cube => _cube;

        internal readonly UnityEngine.Color Color => _color;
    }
}
