using UnityEditor;
using UnityEngine.UIElements;
using Keystone.Goap;
using Keystone.Goap.Agent;

#if UNITY_2021
using UnityEditor.UIElements;
#endif

namespace Keystone.Editor.Package
{
    public class CapabilityActionElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; private set; }
        public ClassRefField Action { get; private set; }
        public FloatField BaseCostField { get; private set; }
        public ClassRefField Target { get; private set; }

        public FloatField InRangeField { get; set; }

        public Toggle ValidateConditionsField { get; set; }
        public Toggle ValidateTargetField { get; set; }

        public Toggle RequiresTargetField { get; set; }

        public EnumField MoveModeField { get; set; }
        public ActionPropertiesElement Properties { get; private set; }

        
        public CapabilityActionElement(SerializedObject serializedObject, SerializedProperty serializedProperty,
            CapabilityConfigScriptable scriptable, GeneratorScriptable generator, CapabilityAction item)
        {
            Foldout = new Foldout
            {
                value = false
            };
            Add(Foldout);

            var card = new Card((card) =>
            {
                var action = new LabeledField<ClassRefField>("Action", new ClassRefField());
                Action = action.Field;
                card.Add(action);

                var target = new LabeledField<ClassRefField>("Target", new ClassRefField());
                Target = target.Field;
                card.Add(target);

                var baseCost = new LabeledField<FloatField>("Base Cost");
                BaseCostField = baseCost.Field;
                card.Add(baseCost);

                var inRange = new LabeledField<FloatField>("In Range");
                InRangeField = inRange.Field;
                card.Add(inRange);
                
                var requiresTarget = new LabeledField<Toggle>("Requires Target", new Toggle());
                RequiresTargetField = requiresTarget.Field;
                card.Add(requiresTarget);
                
                var validateConditions = new LabeledField<Toggle>("Validate Conditions", new Toggle());
                ValidateConditionsField = validateConditions.Field;
                card.Add(validateConditions);
                
                var validateTarget = new LabeledField<Toggle>("Validate Target", new Toggle());
                ValidateTargetField = validateTarget.Field;
                card.Add(validateTarget);

                var moveMode = new LabeledField<EnumField>("Move Mode", new EnumField(ActionMoveMode.MoveBeforePerforming));
                MoveModeField = moveMode.Field;
                card.Add(moveMode);

                card.Add(new Label("Effects"));
                var effects = new EffectList(serializedProperty, scriptable, generator, item.effects);
                card.Add(effects);

                card.Add(new Label("Conditions"));
                var conditions = new ConditionList(serializedProperty, scriptable, generator, item.conditions);
                card.Add(conditions);
                
                var properties = new ActionPropertiesElement(serializedObject);
                Properties = properties;
                card.Add(properties);
            });
            
            Foldout.Add(card);
            
            Foldout.RegisterValueChangedCallback(evt => 
            {
                // Optional: You might need to trigger a layout update on the root
                // this.parent.parent.MarkDirtyRepaint();
            });
        }

    }
}