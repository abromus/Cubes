namespace Cubes.Game.Configs
{
    [System.Serializable]
    internal struct ShapeTypeInfo
    {
        [UnityEngine.SerializeField] private ShapeType _type;
        [UnityEngine.SerializeField] private UI.MainScreen.Shapes.ShapeView _prefab;

        internal readonly ShapeType Type => _type;

        internal readonly UI.MainScreen.Shapes.ShapeView Prefab => _prefab;
    }
}
