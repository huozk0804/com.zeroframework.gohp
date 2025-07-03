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
    public class AgentTypeBuilder
    {
        private readonly AgentTypeConfig _agentTypeConfig;

        private readonly List<CapabilityBuilder> _capabilityBuilders = new();
        private readonly List<ICapabilityConfig> _capabilityConfigs = new();

        public AgentTypeBuilder(string name)
        {
            _agentTypeConfig = new AgentTypeConfig(name);
        }

        /// <summary>
        ///     Creates a new capability with the specified name.
        /// </summary>
        /// <param name="name">The name of the capability.</param>
        /// <returns>A new instance of <see cref="CapabilityBuilder" />.</returns>
        public CapabilityBuilder CreateCapability(string name)
        {
            var capabilityBuilder = new CapabilityBuilder(name);

            _capabilityBuilders.Add(capabilityBuilder);

            return capabilityBuilder;
        }

        /// <summary>
        ///     Creates a new capability with the specified name and applies the given callback.
        /// </summary>
        /// <param name="name">The name of the capability.</param>
        /// <param name="callback">The callback to apply to the capability builder.</param>
        public void CreateCapability(string name, Action<CapabilityBuilder> callback)
        {
            var capabilityBuilder = new CapabilityBuilder(name);

            callback(capabilityBuilder);

            _capabilityBuilders.Add(capabilityBuilder);
        }

        /// <summary>
        ///     Adds a capability of the specified type.
        /// </summary>
        /// <typeparam name="TCapability">The type of the capability.</typeparam>
        public void AddCapability<TCapability>()
            where TCapability : CapabilityFactoryBase, new()
        {
            _capabilityConfigs.Add(new TCapability().Create());
        }

        /// <summary>
        ///     Adds a capability using the specified capability factory.
        /// </summary>
        /// <param name="capabilityFactory">The capability factory.</param>
        public void AddCapability(CapabilityFactoryBase capabilityFactory)
        {
            _capabilityConfigs.Add(capabilityFactory.Create());
        }

        /// <summary>
        ///     Adds a capability using the specified mono capability factory.
        /// </summary>
        /// <param name="capabilityFactory">The mono capability factory.</param>
        public void AddCapability(MonoCapabilityFactoryBase capabilityFactory)
        {
            _capabilityConfigs.Add(capabilityFactory.Create());
        }

        /// <summary>
        ///     Adds a capability using the specified scriptable capability factory.
        /// </summary>
        /// <param name="capabilityFactory">The scriptable capability factory.</param>
        public void AddCapability(ScriptableCapabilityFactoryBase capabilityFactory)
        {
            _capabilityConfigs.Add(capabilityFactory.Create());
        }

        /// <summary>
        ///     Adds a capability using the specified capability builder.
        /// </summary>
        /// <param name="capabilityBuilder">The capability builder.</param>
        public void AddCapability(CapabilityBuilder capabilityBuilder)
        {
            _capabilityConfigs.Add(capabilityBuilder.Build());
        }

        /// <summary>
        ///     Adds a capability using the specified capability config.
        /// </summary>
        /// <param name="capabilityConfig">The capability config.</param>
        public void AddCapability(ICapabilityConfig capabilityConfig)
        {
            _capabilityConfigs.Add(capabilityConfig);
        }

        /// <summary>
        ///     Builds the agent type configuration.
        /// </summary>
        /// <returns>The built <see cref="AgentTypeConfig" />.</returns>
        public AgentTypeConfig Build()
        {
            _capabilityConfigs.AddRange(_capabilityBuilders.Select(x => x.Build()));

            _agentTypeConfig.Actions = _capabilityConfigs.SelectMany(x => x.Actions).ToList();
            _agentTypeConfig.Goals = _capabilityConfigs.SelectMany(x => x.Goals).ToList();
            _agentTypeConfig.TargetSensors = _capabilityConfigs.SelectMany(x => x.TargetSensors).ToList();
            _agentTypeConfig.WorldSensors = _capabilityConfigs.SelectMany(x => x.WorldSensors).ToList();
            _agentTypeConfig.MultiSensors = _capabilityConfigs.SelectMany(x => x.MultiSensors).ToList();

            return _agentTypeConfig;
        }
    }
}
