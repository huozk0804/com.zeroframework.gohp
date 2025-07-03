//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public abstract class LocalTargetSensorBase : ILocalTargetSensor
    {
        public ITargetKey Key => Config.Key;
        public ITargetSensorConfig Config { get; private set; }
        public virtual ISensorTimer Timer => SensorTimer.Always;
        public void SetConfig(ITargetSensorConfig config) => Config = config;

        public abstract void Created();
        public abstract void Update();
        public Type[] GetKeys() => new[] { Key.GetType() };

        /// <summary>
        ///     Senses the world data using this sensor. Don't override this method, override the other Sense method instead.
        /// </summary>
        /// <param name="worldData">The world data.</param>
        /// <param name="agent">The action receiver.</param>
        /// <param name="references">The component references.</param>
        public void Sense(IWorldData worldData, IActionReceiver agent, IComponentReference references)
        {
            var state = worldData.GetTargetState(Key.GetType());

            if (!Timer.ShouldSense(state?.Timer))
                return;

            worldData.SetTarget(Key, Sense(agent, references, state?.Value));
        }

        /// <summary>
        ///     Senses the world data using the specified action receiver, component references, and existing target.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="references">Use this to get cached component references on the agent.</param>
        /// <param name="existingTarget">The existing target. (The previously returned instance by this sensor).</param>
        /// <returns>The sensed target.</returns>
        public abstract ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget);
    }
}