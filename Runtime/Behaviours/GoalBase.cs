//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public abstract class GoalBase : IGoal
    {
        public int Index { get; set; }
        public IGoalConfig Config { get; private set; }

        public Guid Guid { get; } = Guid.NewGuid();
        public IEffect[] Effects { get; } = { };

        public ICondition[] Conditions { get; private set; }

        public void SetConfig(IGoalConfig config)
        {
            Config = config;
            Conditions = config.Conditions.ToArray();
        }

        public virtual float GetCost(IActionReceiver agent, IComponentReference references)
        {
            return Config.BaseCost;
        }
    }
}
