using UnityEditor;
using UnityEngine.UIElements;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    public class CapabilityGoalElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; private set; }
        public ClassRefField Goal { get; private set; }
        
        public CapabilityGoalElement(SerializedProperty serializedProperty, CapabilityConfigScriptable scriptable, GeneratorScriptable generator, CapabilityGoal item)
        {
            Foldout = new Foldout
            {
                value = false
            };
            Add(Foldout);

            var card = new Card((card) =>
            {
                var goal = new LabeledField<ClassRefField>("Goal", new ClassRefField());
                Goal = goal.Field;
                card.Add(goal);

                card.Add(new Label("Conditions"));
                var conditions = new ConditionList(serializedProperty, scriptable, generator, item.conditions);
                card.Add(conditions);
            });
            
            Foldout.Add(card);
        }
    }
}