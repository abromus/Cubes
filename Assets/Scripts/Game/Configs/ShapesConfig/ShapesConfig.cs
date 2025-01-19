namespace Cubes.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ShapesConfig), menuName = SettingsKeys.GamePathKey + nameof(ShapesConfig))]
    internal sealed class ShapesConfig : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] private ShapeTypeInfo[] _shapeTypeInfos;
        [UnityEngine.SerializeField] private ShapeInfo[] _shapeInfos;

        internal ShapeTypeInfo[] ShapeTypeInfos => _shapeTypeInfos;

        internal ShapeInfo[] ShapeInfos => _shapeInfos;
    }
}
