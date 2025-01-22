namespace Cubes.Game.UI.MainScreen.Shapes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct DraggableShapeInfo
    {
        private bool _isDragging;

        private readonly DragSource _dragSource;
        private readonly IShapePresenter _shapePresenter;

        internal readonly IShapePresenter ShapePresenter => _shapePresenter;

        internal readonly DragSource DragSource => _dragSource;

        internal readonly bool IsDragging => _isDragging;

        internal DraggableShapeInfo(IShapePresenter shapePresenter, DragSource dragSource, bool isDragging)
        {
            _shapePresenter = shapePresenter;
            _dragSource = dragSource;
            _isDragging = isDragging;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void SetDragging(bool isDragging)
        {
            _isDragging = isDragging;
        }
    }
}
