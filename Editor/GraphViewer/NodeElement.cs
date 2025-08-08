using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class NodeElement : VisualElement
    {
        public INode GraphNode { get; }

        public NodeElement(INode graphNode, VisualElement bezierRoot, EditorWindowValues values, INode[] shownList = null)
        {
            var list = shownList ?? Array.Empty<INode>();

            GraphNode = graphNode;
            AddToClassList("wrapper");

            NodeWrapper = new VisualElement();
            NodeWrapper.AddToClassList("node-wrapper");

            Node = new VisualElement();
            Node.AddToClassList("node");

            Content = new VisualElement();
            Content.AddToClassList("content");
            Title = new Label($"{graphNode.Action.GetType().GetGenericTypeName()}");
            Cost = new Label();

            var costWrapper = new VisualElement
            {
                style =
                {
                    alignItems = Align.FlexEnd,
                },
            };
            costWrapper.Add(Cost);

            Content.Add(new HorizontalSplitView(Title, costWrapper, 60));

            var targetWrapper = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                },
            };

            Target = new Label();

            if (graphNode.Action is IGoapAction)
            {
                TargetCircle = new Circle(Color.white, 10f)
                {
                    style =
                    {
                        marginRight = 4,
                        marginTop = 2,
                    },
                };
                targetWrapper.Add(TargetCircle);
                targetWrapper.Add(Target);
            }

            Content.Add(targetWrapper);

            Conditions = new VisualElement();
            Conditions.AddToClassList("conditions");
            Conditions.Add(new Label("Conditions"));

            Node.Add(Content);
            Node.Add(Conditions);

            Node.RegisterCallback<ClickEvent>(evt =>
            {
                Node.ToggleInClassList("collapsed");

                values.Update();
            });

            NodeWrapper.Add(Node);

            ChildWrapper = new VisualElement();
            ChildWrapper.AddToClassList("child-wrapper");

            Add(NodeWrapper);
            Add(ChildWrapper);

            foreach (var condition in graphNode.Conditions)
            {
                var conditionElement = new ConditionElement(condition, values);
                Conditions.Add(conditionElement);

                foreach (var connection in condition.Connections)
                {
                    if (list.Contains(connection))
                    {
                        Debug.Log($"Skipping connection {connection.Action?.GetType().GetGenericTypeName()} because it's already shown in the list. Recursive connection detected!");
                        continue;
                    }

                    var connectionNode = new NodeElement(connection, bezierRoot, values, new[] { connection }.Concat(list).ToArray());
                    ChildWrapper.Add(connectionNode);

                    bezierRoot.Add(new ConnectionElement(this, conditionElement, connectionNode, values));
                }
            }

            if (!Application.isPlaying)
            {
                if (graphNode.Action is IGoapAction goapAction)
                {
                    TargetCircle.SetColor(GetCircleColor(goapAction, null));
                    Target.text = Target.text = GetTargetText(values, null, goapAction);
                    Cost.text = $"Cost: {goapAction.Config.BaseCost}";
                }
            }

            Effects = new VisualElement();
            Effects.AddToClassList("effects");
            Effects.Add(new Label("Effects"));
            Node.Add(Effects);

            foreach (var effect in graphNode.Effects)
            {
                var effectElement = new EffectElement(effect);
                Effects.Add(effectElement);
            }

            schedule.Execute(() =>
            {
                if (!Application.isPlaying)
                    return;

                if (values.SelectedObject == null)
                    return;

                if (values.SelectedObject is not IMonoGoapActionProvider provider)
                    return;

                if (!provider.isActiveAndEnabled)
                    return;

                UpdateClasses(values, graphNode, provider);

                Cost.text = $"Cost: {graphNode.GetCost(provider):0.00}";

                if (graphNode.Action is IGoapAction action)
                {
                    TargetCircle.SetColor(GetCircleColor(action, provider));
                    Target.text = GetTargetText(values, provider, action);
                }
            }).Every(33);
        }

        private string GetTargetText(EditorWindowValues values, IMonoGoapActionProvider provider, IGoapAction action)
        {
            var targetConfig = action.Config.Target?.GetType().GetGenericTypeName();

            if (!Application.isPlaying || provider == null)
                return $"Target: {targetConfig}";

            var target = provider.WorldData.GetTarget(action);
            var targetText = target?.GetValidPosition()?.ToString() ?? "null";

            if (values.ShowConfig)
                return $"Target: {targetText} ({targetConfig})";

            return $"Target: {targetText}";
        }

        private Color GetCircleColor(IGoapAction goapAction, IMonoGoapActionProvider provider)
        {
            if (!Application.isPlaying)
                return Color.white;

            if (!goapAction.Config.RequiresTarget)
                return Color.green;

            if (provider.WorldData.GetTarget(goapAction) == null)
                return Color.red;

            return Color.green;
        }

        private void UpdateClasses(EditorWindowValues values, INode graphNode, IMonoGoapActionProvider provider)
        {
            Node.RemoveFromClassList("active");
            Node.RemoveFromClassList("disabled");
            Node.RemoveFromClassList("path");
            Node.RemoveFromClassList("hide-effects");

            if (!values.ShowConfig)
            {
                Node.AddToClassList("hide-effects");
            }

            if (provider.CurrentPlan?.Goal == GraphNode.Action)
            {
                Node.AddToClassList("path");
                return;
            }

            if (provider.Receiver?.ActionState.Action == GraphNode.Action)
            {
                Node.AddToClassList("active");
                return;
            }

            if (provider.CurrentPlan?.Plan != null && provider.CurrentPlan.Plan.Contains(GraphNode.Action))
            {
                Node.AddToClassList("path");
                return;
            }

            if (graphNode.Action is not IGoapAction action)
                return;

            if (!action.IsEnabled(provider.Receiver))
            {
                Node.AddToClassList("disabled");
                return;
            }
        }

        public Label Target { get; set; }

        public Circle TargetCircle { get; set; }

        public Label Cost { get; set; }

        public Label Title { get; set; }

        public VisualElement Effects { get; set; }
        public VisualElement ChildWrapper { get; set; }
        public VisualElement Conditions { get; set; }
        public VisualElement Content { get; set; }
        public VisualElement Node { get; set; }
        public VisualElement NodeWrapper { get; private set; }
    }
}
