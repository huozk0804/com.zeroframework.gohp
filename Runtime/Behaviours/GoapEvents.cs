//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public class GoapEvents : IGoapEvents
    {
        public event GoapAgentActionDelegate OnActionStart;

        public void ActionStart(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionStart?.Invoke(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionEnd;

        public void ActionEnd(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionEnd?.Invoke(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionStop;

        public void ActionStop(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionStop?.Invoke(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionComplete;

        public void ActionComplete(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionComplete?.Invoke(actionProvider, action);
        }

        public event AgentGoalRequestDelegate OnNoActionFound;

        public void NoActionFound(IMonoGoapActionProvider actionProvider, IGoalRequest request)
        {
            OnNoActionFound?.Invoke(actionProvider, request);
        }

        public event AgentGoalDelegate OnGoalStart;

        public void GoalStart(IMonoGoapActionProvider actionProvider, IGoal goal)
        {
            OnGoalStart?.Invoke(actionProvider, goal);
        }

        public event AgentGoalDelegate OnGoalCompleted;

        public void GoalCompleted(IMonoGoapActionProvider actionProvider, IGoal goal)
        {
            OnGoalCompleted?.Invoke(actionProvider, goal);
        }

        public event GoapAgentDelegate OnAgentResolve;

        public void AgentResolve(IMonoGoapActionProvider actionProvider)
        {
            OnAgentResolve?.Invoke(actionProvider);
        }

        public event GoapAgentDelegate OnAgentRegistered;

        public void AgentRegistered(IMonoGoapActionProvider actionProvider)
        {
            OnAgentRegistered?.Invoke(actionProvider);
        }

        public event GoapAgentDelegate OnAgentUnregistered;

        public void AgentUnregistered(IMonoGoapActionProvider actionProvider)
        {
            OnAgentUnregistered?.Invoke(actionProvider);
        }

        public event AgentTypeDelegate OnAgentTypeRegistered;

        public void AgentTypeRegistered(IAgentType agentType)
        {
            OnAgentTypeRegistered?.Invoke(agentType);
        }
    }
}
