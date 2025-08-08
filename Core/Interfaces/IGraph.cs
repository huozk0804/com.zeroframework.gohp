//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace Keystone.Goap
{
    public interface IGraph
    {
        List<INode> RootNodes { get; set; }
        List<INode> ChildNodes { get; set; }
        INode[] AllNodes { get; }
        INode[] UnconnectedNodes { get; }
    }
}