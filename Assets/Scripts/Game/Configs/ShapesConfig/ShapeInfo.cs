namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct ShapeInfo
    {
        [UnityEngine.SerializeField] private ShapeType _type;
        [UnityEngine.SerializeField] private UnityEngine.Color _color;

        internal readonly ShapeType Type => _type;

        internal readonly UnityEngine.Color Color => _color;
    }
}
