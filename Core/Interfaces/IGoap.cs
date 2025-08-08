//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace Keystone.Goap
{
    public interface IGoap
    {
        IGoapEvents Events { get; }
        Dictionary<IAgentType, IAgentTypeJobRunner> AgentTypeRunners { get; }
        IGoapController Controller { get; }
        void Register(IAgentType agentType);
        IGraph GetGraph(IAgentType agentType);
        bool Knows(IAgentType agentType);
        List<IMonoGoapActionProvider> Agents { get; }

        IAgentType[] AgentTypes { get; }
        IGoapConfig Config { get; }

        IAgentType GetAgentType(string id);
    }
}