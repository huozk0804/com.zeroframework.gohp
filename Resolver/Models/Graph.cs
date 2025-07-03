//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class Graph : IGraph
    {
        public List<INode> RootNodes { get; set; } = new();
        public List<INode> ChildNodes { get; set; } = new();
        public INode[] AllNodes => this.RootNodes.Union(this.ChildNodes).ToArray();
        public INode[] UnconnectedNodes { get; set; }
    }
}