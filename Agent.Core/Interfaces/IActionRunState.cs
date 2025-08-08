//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap.Agent
{
    public interface IActionRunState
    {
        void Update(IAgent agent, IActionContext context);
        bool ShouldStop(IAgent agent);
        bool ShouldPerform(IAgent agent);
        bool IsCompleted(IAgent agent);
        bool MayResolve(IAgent agent);
        bool IsRunning();
    }
}