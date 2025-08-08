//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public interface IGoal : IConnectable, IHasConfig<IGoalConfig>
    {
        public int Index { get; set; }
        public float GetCost(IActionReceiver agent, IComponentReference references);
    }
}