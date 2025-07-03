//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroFramework.Goap.Agent;
using Unity.Collections;
using Unity.Mathematics;

namespace ZeroFramework.Goap
{
    public class AgentTypeJobRunner : IAgentTypeJobRunner
    {
        private readonly IAgentType _agentType;
        private readonly IGraphResolver _resolver;

        private readonly List<JobRunHandle> _resolveHandles = new();
        private readonly IExecutableBuilder _executableBuilder;
        private readonly IEnabledBuilder _enabledBuilder;
        private readonly IPositionBuilder _positionBuilder;
        private readonly ICostBuilder _costBuilder;
        private readonly IConditionBuilder _conditionBuilder;

        private readonly List<int> _goalIndexes = new();

        public AgentTypeJobRunner(IAgentType agentType, IGraphResolver graphResolver)
        {
            _agentType = agentType;
            _resolver = graphResolver;

            _enabledBuilder = _resolver.GetEnabledBuilder();
            _executableBuilder = _resolver.GetExecutableBuilder();
            _positionBuilder = _resolver.GetPositionBuilder();
            _costBuilder = _resolver.GetCostBuilder();
            _conditionBuilder = _resolver.GetConditionBuilder();

            agentType.SensorRunner.InitializeGraph(graphResolver.GetGraph());
        }

        public void Run(IMonoGoapActionProvider[] queue)
        {
            _resolveHandles.Clear();

            _agentType.SensorRunner.Update();
            _agentType.SensorRunner.SenseGlobal();

            foreach (var agent in queue)
            {
                Run(agent);
            }
        }

        private void Run(IMonoGoapActionProvider actionProvider)
        {
            if (actionProvider.IsNull())
                return;

            if (actionProvider.AgentType != _agentType)
                return;

            if (IsGoalCompleted(actionProvider))
            {
                var goal = actionProvider.CurrentPlan;
                actionProvider.ClearGoal();
                actionProvider.Events.GoalCompleted(goal.Goal);
            }

            if (!MayResolve(actionProvider))
                return;

            var goalRequest = actionProvider.GoalRequest;

            if (goalRequest == null)
                return;

            _agentType.SensorRunner.SenseLocal(actionProvider, goalRequest);

            FillBuilders(actionProvider);

            LogRequest(actionProvider, goalRequest);

            _goalIndexes.Clear();

            foreach (var goal in goalRequest.Goals)
            {
                if (IsGoalCompleted(actionProvider, goal))
                    continue;

                _goalIndexes.Add(_resolver.GetIndex(goal));
            }

            _resolveHandles.Add(new JobRunHandle(actionProvider, goalRequest)
            {
                Handle = _resolver.StartResolve(new RunData
                {
                    StartIndex = new NativeArray<int>(_goalIndexes.ToArray(), Allocator.TempJob),
                    AgentPosition = actionProvider.Position,
                    IsEnabled = new NativeArray<bool>(_enabledBuilder.Build(), Allocator.TempJob),
                    IsExecutable = new NativeArray<bool>(_executableBuilder.Build(), Allocator.TempJob),
                    Positions = new NativeArray<float3>(_positionBuilder.Build(), Allocator.TempJob),
                    Costs = new NativeArray<float>(_costBuilder.Build(), Allocator.TempJob),
                    ConditionsMet = new NativeArray<bool>(_conditionBuilder.Build(), Allocator.TempJob),
                    DistanceMultiplier = actionProvider.DistanceMultiplier,
                }),
            });
        }

        private void FillBuilders(IMonoGoapActionProvider actionProvider)
        {
            var conditionObserver = _agentType.GoapConfig.ConditionObserver;
            conditionObserver.SetWorldData(actionProvider.WorldData);

            _enabledBuilder.Clear();
            _executableBuilder.Clear();
            _positionBuilder.Clear();
            _conditionBuilder.Clear();

            foreach (var goal in _agentType.GetGoals())
            {
                _costBuilder.SetCost(goal, goal.GetCost(actionProvider.Receiver, actionProvider.Receiver.Injector));
            }

            foreach (var node in _agentType.GetActions())
            {
                var allMet = true;

                foreach (var condition in node.Conditions)
                {
                    if (!conditionObserver.IsMet(condition))
                    {
                        allMet = false;
                        continue;
                    }

                    _conditionBuilder.SetConditionMet(condition, true);
                }

                var target = actionProvider.WorldData.GetTarget(node);

                _executableBuilder.SetExecutable(node, node.IsExecutable(actionProvider.Receiver, allMet));
                _enabledBuilder.SetEnabled(node, node.IsEnabled(actionProvider.Receiver));
                _costBuilder.SetCost(node,
                    node.GetCost(actionProvider.Receiver, actionProvider.Receiver.Injector, target));

                _positionBuilder.SetPosition(node, target?.GetValidPosition());
            }
        }

        private bool IsGoalCompleted(IMonoGoapActionProvider actionProvider)
        {
            if (actionProvider.CurrentPlan?.Goal == null)
                return false;

            _agentType.SensorRunner.SenseLocal(actionProvider, actionProvider.CurrentPlan.Goal);

            return IsGoalCompleted(actionProvider, actionProvider.CurrentPlan.Goal);
        }

        private bool IsGoalCompleted(IGoapActionProvider actionProvider, IGoal goal)
        {
            if (goal == null)
                return false;

            var conditionObserver = _agentType.GoapConfig.ConditionObserver;
            conditionObserver.SetWorldData(actionProvider.WorldData);

            foreach (var condition in goal.Conditions)
            {
                if (!conditionObserver.IsMet(condition))
                    return false;
            }

            return true;
        }

        private bool MayResolve(IGoapActionProvider actionProvider)
        {
            if (actionProvider.Receiver.IsPaused)
                return false;

            if (actionProvider.Receiver.ActionState?.RunState == null)
                return true;

            if (actionProvider.Receiver is not IAgent agent)
                return true;

            return actionProvider.Receiver.ActionState.RunState.MayResolve(agent);
        }

        public void Complete()
        {
            foreach (var resolveHandle in _resolveHandles)
            {
                var result = resolveHandle.Handle.Complete();

                if (resolveHandle.ActionProvider.GoalRequest != resolveHandle.GoalRequest)
                    continue;

                if (resolveHandle.ActionProvider.IsNull())
                    continue;

                var goal = result.Goal;
                if (goal == null)
                {
                    resolveHandle.ActionProvider.Events.NoActionFound(resolveHandle.GoalRequest);
                    continue;
                }

                var action = result.Actions.FirstOrDefault() as IGoapAction;

                if (action is null)
                {
                    resolveHandle.ActionProvider.Events.NoActionFound(resolveHandle.GoalRequest);
                    continue;
                }

                if (action != resolveHandle.ActionProvider.Receiver.ActionState.Action)
                {
                    resolveHandle.ActionProvider.SetAction(new GoalResult
                    {
                        Goal = goal,
                        Plan = result.Actions,
                        Action = action,
                    });
                }
            }

            _resolveHandles.Clear();
        }

        private void LogRequest(IGoapActionProvider actionProvider, IGoalRequest request)
        {
#if UNITY_EDITOR
            if (actionProvider.Logger == null)
                return;

            if (!actionProvider.Logger.ShouldLog())
                return;

            var builder = new StringBuilder();
            builder.Append("Trying to resolve goals ");

            foreach (var goal in request.Goals)
            {
                var value = Extensions.GetGenericTypeName(goal.GetType());
                builder.Append(value);
                builder.Append(", ");
            }

            actionProvider.Logger.Log(builder.ToString());
#endif
        }

        public void Dispose()
        {
            foreach (var resolveHandle in _resolveHandles)
            {
                resolveHandle.Handle.Complete();
            }

            _resolver.Dispose();
        }

        private class JobRunHandle
        {
            public IMonoGoapActionProvider ActionProvider { get; }
            public IResolveHandle Handle { get; set; }
            public IGoalRequest GoalRequest { get; set; }

            public JobRunHandle(IMonoGoapActionProvider actionProvider, IGoalRequest goalRequest)
            {
                ActionProvider = actionProvider;
                GoalRequest = goalRequest;
            }
        }

        public IGraph GetGraph() => _resolver.GetGraph();
    }
}