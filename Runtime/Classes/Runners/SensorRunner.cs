//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class SensorRunner : ISensorRunner
    {
        private readonly SensorSet _defaultSet = new();
        private readonly Dictionary<IGoapAction, SensorSet> _actionSets = new();
        private readonly Dictionary<IGoal, SensorSet> _goalSets = new();
        private readonly Dictionary<string, SensorSet> _goalsSets = new();
        private readonly Dictionary<Type, ISensor> _sensors = new();

        private readonly IGlobalWorldData _worldData;

        public SensorRunner(
            IEnumerable<IWorldSensor> worldSensors,
            IEnumerable<ITargetSensor> targetSensors,
            IEnumerable<IMultiSensor> multiSensors,
            IGlobalWorldData globalWorldData
        )
        {
            _worldData = globalWorldData;

            foreach (var worldSensor in worldSensors)
            {
                _defaultSet.AddSensor(worldSensor);

                _sensors.TryAdd(worldSensor.Key.GetType(), worldSensor);
            }

            foreach (var targetSensor in targetSensors)
            {
                _defaultSet.AddSensor(targetSensor);

                _sensors.TryAdd(targetSensor.Key.GetType(), targetSensor);
            }

            foreach (var multiSensor in multiSensors)
            {
                _defaultSet.AddSensor(multiSensor);

                foreach (var (key, value) in multiSensor.LocalSensors)
                {
                    _sensors.TryAdd(key, value);
                }

                foreach (var (key, value) in multiSensor.GlobalSensors)
                {
                    _sensors.TryAdd(key, value);
                }
            }
        }

        public void Update()
        {
            foreach (var localSensor in _defaultSet.LocalSensors)
            {
                localSensor.Update();
            }
        }

        public void Update(IGoapAction action)
        {
            var set = GetSet(action);

            foreach (var localSensor in set.LocalSensors)
            {
                localSensor.Update();
            }
        }

        public void SenseGlobal()
        {
            foreach (var globalSensor in _defaultSet.GlobalSensors)
            {
                globalSensor.Sense(_worldData);
            }
        }

        public void SenseGlobal(IGoapAction action)
        {
            var set = GetSet(action);

            foreach (var globalSensor in set.GlobalSensors)
            {
                globalSensor.Sense(_worldData);
            }
        }

        public void SenseLocal(IMonoGoapActionProvider actionProvider)
        {
            foreach (var localSensor in _defaultSet.LocalSensors)
            {
                localSensor.Sense(actionProvider.WorldData, actionProvider.Receiver, actionProvider.Receiver.Injector);
            }
        }

        public void SenseLocal(IMonoGoapActionProvider actionProvider, IGoapAction action)
        {
            if (actionProvider.IsNull())
                return;

            if (action == null)
                return;

            var set = GetSet(action);

            foreach (var localSensor in set.LocalSensors)
            {
                localSensor.Sense(actionProvider.WorldData, actionProvider.Receiver, actionProvider.Receiver.Injector);
            }
        }

        public void SenseLocal(IMonoGoapActionProvider actionProvider, IGoal goal)
        {
            if (actionProvider.IsNull())
                return;

            if (goal == null)
                return;

            var set = GetSet(goal);

            foreach (var localSensor in set.LocalSensors)
            {
                localSensor.Sense(actionProvider.WorldData, actionProvider.Receiver, actionProvider.Receiver.Injector);
            }
        }

        public void SenseLocal(IMonoGoapActionProvider actionProvider, IGoalRequest goalRequest)
        {
            if (actionProvider.IsNull())
                return;

            if (goalRequest.Goals.Count == 0)
                return;

            var set = GetSet(goalRequest);

            foreach (var localSensor in set.LocalSensors)
            {
                localSensor.Sense(actionProvider.WorldData, actionProvider.Receiver, actionProvider.Receiver.Injector);
            }
        }

        public void InitializeGraph(IGraph graph)
        {
            foreach (var rootNode in graph.RootNodes)
            {
                if (rootNode.Action is not IGoal goal)
                    continue;

                if (_goalSets.ContainsKey(goal))
                    continue;

                var set = CreateSet(rootNode);
                _goalSets[goal] = set;
            }
        }

        private SensorSet GetSet(IGoapAction action)
        {
            if (_actionSets.TryGetValue(action, out var existingSet))
                return existingSet;

            return CreateSet(action);
        }

        private SensorSet GetSet(IGoal goal)
        {
            return _goalSets.GetValueOrDefault(goal);
        }

        private SensorSet GetSet(IGoalRequest goalRequest)
        {
            if (string.IsNullOrEmpty(goalRequest.Key))
                goalRequest.Key = GuidCacheKey.GenerateKey(goalRequest.Goals);

            if (_goalsSets.TryGetValue(goalRequest.Key, out var existingSet))
                return existingSet;

            return CreateSet(goalRequest);
        }

        private SensorSet CreateSet(IGoapAction action)
        {
            var set = new SensorSet();

            foreach (var condition in action.Conditions)
            {
                set.Keys.Add(condition.WorldKey.GetType());
            }

            foreach (var key in set.Keys)
            {
                if (_sensors.TryGetValue(key, out var sensor))
                {
                    set.AddSensor(sensor);
                }
            }

            _actionSets[action] = set;

            return set;
        }

        private SensorSet CreateSet(IGoalRequest goalRequest)
        {
            var set = new SensorSet();

            foreach (var goal in goalRequest.Goals)
            {
                var goalSet = GetSet(goal);
                set.Merge(goalSet);
            }

            _goalsSets[goalRequest.Key] = set;

            return set;
        }

        private SensorSet CreateSet(INode node)
        {
            var actions = new List<IGoapAction>();
            node.GetActions(actions);

            var set = new SensorSet();

            foreach (var condition in node.Conditions.Select(x => x.Condition))
            {
                var key = condition.WorldKey.GetType();

                set.Keys.Add(key);

                if (_sensors.TryGetValue(key, out var sensor))
                {
                    set.AddSensor(sensor);
                }
            }

            foreach (var action in actions.Distinct())
            {
                var actionSet = GetSet(action);
                set.Merge(actionSet);

                if (action.Config.Target != null)
                {
                    set.Keys.Add(action.Config.Target.GetType());
                    set.AddSensor(_sensors[action.Config.Target.GetType()]);
                }
            }

            return set;
        }
    }

    public class SensorSet
    {
        public HashSet<Type> Keys { get; } = new();
        public HashSet<ILocalSensor> LocalSensors { get; } = new();
        public HashSet<IGlobalSensor> GlobalSensors { get; } = new();

        public void AddSensor(ISensor sensor)
        {
            switch (sensor)
            {
                case IMultiSensor multiSensor:
                    LocalSensors.Add(multiSensor);
                    GlobalSensors.Add(multiSensor);
                    break;
                case ILocalSensor localSensor:
                    LocalSensors.Add(localSensor);
                    break;
                case IGlobalSensor globalSensor:
                    GlobalSensors.Add(globalSensor);
                    break;
            }
        }

        public void Merge(SensorSet set)
        {
            Keys.UnionWith(set.Keys);
            LocalSensors.UnionWith(set.LocalSensors);
            GlobalSensors.UnionWith(set.GlobalSensors);
        }
    }
}