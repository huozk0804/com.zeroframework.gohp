using System.Linq;
using UnityEditor;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class GoalList : ListElementBase<CapabilityGoal, CapabilityGoalElement>
    {
        private readonly CapabilityConfigScriptable scriptable;
        private readonly GeneratorScriptable generator;

        public GoalList(SerializedObject serializedObject, CapabilityConfigScriptable scriptable, GeneratorScriptable generator) : base(serializedObject.FindProperty("goals"), scriptable.goals)
        {
            this.scriptable = scriptable;
            this.generator = generator;
            
            Rebuild();
        }

        protected override CapabilityGoalElement CreateListItem(SerializedProperty property, CapabilityGoal item)
        {
            return new CapabilityGoalElement(property, scriptable, generator, item);
        }

        protected override void BindListItem(SerializedProperty property, CapabilityGoalElement element, CapabilityGoal item, int index)
        {
            element.Foldout.text = item.goal.Name;
            
            element.Goal.Bind(scriptable, item.goal, generator.GetGoals().ToArray(), classRef =>
            {
                element.Foldout.text = item.goal.Name;
            });
        }
    }
}