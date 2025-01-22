namespace Cubes.Game.UI.MainScreen
{
    internal sealed class Commentator : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private Tooltip _tooltip;

        [UnityEngine.Header("Localization keys")]
        [UnityEngine.SerializeField] private string _droppedOnHoleKey;
        [UnityEngine.SerializeField] private string _droppedPastHoleKey;
        [UnityEngine.SerializeField] private string _addShapeToTowerKey;
        [UnityEngine.SerializeField] private string _minShapeCountRestrictionKey;
        [UnityEngine.SerializeField] private string _intersectionRestrictionKey;
        [UnityEngine.SerializeField] private string _containerSizeRestrictionKey;

        private Services.LocalizeService _localizeService;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Init(Services.LocalizeService localizeService)
        {
            _localizeService = localizeService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowAddShapeToTowerMessage()
        {
            ShowMessage(_addShapeToTowerKey);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowDroppedOnHoleMessage()
        {
            ShowMessage(_droppedOnHoleKey);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ShowDroppedPastHoleMessage()
        {
            ShowMessage(_droppedPastHoleKey);
        }

        internal void ShowResolverMessage(ResolverStatus status)
        {
            if (status == ResolverStatus.MinShapeCountRestriction)
                ShowMessage(_minShapeCountRestrictionKey);
            else if (status == ResolverStatus.IntersectionRestriction)
                ShowMessage(_intersectionRestrictionKey);
            else if (status == ResolverStatus.ContainerSizeRestriction)
                ShowMessage(_containerSizeRestrictionKey);
        }

        private void ShowMessage(string key)
        {
            var message = _localizeService.Localize(key);

            ShowLocalizedMessage(message);
        }

        private void ShowLocalizedMessage(string message)
        {
            _tooltip.SetText(message);
            _tooltip.FadeIn();
        }
    }
}
