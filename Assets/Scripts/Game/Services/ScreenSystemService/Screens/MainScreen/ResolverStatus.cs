namespace Cubes.Game.Services
{
    internal enum ResolverStatus
    {
        None = 0,
        Successful = 1,
        MinShapeCountRestriction = 2,
        IntersectionRestriction = 3,
        ContainerSizeRestriction = 4,
    }
}
