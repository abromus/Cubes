namespace Cubes.Game.UI.MainScreen
{
    internal sealed class MainScreenDataLoader
    {
        private readonly MainScreenModel _model;
        private readonly MainScreenView _view;
        private readonly ShapeResolver _resolver;
        private readonly ShapePool _pool;
        private readonly Configs.IShapesConfig _config;

        internal MainScreenDataLoader(
            MainScreenModel model,
            MainScreenView view,
            ShapeResolver resolver,
            ShapePool pool,
            Configs.IShapesConfig config)
        {
            _model = model;
            _view = view;
            _resolver = resolver;
            _pool = pool;
            _config = config;
        }

        internal void LoadData()
        {
            _model.LoadData();

            var args = _model.TowerShapeArgs;

            for (int i = 0; i < args.Count; i++)
            {
                var info = args[i];
                var position = new UnityEngine.Vector2(info.PositionX, info.PositionY);
                var config = GetConfig(info.ConfigId);
                var shape = _pool.Get(info.Type);
                shape.UpdateConfig(config);
                shape.UpdatePosition(position);
                shape.Show();

                _model.AddTowerShape(shape, false);
                _resolver.AddShape(shape);
                _view.AddShapeToTower(shape, in position, true);
            }
        }

        private Configs.ShapeInfo GetConfig(int id)
        {
            var infos = _config.ShapeInfos;
            var count = infos.Length;

            for (int i = 0; i < count; i++)
            {
                var config = infos[i];

                if (config.Id == id)
                    return config;
            }

#if UNITY_EDITOR
            UnityEngine.Debug.LogError($"[MainScreenDataLoader]: Config with id {id} not found");
#endif

            return default;
        }
    }
}
