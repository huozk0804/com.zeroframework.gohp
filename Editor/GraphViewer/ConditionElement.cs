using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class ConditionElement : VisualElement
    {
        private readonly EditorWindowValues values;
        public INodeCondition GraphCondition { get; }

        public Circle Circle { get; set; }

        public Label Label { get; set; }

        public ConditionElement(INodeCondition graphCondition, EditorWindowValues values)
        {
            this.values = values;
            GraphCondition = graphCondition;
            AddToClassList("condition");

            Circle = new Circle(GetCircleColor(graphCondition), 10f);
            Add(Circle);
            
            Label = new Label(GetText(graphCondition.Condition));
            Add(Label);

            schedule.Execute(() =>
            {
                Label.text = GetText(GraphCondition.Condition);
                Circle.SetColor(GetCircleColor(GraphCondition));
            }).Every(33);
        }

        private Color GetCircleColor(INodeCondition condition)
        {
            if (Application.isPlaying)
                return GetLiveColor();
            
            if (!condition.Connections.Any())
                return Color.red;
            
            return Color.green;
        }

        private Color GetLiveColor()
        {
            if (values.SelectedObject is not IMonoGoapActionProvider agent)
                return Color.white;
            
            if (agent.AgentType == null)
                return Color.white;
                
            var conditionObserver = agent.AgentType.GoapConfig.ConditionObserver;
            conditionObserver.SetWorldData(agent.WorldData);
            
            return conditionObserver.IsMet(GraphCondition.Condition) ? Color.green : Color.red;
        }
        
        private string GetText(ICondition condition)
        {
            var suffix = GetSuffix(condition);
            
            return $"{condition.WorldKey.Name} {GetText(condition.Comparison)} {condition.Amount} {suffix}";
        }

        private string GetSuffix(ICondition condition)
        {
            if (!Application.isPlaying)
                return "";
            
            if (values.SelectedObject is not IMonoGoapActionProvider agent)
                return "";
            
            var (exists, value) = agent.WorldData.GetWorldValue(condition.WorldKey);
            
            return "(" + (exists ? value.ToString() : "-") + ")";
        }
        
        private string GetText(Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.GreaterThan:
                    return ">";
                case Comparison.GreaterThanOrEqual:
                    return ">=";
                case Comparison.SmallerThan:
                    return "<";
                case Comparison.SmallerThanOrEqual:
                    return "<=";
            }
            
            return "";
        }
    }
}