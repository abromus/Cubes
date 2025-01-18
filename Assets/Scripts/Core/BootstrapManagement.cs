#if UNITY_EDITOR
namespace Cubes.Core
{
    [UnityEditor.InitializeOnLoad]
    internal sealed class BootstrapManagement
    {
        static BootstrapManagement()
        {
            OnSceneListChanged();

            UnityEditor.EditorBuildSettings.sceneListChanged += OnSceneListChanged;
        }

        private static void OnSceneListChanged()
        {
            var scenes = UnityEditor.EditorBuildSettings.scenes;

            if (scenes.Length == 0)
                return;

            var startSceneIndex = 0;
            var scene = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.SceneAsset>(scenes[startSceneIndex].path);

            UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = scene;
        }
    }
}
#endif
