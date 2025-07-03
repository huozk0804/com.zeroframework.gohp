using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using ZeroFramework.Goap;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Editor.Package
{
    public class ActionPropertiesElement : VisualElement
    {
        private readonly SerializedObject serializedObject;
        private PropertyField props;

        public ActionPropertiesElement(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
        }

        public void Bind(SerializedProperty property, CapabilityAction action, Script[] actions)
        {
            validate(action, actions);

            Render(action.properties, property.FindPropertyRelative("properties"));
        }

        private void Render(IActionProperties props, SerializedProperty property)
        {
            Clear();

            if (props == null)
                return;

            var type = props.GetType();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            Add(new Label("Action props"));
            Add(new Card((card) =>
            {
                if (properties.Length == 0 && fields.Length == 0)
                {
                    card.Add(new Label("No properties"));
                    return;
                }

                foreach (var propertyInfo in properties)
                {
                    AddProp(card, property.FindPropertyRelative(propertyInfo.Name));
                }

                foreach (var propertyInfo in fields)
                {
                    AddProp(card, property.FindPropertyRelative(propertyInfo.Name));
                }
            }));
        }

        private void AddProp(Card card, SerializedProperty prop)
        {
            var field = new PropertyField(prop);
            field.BindProperty(prop);
            field.Bind(serializedObject);
            card.Add(field);
        }

        private void validate(CapabilityAction action, Script[] actions)
        {
            var (status, script) = action.action.GetMatch(actions);

            if (status != ClassRefStatus.Full)
                return;

            var type = GetPropertiesType(script);

            if (type == null)
                return;


            if (action.properties?.GetType() == type)
            {
                return;
            }

            action.properties = (IActionProperties) Activator.CreateInstance(type);
        }

        private System.Type GetPropertiesType(Script script)
        {
            if (script == null)
                return null;

            if (script.Type == null)
                return null;

            return script.Type.GetPropertiesType();
        }
    }
}
