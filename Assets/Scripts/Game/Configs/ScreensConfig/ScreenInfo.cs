namespace Cubes.Game.Configs
{
    [System.Serializable]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct ScreenInfo
    {
        [UnityEngine.SerializeField] private ScreenType _screenType;
        [UnityEngine.SerializeField] private Services.BaseScreen _screen;

        internal readonly ScreenType ScreenType => _screenType;

        internal readonly Services.BaseScreen Screen => _screen;
    }
}
