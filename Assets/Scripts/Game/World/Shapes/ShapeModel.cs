namespace Cubes.Game.World
{
    internal abstract class ShapeModel : IShapeModel
    {
        private Services.DragSource _dragSource;

        internal UniRx.ReactiveProperty<UnityEngine.Vector2> Position { get; private set; }

        internal UniRx.ReactiveProperty<UnityEngine.Vector2> DraggablePosition { get; private set; }

        internal Services.DragSource DragSource => _dragSource;

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
        internal void UpdateDragSource(Services.DragSource dragSource)
        {
            _dragSource = dragSource;
        }
    }
}
