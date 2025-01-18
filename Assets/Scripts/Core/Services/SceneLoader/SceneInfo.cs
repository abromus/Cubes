namespace Cubes.Core
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct SceneInfo
    {
        private readonly string _sceneName;
        private readonly UnityEngine.SceneManagement.LoadSceneMode _mode;
        private readonly System.Action _success;
        private readonly bool _isActive;

        internal readonly string SceneName => _sceneName;

        internal readonly UnityEngine.SceneManagement.LoadSceneMode Mode => _mode;

        internal readonly bool IsActive => _isActive;

        internal readonly System.Action Success => _success;

        internal SceneInfo(
            string sceneName,
            UnityEngine.SceneManagement.LoadSceneMode mode,
            bool isActive,
            System.Action success)
        {
            _sceneName = sceneName;
            _mode = mode;
            _isActive = isActive;
            _success = success;
        }
    }
}
