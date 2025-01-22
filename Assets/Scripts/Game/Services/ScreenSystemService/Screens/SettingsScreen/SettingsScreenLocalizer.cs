﻿namespace Cubes.Game.Services
{
    internal sealed class SettingsScreenLocalizer : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _backgroundMusicText;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _toggleSoundsText;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _buttonRestartText;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _buttonExitText;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _buttonApplyText;

        [UnityEngine.Header("Localization keys")]
        [UnityEngine.SerializeField] private string _backgroundMusicKey;
        [UnityEngine.SerializeField] private string _toggleSoundsKey;
        [UnityEngine.SerializeField] private string _buttonRestartKey;
        [UnityEngine.SerializeField] private string _buttonExitKey;
        [UnityEngine.SerializeField] private string _buttonApplyKey;

        private LocalizeService _localizeService;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Init(LocalizeService localizeService)
        {
            _localizeService = localizeService;

            Localize();
        }

        private void Localize()
        {
            _backgroundMusicText.text = _localizeService.Localize(_backgroundMusicKey);
            _toggleSoundsText.text = _localizeService.Localize(_toggleSoundsKey);
            _buttonRestartText.text = _localizeService.Localize(_buttonRestartKey);
            _buttonExitText.text = _localizeService.Localize(_buttonExitKey);
            _buttonApplyText.text = _localizeService.Localize(_buttonApplyKey);
        }
    }
}
