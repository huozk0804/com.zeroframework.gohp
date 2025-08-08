//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class AgentTypeEvents : IAgentTypeEvents
    {
        private IGoapEvents _goapEvents;

        public void Bind(IGoapEvents events)
        {
            _goapEvents = events;
        }

        public event GoapAgentActionDelegate OnActionStart;

        public void ActionStart(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionStart?.Invoke(actionProvider, action);
            _goapEvents?.ActionStart(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionEnd;

        public void ActionEnd(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionEnd?.Invoke(actionProvider, action);
            _goapEvents?.ActionEnd(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionStop;

        public void ActionStop(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionStop?.Invoke(actionProvider, action);
            _goapEvents?.ActionStop(actionProvider, action);
        }

        public event GoapAgentActionDelegate OnActionComplete;

        public void ActionComplete(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            OnActionComplete?.Invoke(actionProvider, action);
            _goapEvents?.ActionComplete(actionProvider, action);
        }

        public event AgentGoalRequestDelegate OnNoActionFound;

        public void NoActionFound(IMonoGoapActionProvider actionProvider, IGoalRequest request)
        {
            OnNoActionFound?.Invoke(actionProvider, request);
            _goapEvents?.NoActionFound(actionProvider, request);
        }

        public event AgentGoalDelegate OnGoalStart;

        public void GoalStart(IMonoGoapActionProvider actionProvider, IGoal goal)
        {
            OnGoalStart?.Invoke(actionProvider, goal);
            _goapEvents?.GoalStart(actionProvider, goal);
        }

        public event AgentGoalDelegate OnGoalCompleted;

        public void GoalCompleted(IMonoGoapActionProvider actionProvider, IGoal goal)
        {
            OnGoalCompleted?.Invoke(actionProvider, goal);
            _goapEvents?.GoalCompleted(actionProvider, goal);
        }

        public event GoapAgentDelegate OnAgentResolve;

        public void AgentResolve(IMonoGoapActionProvider actionProvider)
        {
            OnAgentResolve?.Invoke(actionProvider);
            _goapEvents?.AgentResolve(actionProvider);
        }

        public event GoapAgentDelegate OnAgentRegistered;

        public void AgentRegistered(IMonoGoapActionProvider actionProvider)
        {
            OnAgentRegistered?.Invoke(actionProvider);
            _goapEvents?.AgentRegistered(actionProvider);
        }

        public event GoapAgentDelegate OnAgentUnregistered;

        public void AgentUnregistered(IMonoGoapActionProvider actionProvider)
        {
            OnAgentUnregistered?.Invoke(actionProvider);
            _goapEvents?.AgentUnregistered(actionProvider);
        }
    }
}
