//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class ReactiveController : IGoapController
    {
        private IGoap _goap;

        public void Initialize(IGoap goap)
        {
            _goap = goap;
            _goap.Events.OnAgentResolve += OnAgentResolve;
            _goap.Events.OnNoActionFound += OnNoActionFound;
        }

        public void Disable()
        {
            if (_goap.IsNull())
                return;

            if (_goap?.Events == null)
                return;

            _goap.Events.OnAgentResolve -= OnAgentResolve;
            _goap.Events.OnNoActionFound -= OnNoActionFound;
        }

        public void OnUpdate()
        {
            foreach (var (type, runner) in _goap.AgentTypeRunners)
            {
                var queue = type.Agents.GetQueue();

                runner.Run(queue);
            }

            foreach (var agent in _goap.Agents)
            {
                if (agent.IsNull())
                    continue;

                if (agent.Receiver == null)
                    continue;

                if (agent.Receiver.IsPaused)
                    continue;

                // Update the action sensors for the agent
                agent.AgentType.SensorRunner.SenseLocal(agent, agent.Receiver.ActionState.Action as IGoapAction);
            }
        }

        public void OnLateUpdate()
        {
            foreach (var runner in _goap.AgentTypeRunners.Values)
            {
                runner.Complete();
            }
        }

        private void OnNoActionFound(IMonoGoapActionProvider actionProvider, IGoalRequest request)
        {
            Enqueue(actionProvider);
        }

        private void OnAgentResolve(IMonoGoapActionProvider actionProvider)
        {
            Enqueue(actionProvider);
        }

        private void Enqueue(IMonoGoapActionProvider actionProvider)
        {
            actionProvider.AgentType?.Agents.Enqueue(actionProvider);
        }
    }
}