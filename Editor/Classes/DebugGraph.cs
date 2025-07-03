using System;
using System.Linq;

namespace ZeroFramework.Editor.Package
{
    public class DebugGraph
    {
        private readonly Graph graph;

        public DebugGraph(Graph graph)
        {
            this.graph = graph;
        }

        public Nodes GetGraph(Node entryNode)
        {
            var nodes = new Nodes();
            
            StoreNode(0, entryNode, nodes);

            return nodes;
        }

        private void StoreNode(int depth, Node node, Nodes nodes)
        {
            if (nodes.Contains(node))
                return;
            
            nodes.Add(depth, node);

            foreach (var connection in node.Conditions.SelectMany(condition => condition.Connections))
            {
                StoreNode(depth + 1,  GetNode(connection), nodes);
            }
        }

        private Node GetNode(Guid guid)
        {
            return graph.AllNodes.Values.First(x => x.Guid == guid);
        }
    }
}