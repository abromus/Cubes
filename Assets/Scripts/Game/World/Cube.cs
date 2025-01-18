namespace Cubes.Game
{
    internal sealed class Cube : UnityEngine.MonoBehaviour, IShape
    {
        [UnityEngine.SerializeField] private UnityEngine.SpriteRenderer _renderer;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetColor(UnityEngine.Color color)
        {
            _renderer.color = color;
        }
    }
}
