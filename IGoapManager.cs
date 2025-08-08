//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public interface IGoapManager
    {
        IGoapConfig Config { get; }
        IAgentType[] AgentTypes { get; }
        IAgentType GetAgentType(string id);
        void Register(IAgentType agentType);
        void Register(IAgentTypeConfig agentTypeConfig);
    }
}