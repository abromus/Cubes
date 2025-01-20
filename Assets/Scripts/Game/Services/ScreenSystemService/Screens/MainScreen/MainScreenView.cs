using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class MainScreenView : BaseScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _shapesContainer;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _draggingShapeContainer;
        [UnityEngine.SerializeField] private DroppedZone _holeZone;
        [UnityEngine.SerializeField] private DroppedZone _shapesStorageZone;
        [UnityEngine.SerializeField] private ShapesTower _tower;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        [Zenject.Inject] private readonly AudioService _audioService;

        private MainScreenPresenter _presenter;

        private readonly CompositeDisposable _subscriptions = new();

        internal override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        internal UnityEngine.RectTransform ShapesContainer => _shapesContainer;

        internal UnityEngine.RectTransform DraggingShapeContainer => _draggingShapeContainer;

        internal UnityEngine.RectTransform TowerShapeContainer => _tower.ShapeContainer;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(IScreenPresenter presenter)
        {
            _presenter = presenter as MainScreenPresenter;
        }

        internal override void Show()
        {
            base.Show();

            PlayBackgroundMusic();
            Subscribe();
        }

        internal override void Hide()
        {
            base.Hide();

            StopBackgroundMusic();
            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddShapeToTower(World.IShapePresenter shapePresenter)
        {
            _tower.Add(shapePresenter);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal UnityEngine.Vector2 GetTowerSize()
        {
            return _tower.GetAvailableSize();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PlayBackgroundMusic()
        {
            _audioService.PlayBackgroundMusic(_backgroundMusic);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void StopBackgroundMusic()
        {
            _audioService.StopBackgroundMusic();
        }

        private void Subscribe()
        {
            _buttonSettings.OnClickAsObservable().Subscribe(OnButtonSettingsClicked).AddTo(_subscriptions);
            _holeZone.Dropped.Subscribe(OnHoleZoneDropped).AddTo(_subscriptions);
            _shapesStorageZone.Dropped.Subscribe(OnShapesStorageZoneDropped).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnButtonSettingsClicked(Unit _)
        {
            _presenter.ShowSettingsScreen();
        }

        private void OnHoleZoneDropped(DroppedZone zone)
        {
        }

        private void OnShapesStorageZoneDropped(DroppedZone zone)
        {
            _presenter.ResolveShape(zone);
        }
    }
}
