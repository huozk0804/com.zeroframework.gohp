using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Keystone.Editor.Package
{
    public class DragDrawer : PointerManipulator
    {
        private readonly Action<Vector2> callback;

        private bool enabled;
        private Vector2 offset = Vector2.zero;
        private Vector2 delta = Vector2.zero;

        public DragDrawer(VisualElement target, Action<Vector2> callback)
        {
            this.callback = callback;
            this.target = target;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
            target.RegisterCallback<PointerLeaveEvent>(PointerLeveHandler);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        private void PointerDownHandler(PointerDownEvent evt)
        {
            enabled = true;
        }

        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (!enabled)
                return;

            delta += (Vector2) evt.deltaPosition;
            callback(offset + delta);
        }

        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (!enabled)
                return;

            enabled = false;
            offset += delta;
            delta = Vector2.zero;
        }

        private void PointerLeveHandler(PointerLeaveEvent evt)
        {
            if (!enabled)
                return;

            enabled = false;
            offset += delta;
            delta = Vector2.zero;
        }

        private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
        {
            if (!enabled)
                return;

            enabled = false;
            offset += delta;
            delta = Vector2.zero;
        }

        public void Reset()
        {
            enabled = false;
            offset = Vector2.zero;
            delta = Vector2.zero;

            callback(Vector2.zero);
        }
    }
}
