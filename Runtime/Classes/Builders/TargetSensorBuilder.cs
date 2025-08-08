//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    public class TargetSensorBuilder<T> : TargetSensorBuilder where T : ITargetSensor
    {
        public TargetSensorBuilder(TargetKeyBuilder targetKeyBuilder) : base(typeof(T), targetKeyBuilder)
        {
        }

        public TargetSensorBuilder<T> SetTarget<TTarget>()
            where TTarget : ITargetKey
        {
            config.Key = targetKeyBuilder.GetKey<TTarget>();

            return this;
        }

        public TargetSensorBuilder<T> SetCallback(Action<T> callback)
        {
            config.Callback = (obj) => callback((T)obj);
            return this;
        }
    }

    public class TargetSensorBuilder
    {
        protected readonly TargetKeyBuilder targetKeyBuilder;
        protected readonly TargetSensorConfig config;

        public TargetSensorBuilder(Type type, TargetKeyBuilder targetKeyBuilder)
        {
            this.targetKeyBuilder = targetKeyBuilder;
            config = new TargetSensorConfig()
            {
                Name = type.Name,
                ClassType = type.AssemblyQualifiedName,
            };
        }

        public ITargetSensorConfig Build()
        {
            return config;
        }

        public static TargetSensorBuilder<TTargetSensor> Create<TTargetSensor>(TargetKeyBuilder targetKeyBuilder)
            where TTargetSensor : ITargetSensor
        {
            return new TargetSensorBuilder<TTargetSensor>(targetKeyBuilder);
        }
    }
}