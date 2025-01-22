using Cubes.Game.UI.MainScreen.Shapes;

namespace Cubes.Game.UI.MainScreen
{
    internal sealed class MainScreenModel : Services.IScreenModel
    {
        private IShapePresenter _draggableShape;
        private DragSource _dragSource;

        private readonly System.Collections.Generic.List<IShapePresenter> _shapes = new(32);

        internal UniRx.ReactiveProperty<bool> IsShown { get; private set; }

        internal IShapePresenter DraggableShape => _draggableShape;

        internal DragSource DragSource => _dragSource;

        [UnityEngine.Scripting.Preserve]
        public MainScreenModel()
        {
            IsShown = new UniRx.ReactiveProperty<bool>(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateIsShown(bool isShown)
        {
            IsShown.Value = isShown;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddShape(IShapePresenter shape)
        {
            _shapes.Add(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateDraggableShape(IShapePresenter draggableShape, DragSource dragSource)
        {
            _draggableShape = draggableShape;
            _dragSource = dragSource;
        }
    }
}
