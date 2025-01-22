using Cubes.Game.UI.MainScreen.Shapes;
using UniRx;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class MainScreenView : Services.BaseScreenView
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _shapesContainer;
        [UnityEngine.SerializeField] private UnityEngine.RectTransform _draggingShapeContainer;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private HoleDroppedZone _holeZone;
        [UnityEngine.SerializeField] private DroppedZone _holeContainerZone;
        [UnityEngine.SerializeField] private DroppedZone _shapesStorageZone;
        [UnityEngine.SerializeField] private ShapesTower _tower;
        [UnityEngine.SerializeField] private Commentator _commentator;
        [UnityEngine.SerializeField] private Audio _audio;

        private MainScreenPresenter _presenter;

        [Zenject.Inject] private readonly Services.AudioService _audioService;
        [Zenject.Inject] private readonly Services.LocalizeService _localizeService;

        private readonly CompositeDisposable _subscriptions = new();

        internal override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        internal UnityEngine.RectTransform ShapesContainer => _shapesContainer;

        internal UnityEngine.RectTransform DraggingShapeContainer => _draggingShapeContainer;

        internal UnityEngine.RectTransform TowerShapeContainer => _tower.ShapeContainer;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(Services.IScreenPresenter presenter)
        {
            _presenter = presenter as MainScreenPresenter;
            _commentator.Init(_localizeService);
            _audio.Init(_audioService);
        }

        internal override void Show()
        {
            base.Show();

            _audio.PlayBackgroundMusic();

            Subscribe();
        }

        internal override void Hide()
        {
            base.Hide();

            _audio.StopBackgroundMusic();

            Unsubscribe();
        }

        internal override void Destroy()
        {
            _tower.Destroy();

            base.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal bool ContainsInTower(IShapePresenter draggableShape)
        {
            return _tower.Contains(draggableShape);
        }

        internal void AddShapeToTower(IShapePresenter shapePresenter, in UnityEngine.Vector2 startPosition)
        {
            _tower.Add(shapePresenter, in startPosition);
            _commentator.ShowAddShapeToTowerMessage();
            _audio.PlaySound(SoundType.Jump);
        }

        internal void ExplodeShape(IShapePresenter shapePresenter, in UnityEngine.Vector2 startPosition, System.Action callback)
        {
            _tower.Explode(shapePresenter, in startPosition, Falled, callback);

            void Falled()
            {
                _audio.PlaySound(SoundType.Explode);
            }
        }

        internal void RemoveShapeFromTower(IShapePresenter draggableShape)
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
            if (_presenter.CheckHole() == false)
                return;

            _commentator.ShowDroppedOnHoleMessage();
            _audio.PlaySound(SoundType.Hole);
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

        private void OnTowerShapeDragging(DraggableShapeInfo info)
        {
            _presenter.CheckDraggingShape(in info);
        }
    }
}
