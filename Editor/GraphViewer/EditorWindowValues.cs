using System;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ZeroFramework.Editor.Package
{
    public class EditorWindowValues
    {
        public int Zoom { get; set; } = 100;
        public VisualElement RootElement { get; set; }
        public DragDrawer DragDrawer { get; set; }
        public Object SelectedObject { get; set; }
        public bool ShowConfig { get; set; }

        public void UpdateZoom(int zoom)
        {
            if (zoom > 0)
            {
                Zoom = Math.Min(100, Zoom + zoom);
                return;
            }

            Zoom = Math.Max(50, Zoom + zoom);
        }

        public delegate void UpdateEvent();

        public event UpdateEvent OnUpdate;

        public void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
