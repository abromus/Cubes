using Newtonsoft.Json;

namespace Cubes.Game.Data
{
    [System.Serializable]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
    internal struct ShapeDataArgs
    {
        [JsonProperty] private float _positionX;
        [JsonProperty] private float _positionY;
        [JsonProperty] private readonly Configs.ShapeType _type;
        [JsonProperty] private readonly int _configId;

        internal readonly Configs.ShapeType Type => _type;

        internal readonly float PositionX => _positionX;

        internal readonly float PositionY => _positionY;

        internal readonly int ConfigId => _configId;

        internal ShapeDataArgs(
            Configs.ShapeType type,
            float positionX,
            float positionY,
            int configId)
        {
            _type = type;
            _positionX = positionX;
            _positionY = positionY;
            _configId = configId;
        }

        internal void UpdatePosition(in UnityEngine.Vector2 position)
        {
            _positionX = position.x;
            _positionY = position.y;
        }
    }
}
