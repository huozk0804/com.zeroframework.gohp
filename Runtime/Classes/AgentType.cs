//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Keystone.Goap
{
    public class AgentType : IAgentType
    {
        public string Id { get; }
        public IAgentCollection Agents { get; }
        public IGoapConfig GoapConfig { get; }
        public ISensorRunner SensorRunner { get; }
        public IAgentTypeEvents Events { get; } = new AgentTypeEvents();
        public IGlobalWorldData WorldData { get; }

        private readonly List<IGoal> _goals;
        private readonly Dictionary<Type, IGoal> _goalsLookup;
        private readonly List<IGoapAction> _actions;

        public AgentType(string id, IGoapConfig config, List<IGoal> goals, List<IGoapAction> actions,
            ISensorRunner sensorRunner, IGlobalWorldData worldData)
        {
            Id = id;
            GoapConfig = config;
            SensorRunner = sensorRunner;
            _goals = goals;
            _goalsLookup = goals.ToDictionary(x => x.GetType());
            _actions = actions;
            WorldData = worldData;

            Agents = new AgentCollection(this);
        }

        public void Register(IMonoGoapActionProvider actionProvider)
        {
            Agents.Add(actionProvider);
        }

        public void Unregister(IMonoGoapActionProvider actionProvider)
        {
            Agents.Remove(actionProvider);
        }

        public TGoal ResolveGoal<TGoal>() where TGoal : IGoal
        {
            if (_goalsLookup.TryGetValue(typeof(TGoal), out var result))
                return (TGoal)result;

            throw new KeyNotFoundException($"No action found of type {typeof(TGoal)}");
        }

        public IGoal ResolveGoal(Type goalType)
        {
            if (_goalsLookup.TryGetValue(goalType, out var result))
                return result;

            throw new KeyNotFoundException($"No action found of type {goalType}");
        }

        public bool AllConditionsMet(IGoapActionProvider actionProvider, IGoapAction action)
        {
            var conditionObserver = GoapConfig.ConditionObserver;
            conditionObserver.SetWorldData(actionProvider.WorldData);

            foreach (var condition in action.Conditions)
            {
                if (!conditionObserver.IsMet(condition))
                    return false;
            }

            return true;
        }

        public List<IConnectable> GetAllNodes() => _actions.Cast<IConnectable>().Concat(_goals).ToList();
        public List<IGoapAction> GetActions() => _actions;

        public List<TAction> GetActions<TAction>() where TAction : IGoapAction =>
            _actions.OfType<TAction>().ToList();

        public List<IGoal> GetGoals() => _goals;
    }
}