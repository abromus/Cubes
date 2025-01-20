namespace Cubes.Core
{
    public sealed class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly System.Collections.Generic.List<T> _objects;
        private readonly System.Func<T> _createFunc;

        public ObjectPool(System.Func<T> createFunc, int capacity = 10)
        {
            _createFunc = createFunc ?? throw new System.ArgumentNullException(nameof(createFunc));

            _objects = new(capacity);
        }

        public T Get()
        {
            T value;

            if (_objects.Count != 0)
            {
                var index = _objects.Count - 1;
                value = _objects[index];

                _objects.RemoveAt(index);
            }
            else
            {
                value = _createFunc();
            }

            return value;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Release(T pooledObject)
        {
            _objects.Add(pooledObject);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            _objects.Clear();
        }
    }
}
