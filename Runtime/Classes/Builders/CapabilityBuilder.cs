//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class CapabilityBuilder
    {
        private readonly CapabilityConfig _capabilityConfig;
        private readonly List<ActionBuilder> _actionBuilders = new();
        private readonly List<GoalBuilder> _goalBuilders = new();
        private readonly List<TargetSensorBuilder> _targetSensorBuilders = new();
        private readonly List<WorldSensorBuilder> _worldSensorBuilders = new();
        private readonly List<MultiSensorBuilder> _multiSensorBuilders = new();
        private readonly WorldKeyBuilder _worldKeyBuilder = new();
        private readonly TargetKeyBuilder _targetKeyBuilder = new();

        public CapabilityBuilder(string name)
        {
            _capabilityConfig = new CapabilityConfig(name);
        }

        /// <summary>
        ///     Adds an action to the capability.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <returns>An instance of <see cref="ActionBuilder{TAction}" />.</returns>
        public ActionBuilder<TAction> AddAction<TAction>() where TAction : IAction
        {
            var actionBuilder = ActionBuilder.Create<TAction>(_worldKeyBuilder, _targetKeyBuilder);

            _actionBuilders.Add(actionBuilder);

            return actionBuilder;
        }

        /// <summary>
        ///     Adds a goal to the capability.
        /// </summary>
        /// <typeparam name="TGoal">The type of the goal.</typeparam>
        /// <returns>An instance of <see cref="GoalBuilder{TGoal}" />.</returns>
        public GoalBuilder<TGoal> AddGoal<TGoal>() where TGoal : IGoal
        {
            var goalBuilder = GoalBuilder.Create<TGoal>(_worldKeyBuilder);

            _goalBuilders.Add(goalBuilder);

            return goalBuilder;
        }

        /// <summary>
        ///     Adds a world sensor to the capability.
        /// </summary>
        /// <typeparam name="TWorldSensor">The type of the world sensor.</typeparam>
        /// <returns>An instance of <see cref="WorldSensorBuilder{TWorldSensor}" />.</returns>
        public WorldSensorBuilder<TWorldSensor> AddWorldSensor<TWorldSensor>()
            where TWorldSensor : IWorldSensor
        {
            var worldSensorBuilder = WorldSensorBuilder.Create<TWorldSensor>(_worldKeyBuilder);

            _worldSensorBuilders.Add(worldSensorBuilder);

            return worldSensorBuilder;
        }

        /// <summary>
        ///     Adds a target sensor to the capability.
        /// </summary>
        /// <typeparam name="TTargetSensor">The type of the target sensor.</typeparam>
        /// <returns>An instance of <see cref="TargetSensorBuilder{TTargetSensor}" />.</returns>
        public TargetSensorBuilder<TTargetSensor> AddTargetSensor<TTargetSensor>()
            where TTargetSensor : ITargetSensor
        {
            var targetSensorBuilder = TargetSensorBuilder.Create<TTargetSensor>(_targetKeyBuilder);

            _targetSensorBuilders.Add(targetSensorBuilder);

            return targetSensorBuilder;
        }

        /// <summary>
        ///     Adds a multi sensor to the capability.
        /// </summary>
        /// <typeparam name="TMultiSensor">The type of the multi sensor.</typeparam>
        /// <returns>An instance of <see cref="MultiSensorBuilder{TMultiSensor}" />.</returns>
        public MultiSensorBuilder<TMultiSensor> AddMultiSensor<TMultiSensor>()
            where TMultiSensor : IMultiSensor
        {
            var multiSensorBuilder = MultiSensorBuilder.Create<TMultiSensor>();

            _multiSensorBuilders.Add(multiSensorBuilder);

            return multiSensorBuilder;
        }

        public WorldKeyBuilder GetWorldKeyBuilder()
        {
            return _worldKeyBuilder;
        }

        /// <summary>
        ///     Builds the capability configuration.
        /// </summary>
        /// <returns>The built <see cref="CapabilityConfig" />.</returns>
        public CapabilityConfig Build()
        {
            _capabilityConfig.Actions = _actionBuilders.Select(x => x.Build()).ToList();
            _capabilityConfig.Goals = _goalBuilders.Select(x => x.Build()).ToList();
            _capabilityConfig.TargetSensors = _targetSensorBuilders.Select(x => x.Build()).ToList();
            _capabilityConfig.WorldSensors = _worldSensorBuilders.Select(x => x.Build()).ToList();
            _capabilityConfig.MultiSensors = _multiSensorBuilders.Select(x => x.Build()).ToList();

            return _capabilityConfig;
        }
    }
}