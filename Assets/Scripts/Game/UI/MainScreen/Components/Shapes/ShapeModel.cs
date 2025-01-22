namespace Cubes.Game.UI.MainScreen.Shapes
{
    internal abstract class ShapeModel : IShapeModel
    {
        private DragSource _dragSource;

        internal UniRx.ReactiveProperty<UnityEngine.Vector2> Position { get; private set; }

        internal UniRx.ReactiveProperty<UnityEngine.Vector2> DraggablePosition { get; private set; }

        internal DragSource DragSource => _dragSource;

        [UnityEngine.Scripting.Preserve]
        internal ShapeModel()
        {
            Position = new UniRx.ReactiveProperty<UnityEngine.Vector2>(UnityEngine.Vector2.zero);
            DraggablePosition = new UniRx.ReactiveProperty<UnityEngine.Vector2>(UnityEngine.Vector2.zero);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdatePosition(in UnityEngine.Vector2 position)
        {
            Position.Value = position;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateDraggablePosition(in UnityEngine.Vector2 position)
        {
            DraggablePosition.Value = position;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void UpdateDragSource(DragSource dragSource)
        {
            _dragSource = dragSource;
        }
    }
}
