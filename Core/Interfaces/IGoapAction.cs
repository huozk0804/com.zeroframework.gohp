//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public interface IGoapAction : IAction, IConnectable, IHasConfig<IActionConfig>
    {
        float GetCost(IActionReceiver agent, IComponentReference references, ITarget target);
        bool IsEnabled(IActionReceiver agent);
        void Enable(IActionReceiver receiver);
        void Disable(IActionReceiver receiver, IActionDisabler disabler);
    }
}
