//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public interface INodeEffect
    {
        IEffect Effect { get; set; }
        INode[] Connections { get; set; }
    }
}