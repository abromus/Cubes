namespace Cubes.Game.Services
{
    internal sealed class LocalizeService
    {
        private string _language;

        [Zenject.Inject] private readonly Configs.LocalizationConfig _localizationConfig;

        private readonly System.Collections.Generic.Dictionary<string, string> _keys = new(32);

        [Zenject.Inject]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Construct()
        {
            ChangeLanguage(Languages.Ru);
            UpdateKeys();
        }

        internal string Localize(string key)
        {
            if (_keys.TryGetValue(key, out string text))
                return text;

#if UNITY_EDITOR
            UnityEngine.Debug.LogWarning($"[LocalizeService]: Text for key \"{key}\" wasn't found");
#endif
            return "<#NOTFOUND>";
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ChangeLanguage(string language)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"[LocalizeService]: Language {language} selected");
#endif

            _language = language;
        }

        private void UpdateKeys()
        {
            var infos = _localizationConfig.LocalizationInfos;

            for (int i = 0; i < infos.Length; i++)
            {
                var info = infos[i];
                var key = info.Key;
                var message = _language.Equals(Languages.Ru) ? info.Ru : info.En;

                _keys[key] = message;
            }
        }

        private sealed class Languages
        {
            internal const string Ru = "Ru";
            internal const string En = "En";
        }
    }
}
