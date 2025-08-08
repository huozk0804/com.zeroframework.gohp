//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public abstract class LocalWorldSensorBase : ILocalWorldSensor
    {
        public IWorldKey Key => Config.Key;
        public virtual ISensorTimer Timer => SensorTimer.Always;
        public IWorldSensorConfig Config { get; private set; }
        public void SetConfig(IWorldSensorConfig config) => Config = config;

        /// <summary>
        ///     Called when the sensor is created.
        /// </summary>
        public abstract void Created();

        /// <summary>
        ///     Called when the sensor needs to update. Use this for caching data.
        /// </summary>
        public abstract void Update();

        public Type[] GetKeys() => new[] { Key.GetType() };

        /// <summary>
        ///     Senses the world data using this sensor.
        /// </summary>
        /// <param name="data">The world data.</param>
        /// <param name="agent">The action receiver.</param>
        /// <param name="references">Use this to get cached component references on the agent</param>
        public void Sense(IWorldData data, IActionReceiver agent, IComponentReference references)
        {
            var state = data.GetWorldState(Key.GetType());

            if (!Timer.ShouldSense(state?.Timer))
                return;

            data.SetState(Key, Sense(agent, references));
        }

        /// <summary>
        ///     Senses the world data using the specified action receiver and component references.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="references">Use this to get cached component references on the agent.</param>
        /// <returns>The sensed value.</returns>
        public virtual SenseValue Sense(IActionReceiver agent, IComponentReference references)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return Sense(agent as IMonoAgent, references);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}