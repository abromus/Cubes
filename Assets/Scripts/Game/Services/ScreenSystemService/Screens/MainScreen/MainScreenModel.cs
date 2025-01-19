namespace Cubes.Game.Services
{
    internal sealed class MainScreenModel : IScreenModel
    {
        private World.IShapePresenter _draggableShape;

        private readonly System.Collections.Generic.List<World.IShapePresenter> _shapes = new(32);

        internal UniRx.ReactiveProperty<bool> IsShown { get; private set; }

        internal World.IShapePresenter DraggableShape => _draggableShape;

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
        internal void AddShape(World.IShapePresenter shape)
        {
            _shapes.Add(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateDraggableShape(World.IShapePresenter draggableShape)
        {
            _draggableShape = draggableShape;
        }
    }
}
