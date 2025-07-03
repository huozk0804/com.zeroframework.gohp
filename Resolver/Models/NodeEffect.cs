//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class NodeEffect : INodeEffect
    {
        public IEffect Effect { get; set; }
        public INode[] Connections { get; set; } = Array.Empty<INode>();
    }
}