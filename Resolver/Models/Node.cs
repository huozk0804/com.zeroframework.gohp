//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Keystone.Goap
{
    public class Node : INode
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public IConnectable Action { get; set; }

        public List<INodeEffect> Effects { get; set; } = new();
        public List<INodeCondition> Conditions { get; set; } = new();

        public bool IsRootNode => Action is IGoal;

        public void GetActions(List<IGoapAction> actions)
        {
            if (actions.Contains(Action as IGoapAction))
                return;

            if (Action is IGoapAction goapAction)
                actions.Add(goapAction);

            foreach (var condition in Conditions)
            {
                foreach (var connection in condition.Connections)
                {
                    connection.GetActions(actions);
                }
            }
        }
    }
}
