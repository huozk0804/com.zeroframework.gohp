//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Keystone.Goap
{
    public class GraphBuilder
    {
        private readonly IKeyResolver _keyResolver;

        public GraphBuilder(IKeyResolver keyResolver)
        {
            _keyResolver = keyResolver;
        }

        public Graph Build(IEnumerable<IConnectable> actions)
        {
            var nodes = actions.ToNodes();

            var graph = new Graph
            {
                RootNodes = nodes.RootNodes.ToList(),
            };

            var allNodes = nodes.RootNodes.Union(nodes.ChildNodes).ToArray();

            var effectMap = GetEffectMap(allNodes);
            var conditionMap = GetConditionMap(allNodes);

            foreach (var node in nodes.RootNodes)
            {
                ConnectNodes(node, effectMap, conditionMap, graph);
            }

            graph.UnconnectedNodes = allNodes.Where(x => !graph.ChildNodes.Contains(x) && !graph.RootNodes.Contains(x))
                .ToArray();

            return graph;
        }

        private void ConnectNodes(
            INode node, Dictionary<string, List<INode>> effectMap,
            Dictionary<string, List<INode>> conditionMap, IGraph graph
        )
        {
            if (!graph.ChildNodes.Contains(node) && !node.IsRootNode)
                graph.ChildNodes.Add(node);

            foreach (var actionNodeCondition in node.Conditions)
            {
                if (actionNodeCondition.Connections.Any())
                    continue;

                var key = _keyResolver.GetKey(actionNodeCondition.Condition);

                if (!effectMap.ContainsKey(key))
                    continue;

                var connections = effectMap[key].Where(x => !HasConflictingConditions(node, x)).ToArray();

                actionNodeCondition.Connections = connections;

                foreach (var connection in actionNodeCondition.Connections)
                {
                    connection.Effects.First(x => _keyResolver.GetKey(x.Effect) == key)
                        .Connections = conditionMap[key].Where(x => !HasConflictingConditions(node, x)).ToArray();
                }

                foreach (var subNode in actionNodeCondition.Connections)
                {
                    ConnectNodes(subNode, effectMap, conditionMap, graph);
                }
            }
        }

        private bool HasConflictingConditions(INode node, INode otherNode)
        {
            foreach (var condition in node.Conditions)
            {
                foreach (var otherEffects in otherNode.Effects)
                {
                    if (_keyResolver.AreConflicting(otherEffects.Effect, condition.Condition))
                        return true;
                }
            }

            return false;
        }

        private Dictionary<string, List<INode>> GetEffectMap(INode[] actionNodes)
        {
            var map = new Dictionary<string, List<INode>>();

            foreach (var actionNode in actionNodes)
            {
                foreach (var actionNodeEffect in actionNode.Effects)
                {
                    var key = _keyResolver.GetKey(actionNodeEffect.Effect);

                    if (!map.ContainsKey(key))
                        map[key] = new List<INode>();

                    map[key].Add(actionNode);
                }
            }

            return map;
        }

        private Dictionary<string, List<INode>> GetConditionMap(INode[] actionNodes)
        {
            var map = new Dictionary<string, List<INode>>();

            foreach (var actionNode in actionNodes)
            {
                foreach (var actionNodeConditions in actionNode.Conditions)
                {
                    var key = _keyResolver.GetKey(actionNodeConditions.Condition);

                    if (!map.ContainsKey(key))
                        map[key] = new List<INode>();

                    map[key].Add(actionNode);
                }
            }

            return map;
        }
    }
}