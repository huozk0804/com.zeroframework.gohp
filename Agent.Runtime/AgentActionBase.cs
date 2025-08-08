//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap.Agent
{
    // Backwards compatibility for old actions
    public abstract class AgentActionBase<TActionData> : AgentActionBase<TActionData, EmptyActionProperties>
        where TActionData : IActionData, new()
    {
    }
}