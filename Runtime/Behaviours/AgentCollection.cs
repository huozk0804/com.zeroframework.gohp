//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class AgentCollection : IAgentCollection
    {
        private readonly IAgentType _agentType;
        private readonly HashSet<IMonoGoapActionProvider> _agents = new();
        private readonly HashSet<IMonoGoapActionProvider> _queue = new();

        public AgentCollection(IAgentType agentType)
        {
            _agentType = agentType;
        }

        public HashSet<IMonoGoapActionProvider> All() => _agents;

        public void Add(IMonoGoapActionProvider actionProvider)
        {
            if (!actionProvider.isActiveAndEnabled)
                return;

            if (!_agents.Add(actionProvider))
                return;

            _agentType.Events.AgentRegistered(actionProvider);
        }

        public void Remove(IMonoGoapActionProvider actionProvider)
        {
            if (!_agents.Contains(actionProvider))
                return;

            _agents.Remove(actionProvider);
            _agentType.Events.AgentUnregistered(actionProvider);
        }

        public void Enqueue(IMonoGoapActionProvider actionProvider)
        {
            if (!_agents.Contains(actionProvider))
                return;

            _queue.Add(actionProvider);
        }

        public int GetQueueCount() => _queue.Count;

        public IMonoGoapActionProvider[] GetQueue()
        {
            var data = _queue.ToArray();
            _queue.Clear();

            return data;
        }
    }
}