//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class Goap : IGoap
    {
        private readonly Stopwatch _stopwatch = new();

        /** GC caches **/
        private readonly List<IMonoGoapActionProvider> _agentsGC = new();

        /** **/
        public IGoapEvents Events { get; } = new GoapEvents();

        public Dictionary<IAgentType, IAgentTypeJobRunner> AgentTypeRunners { get; private set; } = new();

        public float RunTime { get; private set; }
        public float CompleteTime { get; private set; }

        public IGoapController Controller { get; }

        public Goap(IGoapController controller)
        {
            Controller = controller;
            Config = GoapConfig.Default;
        }

        public void Register(IAgentType agentType)
        {
            AgentTypeRunners.Add(agentType,
                new AgentTypeJobRunner(agentType,
                    new GraphResolver(agentType.GetAllNodes().ToArray(), agentType.GoapConfig.KeyResolver)));

            agentType.Events.Bind(Events);
            Events.AgentTypeRegistered(agentType);
        }
        
        public void Dispose()
        {
            foreach (var runner in AgentTypeRunners.Values)
            {
                runner.Dispose();
            }
        }

        private float GetElapsedMs()
        {
            _stopwatch.Stop();

            return (float)((double)_stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000);
        }

        public IGraph GetGraph(IAgentType agentType) => AgentTypeRunners[agentType].GetGraph();
        public bool Knows(IAgentType agentType) => AgentTypeRunners.ContainsKey(agentType);

        public List<IMonoGoapActionProvider> Agents
        {
            get
            {
                _agentsGC.Clear();

                foreach (var runner in AgentTypeRunners.Keys)
                {
                    _agentsGC.AddRange(runner.Agents.All());
                }

                return _agentsGC;
            }
        }

        public IAgentType[] AgentTypes => AgentTypeRunners.Keys.ToArray();
        public IGoapConfig Config { get; }

        public IAgentType GetAgentType(string id)
        {
            var agentTypes = AgentTypes.FirstOrDefault(x => x.Id == id);

            if (agentTypes == null)
                throw new KeyNotFoundException($"No agentType with id {id} found");

            return agentTypes;
        }
    }
}