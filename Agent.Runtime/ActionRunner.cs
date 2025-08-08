//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Keystone.Goap.Agent
{
    public class ActionRunner
    {
        private readonly IMonoAgent _agent;
        private readonly IAgentProxy _proxy;
        private readonly ActionContext _context = new();

        public ActionRunner(IMonoAgent agent, IAgentProxy proxy)
        {
            _agent = agent;
            _proxy = proxy;
        }

        public void Run()
        {
            if (!RunIsValid())
                return;

            switch (_agent.ActionState.Action.GetMoveMode(_agent))
            {
                case ActionMoveMode.MoveBeforePerforming:
                    RunMoveBeforePerforming();
                    break;
                case ActionMoveMode.PerformWhileMoving:
                    RunPerformWhileMoving();
                    break;
            }
        }

        private bool RunIsValid()
        {
            if (_agent.ActionState?.Action == null)
            {
                _agent.Logger.Warning("No action to run!");
                return false;
            }

            var isValid = _agent.ActionState.Action.IsValid(_agent, _agent.ActionState.Data);

            if (!isValid)
            {
                _agent.Logger.Warning($"Action {_agent.ActionState.Action.GetType().GetGenericTypeName()} is not valid!");
                _agent.StopAction();
                return false;
            }

            return true;
        }

        private void RunPerformWhileMoving()
        {
            if (_proxy.IsInRange())
            {
                _proxy.SetState(AgentState.PerformingAction);
                _proxy.SetMoveState(AgentMoveState.InRange);
                PerformAction();
                return;
            }

            _proxy.SetState(AgentState.MovingWhilePerformingAction);
            _proxy.SetMoveState(AgentMoveState.NotInRange);
            Move();
            PerformAction();
        }

        private void RunMoveBeforePerforming()
        {
            if (_proxy.IsInRange() || _agent.State == AgentState.PerformingAction)
            {
                _proxy.SetState(AgentState.PerformingAction);
                _proxy.SetMoveState(AgentMoveState.InRange);
                PerformAction();
                return;
            }

            _proxy.SetState(AgentState.MovingToTarget);
            _proxy.SetMoveState(AgentMoveState.NotInRange);
            Move();
        }

        private void Move()
        {
            if (_agent.CurrentTarget == null)
                return;

            _agent.Events.Move(_agent.CurrentTarget);
        }

        private void PerformAction()
        {
            if (!ShouldContinue())
                return;

            _context.DeltaTime = Time.deltaTime;
            _context.IsInRange = _proxy.IsInRange();

            if (!_agent.ActionState.HasPerformed)
                _agent.ActionState.Action.BeforePerform(_agent, _agent.ActionState.Data);

            var state = _agent.ActionState.Action.Perform(_agent, _agent.ActionState.Data, _context);

            if (state.IsCompleted(_agent))
            {
                _agent.CompleteAction();
                return;
            }

            if (state.ShouldStop(_agent))
            {
                _agent.StopAction();
                return;
            }

            _proxy.SetRunState(state);
        }

        private bool ShouldContinue()
        {
            if (_agent.ActionState.RunState == null)
                return true;

            _agent.ActionState.RunState.Update(_agent, _context);

            if (_agent.ActionState.RunState.IsCompleted(_agent))
            {
                _agent.CompleteAction();
                return false;
            }

            if (_agent.ActionState.RunState.ShouldStop(_agent))
            {
                _agent.StopAction();
                return false;
            }

            return _agent.ActionState.RunState.ShouldPerform(_agent);
        }
    }

    public class AgentProxy : IAgentProxy
    {
        private readonly Action<AgentState> _setState;
        private readonly Action<AgentMoveState> _setMoveState;
        private readonly Action<IActionRunState> _setRunState;
        private readonly Func<bool> _isInRange;

        public AgentProxy(Action<AgentState> setState, Action<AgentMoveState> setMoveState, Action<IActionRunState> setRunState, Func<bool> isInRange)
        {
            _setState = setState;
            _setMoveState = setMoveState;
            _setRunState = setRunState;
            _isInRange = isInRange;
        }

        public void SetState(AgentState state) => _setState(state);
        public void SetMoveState(AgentMoveState state) => _setMoveState(state);
        public void SetRunState(IActionRunState state) => _setRunState(state);
        public bool IsInRange() => _isInRange();
    }

    public interface IAgentProxy
    {
        void SetState(AgentState state);
        void SetMoveState(AgentMoveState state);
        void SetRunState(IActionRunState state);
        bool IsInRange();
    }
}
