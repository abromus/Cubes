using Newtonsoft.Json;

namespace Cubes.Core.Services
{
    public sealed class SaveLoadService
    {
        public void Save<T>(T data, string fileName) where T : class, ISavableData
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            var path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, fileName);

            System.IO.File.WriteAllText(path, json);
        }

        public bool TryLoad<T>(string fileName, out T data) where T : class, ISavableData
        {
            var path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, fileName);

            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);

                data = JsonConvert.DeserializeObject<T>(json);

                return true;
            }

            data = null;

            return false;
        }
    }
}
