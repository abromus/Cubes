namespace Cubes.Core
{
    public interface IObjectPool<T> where T : class
    {
        public T Get();

        public void Release(T pooledObject);
    }
}
