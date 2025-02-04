﻿namespace Cubes.Game.UI.SettingsScreen
{
    internal sealed class SettingsScreenModel : Services.IScreenModel
    {
        internal UniRx.ReactiveProperty<bool> IsShown { get; private set; }

        [UnityEngine.Scripting.Preserve]
        public SettingsScreenModel()
        {
            IsShown = new UniRx.ReactiveProperty<bool>(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateIsShown(bool isShown)
        {
            IsShown.Value = isShown;
        }
    }
}
