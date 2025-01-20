namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ShapesConfig), menuName = SettingsKeys.GamePathKey + nameof(ShapesConfig))]
    internal sealed class ShapesConfig : UnityEngine.ScriptableObject, IShapesConfig
    {
        [UnityEngine.SerializeField] private ShapeTypeInfo[] _shapeTypeInfos;
        [UnityEngine.SerializeField] private ShapeInfo[] _shapeInfos;

        public ShapeTypeInfo[] ShapeTypeInfos => _shapeTypeInfos;

        public ShapeInfo[] ShapeInfos => _shapeInfos;
    }
}
