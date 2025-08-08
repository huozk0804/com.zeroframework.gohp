//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public abstract class GlobalTargetSensorBase : IGlobalTargetSensor
    {
        private Type _key;
        public ITargetKey Key => Config.Key;
        public virtual ISensorTimer Timer => SensorTimer.Always;
        public ITargetSensorConfig Config { get; private set; }
        public void SetConfig(ITargetSensorConfig config) => Config = config;
        public Type[] GetKeys() => new[] { Key.GetType() };

        /// <summary>
        ///     Called when the sensor is created.
        /// </summary>
        public abstract void Created();

        /// <summary>
        ///     Senses the world data using this sensor. Do not override this method.
        /// </summary>
        /// <param name="worldData">The world data.</param>
        public void Sense(IWorldData worldData)
        {
            var state = worldData.GetTargetState(Key.GetType());

            if (!Timer.ShouldSense(state?.Timer))
                return;

            worldData.SetTarget(Key, Sense(state?.Value));
        }

        /// <summary>
        ///     Senses the world data using the specified existing target.
        /// </summary>
        /// <param name="target">The existing target. (The previously returned instance by this sensor).</param>
        /// <returns>The sensed target.</returns>
        public abstract ITarget Sense(ITarget target);
    }
}