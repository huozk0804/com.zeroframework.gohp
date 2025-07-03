using System;
using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Editor.Package
{
    public class Nodes
    {
        public Dictionary<int, List<RenderNode>> DepthNodes { get; private set; } = new ();
        public Dictionary<Guid, RenderNode> AllNodes { get; private set; } = new();
        public int MaxWidth { get; private set; }

        public RenderNode Get(Guid guid) => AllNodes[guid];
        
        private List<RenderNode> GetList(int depth)
        {
            if (DepthNodes.TryGetValue(depth, out var levels))
                return levels;

            levels = new List<RenderNode>();
            DepthNodes.Add(depth, levels);
            return levels;
        }
        
        public int GetMaxWidth()
        {
            var max = 0;

            foreach (var (key, value) in DepthNodes)
            {
                if (value.Count > max)
                    max = value.Count;
            }

            return max;
        }

        public void Add(int depth, Node node)
        {
            if (Contains(node))
                return;

            var newNode = new RenderNode(node);
            GetList(depth).Add(newNode);
            AllNodes.Add(newNode.Node.Guid, newNode);

            MaxWidth = GetMaxWidth();
        }

        public bool Contains(Node node)
        {
            return AllNodes.Values.Any(x => x.Node == node);
        }
    }
}