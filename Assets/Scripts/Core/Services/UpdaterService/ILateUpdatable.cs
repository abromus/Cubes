namespace Cubes.Core.Services
{
    public interface ILateUpdatable
    {
        public void LateTick(float deltaTime);
    }
}
