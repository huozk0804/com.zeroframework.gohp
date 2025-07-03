using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    public class ConditionList : ListElementBase<CapabilityCondition, CapabilityConditionElement>
    {
        private readonly CapabilityConfigScriptable scriptable;
        private readonly GeneratorScriptable generator;

        public ConditionList(SerializedProperty serializedProperty, CapabilityConfigScriptable scriptable, GeneratorScriptable generator, List<CapabilityCondition> conditions) : base(serializedProperty.FindPropertyRelative("conditions"), conditions)
        {
            this.scriptable = scriptable;
            this.generator = generator;
            
            Rebuild();
        }


        protected override CapabilityConditionElement CreateListItem(SerializedProperty property, CapabilityCondition item)
        {
            return new CapabilityConditionElement(scriptable, generator);
        }

        protected override void BindListItem(SerializedProperty property,  CapabilityConditionElement element, CapabilityCondition item, int index)
        {
            element.Foldout.text = item.ToString();
            
            element.WorldKeyField.Bind(scriptable, item.worldKey, generator.GetWorldKeys().ToArray(), classRef =>
            {
                element.Foldout.text = item.ToString();
            });
            
            element.ComparisonField.value = item.comparison;
            element.ComparisonField.RegisterValueChangedCallback(evt =>
            {
                item.comparison = (Comparison) evt.newValue;
                element.Foldout.text = item.ToString();
                EditorUtility.SetDirty(scriptable);
            });
            
            element.AmountField.value = item.amount;
            element.AmountField.RegisterValueChangedCallback(evt =>
            {
                item.amount = evt.newValue;
                element.Foldout.text = item.ToString();
                EditorUtility.SetDirty(scriptable);
            });
        }
    }
}