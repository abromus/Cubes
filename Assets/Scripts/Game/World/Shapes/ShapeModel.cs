namespace Cubes.Game.World
{
    internal abstract class ShapeModel : IShapeModel
    {
        internal UniRx.ReactiveProperty<UnityEngine.Vector2> Position { get; private set; }

        internal UniRx.ReactiveProperty<UnityEngine.Vector2> DraggablePosition { get; private set; }

        [UnityEngine.Scripting.Preserve]
        internal ShapeModel()
        {
            Position = new UniRx.ReactiveProperty<UnityEngine.Vector2>(UnityEngine.Vector2.zero);
            DraggablePosition = new UniRx.ReactiveProperty<UnityEngine.Vector2>(UnityEngine.Vector2.zero);
        }

        internal void UpdatePosition(in UnityEngine.Vector2 position)
        {
            Position.Value = position;
        }

        internal void UpdateDraggablePosition(in UnityEngine.Vector2 position)
        {
            DraggablePosition.Value = position;
        }
    }
}
