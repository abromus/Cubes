using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class MainScreenModel : Services.IScreenModel
    {
        private Core.Services.SaveLoadService _saveLoadService;
        private Data.MainScreenData _data;
        private IShapePresenter _draggableShape;
        private DragSource _dragSource;

        private readonly System.Collections.Generic.List<IShapePresenter> _strorageShapes = new(32);
        private readonly System.Collections.Generic.List<IShapePresenter> _towerShapes = new(32);

        internal UniRx.ReactiveProperty<bool> IsShown { get; private set; }

        internal IShapePresenter DraggableShape => _draggableShape;

        internal DragSource DragSource => _dragSource;

        internal System.Collections.Generic.List<Data.ShapeDataArgs> TowerShapeArgs => _data.TowerShapeArgs;

        [Zenject.Inject]
        [UnityEngine.Scripting.Preserve]
        public MainScreenModel()
        {
            IsShown = new UniRx.ReactiveProperty<bool>(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init(Core.Services.SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateIsShown(bool isShown)
        {
            IsShown.Value = isShown;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddStorageShape(IShapePresenter shape)
        {
            _strorageShapes.Add(shape);
        }

        internal void AddTowerShape(IShapePresenter shape, bool withArgs = true)
        {
            _towerShapes.Add(shape);

            var position = shape.Position;
            var args = new Data.ShapeDataArgs(shape.ShapeType, position.x, position.y, shape.Config.Id);
            _data.AddTowerShape(shape);

            if (withArgs == false)
                return;

            _data.AddTowerShapeArgs(in args);
        }

        internal void RemoveTowerShape(IShapePresenter shape)
        {
            _towerShapes.Remove(shape);
            _data.RemoveTowerShape(shape);
        }

        internal void UpdateDraggableShape(IShapePresenter draggableShape, DragSource dragSource)
        {
            _draggableShape = draggableShape;
            _dragSource = dragSource;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void LoadData()
        {
            if (_saveLoadService.TryLoad(Keys.FileName, out _data) == false)
                _data = new();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void SaveData()
        {
            _saveLoadService.Save(_data, Keys.FileName);
        }

        private sealed class Keys
        {
            internal const string FileName = "MainScreenData.json";
        }
    }
}
