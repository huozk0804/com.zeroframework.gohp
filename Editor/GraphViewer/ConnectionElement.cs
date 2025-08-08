using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class ConnectionElement : VisualElement
    {
        private readonly NodeElement fromNode;
        private readonly NodeElement toNode;
        private readonly ConditionElement condition;
        private readonly EditorWindowValues values;

        public BezierDrawer Bezier { get; set; }
        public Label Cost { get; set; }

        public ConnectionElement(NodeElement fromNode, ConditionElement condition, NodeElement toNode, EditorWindowValues values)
        {
            this.fromNode = fromNode;
            this.condition = condition;
            this.toNode = toNode;
            this.values = values;

            Bezier = new BezierDrawer();
            Add(Bezier);

            Cost = new Label("")
            {
                style =
                {
                    position = Position.Absolute,
                    top = 0,
                    left = 0,
                },
            };
            Cost.AddToClassList("distance-cost");
            Add(Cost);

            Update();

            this.values.OnUpdate += Update;

            schedule.Execute(Update).Every(100);
        }

        public void Update()
        {
            var magicOffset = 10f;

            var start = condition;
            var end = toNode.Node;

            var startPos = new Vector3(condition.parent.parent.worldBound.position.x + condition.parent.parent.worldBound.width, start.worldBound.position.y - magicOffset, 0);
            var endPos = new Vector3(end.worldBound.position.x, end.worldBound.position.y - magicOffset, 0);

            var startTan = startPos + (Vector3.right * 40);
            var endTan = endPos + (Vector3.left * 40);

            Bezier.Update(startPos, endPos, startTan, endTan, 2f, GetColor());

            var center = Bezier.GetCenter();
            var cost = GetCost();
            var length = cost.Length * 5;

            Cost.style.left = center.x - length;
            Cost.style.top = center.y;
            Cost.text = cost;
            Cost.visible = !string.IsNullOrEmpty(cost);
        }

        private Color GetColor()
        {
            if (values.SelectedObject == null)
                return Color.black;

            if (values.SelectedObject is not IMonoGoapActionProvider agent)
                return Color.black;

            var actions = agent.CurrentPlan?.Plan;

            if (actions == null)
                return Color.black;

            if (actions.Contains(toNode.GraphNode.Action))
                return new Color(0, 157, 100);

            return Color.black;
        }

        private string GetCost()
        {
            if (values.SelectedObject is not IMonoGoapActionProvider agent)
                return "";

            if (!Application.isPlaying)
                return "";

            var fromAction = fromNode.GraphNode.Action as IGoapAction;
            var toAction = toNode.GraphNode.Action as IGoapAction;

            if (fromAction == null || toAction == null)
                return "";

            var startVector = agent.WorldData.GetTarget(fromAction);
            var endVector = agent.WorldData.GetTarget(toAction);

            if (startVector == null || endVector == null)
                return "";

            if (!startVector.IsValid() || !endVector.IsValid())
                return "";

            var distance = Vector3.Distance(startVector.Position, endVector.Position);
            var cost = agent.DistanceMultiplier * distance;

            return cost.ToString("F2");
        }
    }
}
