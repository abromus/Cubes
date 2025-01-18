using UniRx;

namespace Cubes.Core.Services
{
    internal sealed class SceneLoader
    {
        private SceneInfo _info;

        internal void LoadAsync(in SceneInfo info)
        {
            _info = info;

            Observable.FromCoroutine(LoadSceneCoroutine).Subscribe(OnSuccess, OnError);
        }

        private System.Collections.IEnumerator LoadSceneCoroutine()
        {
            var sceneName = _info.SceneName;

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsFalse(string.IsNullOrEmpty(sceneName));
#endif

            if (IsSceneLoaded(sceneName))
                yield break;

            var progressSceneActivation = 0.9f;
            var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, _info.Mode);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.isDone == false)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log($"Прогресс загрузки: {asyncOperation.progress * 100}%");
#endif

                if (progressSceneActivation <= asyncOperation.progress)
                    asyncOperation.allowSceneActivation = _info.IsActive;

                yield return null;
            }
        }

        private bool IsSceneLoaded(string sceneName)
        {
            var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);

                if (scene.name.Equals(sceneName))
                    return true;
            }

            return false;
        }

        private void OnSuccess(Unit _)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"Сцена {_info.SceneName} успешно загружена!");
#endif

            _info.Success?.Invoke();
        }

        private void OnError(System.Exception exception)
        {
            UnityEngine.Debug.LogError($"Ошибка при загрузке сцены {_info.SceneName}: {exception.Message}");
        }
    }
}
