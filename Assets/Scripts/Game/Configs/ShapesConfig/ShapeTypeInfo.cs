namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct ShapeTypeInfo
    {
        [UnityEngine.SerializeField] private ShapeType _type;
        [UnityEngine.SerializeField] private World.ShapeView _prefab;

        internal readonly ShapeType Type => _type;

        internal readonly World.ShapeView Prefab => _prefab;
    }
}
