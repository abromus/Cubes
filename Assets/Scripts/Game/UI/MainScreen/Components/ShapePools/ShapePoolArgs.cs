namespace Cubes.Game.UI.MainScreen
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ShapePoolArgs
    {
        private readonly Factories.ShapeFactory _factory;
        private readonly Configs.IShapesConfig _config;
        private readonly UnityEngine.RectTransform _parent;
        private readonly UnityEngine.RectTransform _screenRectTransform;

        internal readonly Factories.ShapeFactory Factory => _factory;

        internal readonly Configs.IShapesConfig Config => _config;

        internal readonly UnityEngine.RectTransform Parent => _parent;

        internal readonly UnityEngine.RectTransform ScreenRectTransform => _screenRectTransform;

        internal ShapePoolArgs(
            Factories.ShapeFactory factory,
            Configs.IShapesConfig config,
            UnityEngine.RectTransform parent,
            UnityEngine.RectTransform screenRectTransform)
        {
            _factory = factory;
            _config = config;
            _parent = parent;
            _screenRectTransform = screenRectTransform;
        }
    }
}
