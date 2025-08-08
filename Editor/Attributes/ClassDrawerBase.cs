using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Keystone.Editor.Package
{
    public abstract class ClassDrawerBase<TClassType> : PropertyDrawer
    {
        private List<string> types = new();

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.stringValue = DrawTypeSelect(position, property.stringValue, GetAllOfType<TClassType>());
        }

        private string DrawTypeSelect(Rect position, string currentValue, List<string> options)
        {
            var index = options.FindIndex(x => x == currentValue);

            index = EditorGUI.Popup(position, "Class", index, options.ToArray());

            if (index == -1)
                return "";

            return options[index];
        }

        private List<string> GetAllOfType<TType>()
        {
            if (!types.Any())
            {
                types = LoadAllOfType<TType>();
            }

            return types;
        }

        private List<string> LoadAllOfType<TType>()
        {
            var type = typeof(TType);

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !a.FullName.ToLower().Contains("test"))
                .SelectMany(a => a.GetTypes())
                .Where(t => type.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .Select(x => x.AssemblyQualifiedName).ToList();
        }
    }
}