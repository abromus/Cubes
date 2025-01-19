namespace Cubes.Game.Services
{
    internal sealed class IntersectionRestriction : IRestriction
    {
        private readonly UnityEngine.Vector3[] _firstShapeCorners = new UnityEngine.Vector3[4];
        private readonly UnityEngine.Vector3[] _secondShapeCorners = new UnityEngine.Vector3[4];

        public bool Check(World.IShapePresenter lastShape, World.IShapePresenter shape)
        {
            lastShape.RectTransform.GetWorldCorners(_firstShapeCorners);
            shape.DraggableRectTransform.GetWorldCorners(_secondShapeCorners);

            return IsRectIntersecting(_firstShapeCorners, _secondShapeCorners);
        }

        private bool IsRectIntersecting(UnityEngine.Vector3[] firstShapeCorners, UnityEngine.Vector3[] secondShapeCorners)
        {
            var minXIndex = 0;
            var maxXIndex = 2;

            return (firstShapeCorners[maxXIndex].x < secondShapeCorners[minXIndex].x || secondShapeCorners[maxXIndex].x < firstShapeCorners[minXIndex].x) == false;
        }
    }
}
