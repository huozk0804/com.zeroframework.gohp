//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    public abstract class GlobalWorldSensorBase : IGlobalWorldSensor
    {
        public IWorldKey Key => Config.Key;
        public virtual ISensorTimer Timer => SensorTimer.Always;
        public IWorldSensorConfig Config { get; private set; }
        public void SetConfig(IWorldSensorConfig config) => Config = config;

        /// <summary>
        ///     Called when the sensor is created.
        /// </summary>
        public abstract void Created();

        public Type[] GetKeys() => new[] { Key.GetType() };

        /// <summary>
        ///     Senses the world data using this sensor. Don't override this method.
        /// </summary>
        /// <param name="data">The world data.</param>
        public void Sense(IWorldData data)
        {
            var state = data.GetWorldState(Key.GetType());

            if (!Timer.ShouldSense(state?.Timer))
                return;

            data.SetState(Key, Sense());
        }

        /// <summary>
        ///     Senses the world data.
        /// </summary>
        /// <returns>The sensed value.</returns>
        public abstract SenseValue Sense();
    }
}
