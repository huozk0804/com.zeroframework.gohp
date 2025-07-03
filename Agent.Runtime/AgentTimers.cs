//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    public class AgentTimers : IAgentTimers
    {
        public ITimer Action { get; } = new Timer();
        public ITimer Goal { get; } = new Timer();
        public ITimer Resolve { get; } = new Timer();
    }
}