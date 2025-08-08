//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class GoapAgentEvents : IGoapAgentEvents
    {
        private IMonoGoapActionProvider _actionProvider;
        private IAgentTypeEvents _typeEvents;

        public void Bind(IMonoGoapActionProvider actionProvider, IAgentTypeEvents events)
        {
            _typeEvents = events;
            _actionProvider = actionProvider;
        }

        public void Bind(IActionReceiver receiver)
        {
            Unbind();

            receiver.Events.OnActionStart += ActionStart;
            receiver.Events.OnActionEnd += ActionEnd;
            receiver.Events.OnActionStop += ActionStop;
            receiver.Events.OnActionComplete += ActionComplete;
        }

        public void Unbind()
        {
            if (_actionProvider == null)
                return;

            if (_actionProvider.Receiver == null)
                return;

            _actionProvider.Receiver.Events.OnActionStart -= ActionStart;
            _actionProvider.Receiver.Events.OnActionEnd -= ActionEnd;
            _actionProvider.Receiver.Events.OnActionStop -= ActionStop;
            _actionProvider.Receiver.Events.OnActionComplete -= ActionComplete;
        }

        public event GoalRequestDelegate OnNoActionFound;

        public void NoActionFound(IGoalRequest goal)
        {
            OnNoActionFound?.Invoke(goal);
            _typeEvents?.NoActionFound(_actionProvider, goal);
        }

        // Goals
        public event GoalDelegate OnGoalStart;

        public void GoalStart(IGoal goal)
        {
            OnGoalStart?.Invoke(goal);
            _typeEvents?.GoalStart(_actionProvider, goal);
        }

        public event GoalDelegate OnGoalCompleted;

        public void GoalCompleted(IGoal goal)
        {
            OnGoalCompleted?.Invoke(goal);
            _typeEvents?.GoalCompleted(_actionProvider, goal);
        }

        // General
        public event EmptyDelegate OnResolve;

        public void Resolve()
        {
            OnResolve?.Invoke();
            _typeEvents?.AgentResolve(_actionProvider);
        }

        // Agent events
        public event GoapActionDelegate OnActionStart;
        public event GoapActionDelegate OnActionEnd;
        public event GoapActionDelegate OnActionStop;
        public event GoapActionDelegate OnActionComplete;

        // Pass through
        private void ActionStart(IAction action)
        {
            if (action is not IGoapAction goapAction)
                return;

            OnActionStart?.Invoke(goapAction);
            _typeEvents.ActionStart(_actionProvider, goapAction);
        }

        private void ActionEnd(IAction action)
        {
            if (action is not IGoapAction goapAction)
                return;

            OnActionEnd?.Invoke(goapAction);
            _typeEvents.ActionEnd(_actionProvider, goapAction);
        }

        private void ActionStop(IAction action)
        {
            if (action is not IGoapAction goapAction)
                return;

            OnActionStop?.Invoke(goapAction);
            _typeEvents.ActionStop(_actionProvider, goapAction);
        }

        private void ActionComplete(IAction action)
        {
            if (action is not IGoapAction goapAction)
                return;

            OnActionComplete?.Invoke(goapAction);
            _typeEvents.ActionComplete(_actionProvider, goapAction);
        }
    }
}
