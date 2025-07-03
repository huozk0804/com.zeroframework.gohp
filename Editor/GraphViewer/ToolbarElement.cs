using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public class ToolbarElement : Toolbar
    {
        public ToolbarElement(EditorWindowValues values)
        {
            Add(new ToolbarButton(() =>
            {
                Selection.activeObject = values.SelectedObject;
            })
            {
                text = values.SelectedObject.name,
            });

            Add(new ToolbarButton(() =>
            {
                var elementsWithClass = values.RootElement.Query<VisualElement>(className: "node").ToList();

                foreach (var element in elementsWithClass)
                {
                    element.AddToClassList("collapsed");
                }
            })
            {
                text = "collapse",
            });

            Add(new ToolbarButton(() =>
            {
                var elementsWithClass = values.RootElement.Query<VisualElement>(className: "node").ToList();

                foreach (var element in elementsWithClass)
                {
                    element.RemoveFromClassList("collapsed");
                }
            })
            {
                text = "open",
            });

            ToolbarButton configToggle = null;
            configToggle = new ToolbarButton(() =>
            {
                values.ShowConfig = !values.ShowConfig;
                configToggle.text = values.ShowConfig ? "Config (true)" : "Config (false)";
            })
            {
                text = "toggle config",
            };

            Add(configToggle);

            var spacer = new VisualElement();
            spacer.style.flexGrow = 1; // This makes the spacer flexible, filling available space
            Add(spacer);

            Add(new ToolbarButton(() =>
            {
                values.UpdateZoom(10);
            })
            {
                text = "+",
            });
            Add(new ToolbarButton(() =>
            {
                values.UpdateZoom(-10);
            })
            {
                text = "-",
            });
            Add(new ToolbarButton(() =>
            {
                values.Zoom = 100;
                values.DragDrawer.Reset();
            })
            {
                text = "reset",
            });
        }
    }
}
