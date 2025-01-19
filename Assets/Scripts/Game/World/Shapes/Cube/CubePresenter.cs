﻿using System;
using UniRx;

namespace Cubes.Game.World
{
    internal sealed class CubePresenter : BaseShapePresenter
    {
        private CubeModel _model;
        private CubeView _view;
        private UnityEngine.RectTransform _screenRectTransform;
        private DraggableShapeInfo _info;

        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<DraggableShapeInfo> _dragging = new();

        public override UnityEngine.RectTransform ScreenRectTransform => _screenRectTransform;

        public override UnityEngine.RectTransform DraggableShapeParent => _view.DraggableShapeParent;

        public override Subject<DraggableShapeInfo> Dragging => _dragging;

        public override void Init(IShapeModel model, BaseShapeView view, UnityEngine.RectTransform screenRectTransform)
        {
            _model = model as CubeModel;
            _view = view as CubeView;
            _screenRectTransform = screenRectTransform;

            _info = new DraggableShapeInfo(this, false);

            Subscribe();
        }

        public override void Destroy()
        {
            _view.Destroy();

            Unsubscribe();
        }

        public override void UpdatePosition(UnityEngine.EventSystems.PointerEventData eventData)
        {
            UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _screenRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out UnityEngine.Vector2 position);

            _model.UpdatePosition(in position);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdatePosition(in UnityEngine.Vector2 position)
        {
            _model.UpdatePosition(in position);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateParent(UnityEngine.Transform parent)
        {
            _view.UpdateParent(parent);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void UpdateDraggableParent(UnityEngine.Transform parent)
        {
            _view.UpdateDraggableParent(parent);
        }

        private void Subscribe()
        {
            _model.Position.Subscribe(OnUpdatePosition).AddTo(_subscriptions);
            _view.Dragging.Subscribe(OnDragging).AddTo(_subscriptions);
        }

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnUpdatePosition(UnityEngine.Vector2 position)
        {
            _view.UpdateDragPosition(in position);
        }

        private void OnDragging(bool isDragging)
        {
            _info.SetDragging(isDragging);
            _dragging.OnNext(_info);
        }
    }
}
