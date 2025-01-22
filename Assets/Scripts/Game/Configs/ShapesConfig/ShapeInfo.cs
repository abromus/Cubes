namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct ShapeInfo
    {
        [UnityEngine.SerializeField] private int _id;
        [UnityEngine.SerializeField] private ShapeType _type;
        [UnityEngine.SerializeField] private UnityEngine.Color _color;

        internal readonly int Id => _id;

        internal readonly ShapeType Type => _type;

        internal readonly UnityEngine.Color Color => _color;
    }
}
