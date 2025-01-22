using Cubes.Game.UI.MainScreen.Shapes;
using Newtonsoft.Json;

namespace Cubes.Game.Data
{
    [System.Serializable]
    internal sealed class MainScreenData : Core.Services.ISavableData
    {
        [JsonProperty] private readonly System.Collections.Generic.List<ShapeDataArgs> _towerShapeArgs = new(32);

        [JsonIgnore] private readonly System.Collections.Generic.List<IShapePresenter> _towerShapes = new(32);

        internal System.Collections.Generic.List<ShapeDataArgs> TowerShapeArgs => _towerShapeArgs;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddTowerShape(IShapePresenter shape)
        {
            _towerShapes.Add(shape);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void AddTowerShapeArgs(in ShapeDataArgs args)
        {
            _towerShapeArgs.Add(args);
        }

        internal void RemoveTowerShape(IShapePresenter shape)
        {
            var index = _towerShapes.IndexOf(shape);

            _towerShapeArgs.RemoveAt(index);
            _towerShapes.RemoveAt(index);

            UpdateArgs();
        }

        private void UpdateArgs()
        {
            for (int i = 0; i < _towerShapes.Count; i++)
            {
                var args = _towerShapeArgs[i];
                var position = _towerShapes[i].Position;
                args.UpdatePosition(in position);

                _towerShapeArgs[i] = args;
            }
        }
    }
}
