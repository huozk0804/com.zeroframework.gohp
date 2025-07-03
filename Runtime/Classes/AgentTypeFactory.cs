//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace ZeroFramework.Goap
{
    public class AgentTypeFactory
    {
        private readonly IGoapConfig _goapConfig;
        private readonly ClassResolver _classResolver = new();

        private readonly IAgentTypeConfigValidatorRunner _agentTypeConfigValidatorRunner =
            new AgentTypeConfigValidatorRunner();

        public AgentTypeFactory(IGoapConfig goapConfig)
        {
            _goapConfig = goapConfig;
        }

        public AgentType Create(IAgentTypeConfig config, bool validate = true)
        {
            if (validate)
                Validate(config);

            var worldData = new GlobalWorldData();

            var sensorRunner = CreateSensorRunner(config, worldData);

            return new AgentType(
                id: config.Name,
                config: _goapConfig,
                actions: GetActions(config),
                goals: GetGoals(config),
                sensorRunner: sensorRunner,
                worldData: worldData
            );
        }

        private void Validate(IAgentTypeConfig config)
        {
            var results = _agentTypeConfigValidatorRunner.Validate(config);

            foreach (var error in results.GetErrors())
            {
                Debug.LogError(error);
            }

            foreach (var warning in results.GetWarnings())
            {
                Debug.LogWarning(warning);
            }

            if (results.HasErrors())
                throw new GoapException($"AgentTypeConfig has errors: {config.Name}");
        }

        private SensorRunner CreateSensorRunner(IAgentTypeConfig config, GlobalWorldData globalWorldData)
        {
            return new SensorRunner(GetWorldSensors(config), GetTargetSensors(config), GetMultiSensors(config),
                globalWorldData);
        }

        private List<IGoapAction> GetActions(IAgentTypeConfig config)
        {
            var actions = _classResolver.Load<IGoapAction, IActionConfig>(config.Actions);
            var injector = _goapConfig.GoapInjector;

            actions.ForEach(x =>
            {
                if (x.Config is IClassCallbackConfig classCallbackConfig)
                    classCallbackConfig.Callback?.Invoke(x);

                injector.Inject(x);
                x.Created();
            });

            return actions;
        }

        private List<IGoal> GetGoals(IAgentTypeConfig config)
        {
            var goals = _classResolver.Load<IGoal, IGoalConfig>(config.Goals.DistinctBy(x => x.ClassType));
            var injector = _goapConfig.GoapInjector;
            var index = 0;

            goals.ForEach(x =>
            {
                if (x.Config is IClassCallbackConfig classCallbackConfig)
                    classCallbackConfig.Callback?.Invoke(x);

                x.Index = index;
                index++;

                injector.Inject(x);
            });

            return goals;
        }

        private List<IWorldSensor> GetWorldSensors(IAgentTypeConfig config)
        {
            var worldSensors = _classResolver.Load<IWorldSensor, IWorldSensorConfig>(config.WorldSensors);
            var injector = _goapConfig.GoapInjector;

            worldSensors.ForEach(x =>
            {
                if (x.Config is IClassCallbackConfig classCallbackConfig)
                    classCallbackConfig.Callback?.Invoke(x);

                injector.Inject(x);
                x.Created();
            });

            return worldSensors;
        }

        private List<ITargetSensor> GetTargetSensors(IAgentTypeConfig config)
        {
            var targetSensors = _classResolver.Load<ITargetSensor, ITargetSensorConfig>(config.TargetSensors);
            var injector = _goapConfig.GoapInjector;

            targetSensors.ForEach(x =>
            {
                if (x.Config is IClassCallbackConfig classCallbackConfig)
                    classCallbackConfig.Callback?.Invoke(x);

                injector.Inject(x);
                x.Created();
            });

            return targetSensors;
        }

        private List<IMultiSensor> GetMultiSensors(IAgentTypeConfig config)
        {
            var multiSensor = _classResolver.Load<IMultiSensor, IMultiSensorConfig>(config.MultiSensors);
            var injector = _goapConfig.GoapInjector;

            multiSensor.ForEach(x =>
            {
                if (x.Config is IClassCallbackConfig classCallbackConfig)
                    classCallbackConfig.Callback?.Invoke(x);

                injector.Inject(x);
                x.Created();
            });

            return multiSensor;
        }
    }
}