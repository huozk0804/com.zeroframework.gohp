using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class ObjectDrawer : VisualElement
    {
        public ObjectDrawer(object obj)
        {
            if (obj is null)
                return;

            var properties = obj.GetType().GetProperties();

            var label = new Label();
            label.text = GetLabelText(properties, obj);
            Add(label);

            schedule.Execute(() =>
            {
                label.text = GetLabelText(properties, obj);
            }).Every(33);
        }

        private string GetLabelText(PropertyInfo[] properties, object obj)
        {
            return string.Join("\n", properties.Select(x =>
            {
                var value = x.GetValue(obj);
                return $"{x.Name}: {GetValueString(value)}";
            }));
        }

        private string GetValueString(object value)
        {
            if (value == null)
                return "null";

            if (value is TransformTarget transformTarget)
            {
                if (transformTarget.Transform == null)
                    return "null";

                return transformTarget.Transform.name;
            }

            if (value is PositionTarget positionTarget)
                return positionTarget.GetValidPosition().ToString();

            if (value is MonoBehaviour monoBehaviour)
            {
                if (monoBehaviour == null)
                    return "null";

                return monoBehaviour.name;
            }

            if (value is ScriptableObject scriptableObject)
            {
                if (scriptableObject == null)
                    return "null";

                return scriptableObject.name;
            }

            return value.ToString();
        }
    }
}
