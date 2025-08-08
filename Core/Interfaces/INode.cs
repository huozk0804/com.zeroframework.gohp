//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Keystone.Goap
{
    public interface INode
    {
        Guid Guid { get; }
        IConnectable Action { get; set; }
        List<INodeEffect> Effects { get; set; }
        List<INodeCondition> Conditions { get; set; }
        bool IsRootNode { get; }
        void GetActions(List<IGoapAction> actions);
    }
}