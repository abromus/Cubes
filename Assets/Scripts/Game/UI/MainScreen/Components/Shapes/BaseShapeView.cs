namespace Cubes.Game.UI.MainScreen.Shapes
{
    internal abstract class BaseShapeView : UnityEngine.MonoBehaviour, IShapeView
    {
        private IShapePresenter _presenter;

        internal abstract Configs.ShapeType ShapeType { get; }

        internal IShapePresenter Presenter => _presenter;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public virtual void Init(IShapePresenter presenter)
        {
            _presenter = presenter;
        }

        public abstract void UpdateConfig(in Configs.ShapeInfo info);

        internal virtual void Destroy()
        {
            if (this != null && gameObject != null)
                Destroy(gameObject);
        }
    }
}
