using UniRx;

namespace Cubes.Game.Services
{
    internal sealed class MainScreenView : BaseScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _shapesContainer;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _draggingShapeContainer;
        [UnityEngine.SerializeField] private HoleDroppedZone _holeZone;
        [UnityEngine.SerializeField] private DroppedZone _holeContainerZone;
        [UnityEngine.SerializeField] private DroppedZone _shapesStorageZone;
        [UnityEngine.SerializeField] private ShapesTower _tower;
        [UnityEngine.SerializeField] private Commentator _commentator;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        private MainScreenPresenter _presenter;

        [Zenject.Inject] private readonly AudioService _audioService;
        [Zenject.Inject] private readonly LocalizeService _localizeService;

        private readonly CompositeDisposable _subscriptions = new();

        internal override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        internal UnityEngine.RectTransform ShapesContainer => _shapesContainer;

        internal UnityEngine.RectTransform DraggingShapeContainer => _draggingShapeContainer;

        internal UnityEngine.RectTransform TowerShapeContainer => _tower.ShapeContainer;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(IScreenPresenter presenter)
        {
            _presenter = presenter as MainScreenPresenter;
            _commentator.Init(_localizeService);
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

        internal override void Destroy()
        {
            _tower.Destroy();

            base.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool ContainsInTower(World.IShapePresenter draggableShape)
        {
            return _tower.Contains(draggableShape);
        }

        internal void AddShapeToTower(World.IShapePresenter shapePresenter, in UnityEngine.Vector2 startPosition)
        {
            _tower.Add(shapePresenter, in startPosition);

            _commentator.ShowAddShapeToTowerMessage();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void ExplodeShape(World.IShapePresenter shapePresenter, in UnityEngine.Vector2 startPosition, System.Action callback)
        {
            _tower.Explode(shapePresenter, in startPosition, callback);
        }

        internal void RemoveShapeFromTower(World.IShapePresenter draggableShape)
        {
            _tower.Remove(draggableShape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal UnityEngine.Vector2 GetTowerSize()
        {
            return _tower.GetAvailableSize();
        }

        internal void Clear()
        {
            var shapes = _tower.Shapes;
            var count = shapes.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                _presenter.RemoveShape(shapes[i]);

                shapes.RemoveAt(i);
            }
        }

        private void Awake()
        {
            _tower.Init();
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
            _holeContainerZone.Dropped.Subscribe(OnHoleContainerZoneDropped).AddTo(_subscriptions);
            _shapesStorageZone.Dropped.Subscribe(OnShapesStorageZoneDropped).AddTo(_subscriptions);
            _tower.Dragging.Subscribe(OnTowerShapeDragging).AddTo(_subscriptions);
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

        private void OnHoleZoneDropped(BaseDroppedZone zone)
        {
            if (_presenter.CheckHole())
                _commentator.ShowDroppedOnHoleMessage();
        }

        private void OnHoleContainerZoneDropped(BaseDroppedZone zone)
        {
            _commentator.ShowDroppedPastHoleMessage();
        }

        private void OnShapesStorageZoneDropped(BaseDroppedZone zone)
        {
            var status = _presenter.ResolveShape();

            _commentator.ShowResolverMessage(status);
        }

        private void OnTowerShapeDragging(World.DraggableShapeInfo info)
        {
            _presenter.CheckDraggingShape(in info);
        }
    }
}
