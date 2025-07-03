//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class MultiSensorBuilder<T> : MultiSensorBuilder where T : IMultiSensor
    {
        public MultiSensorBuilder() : base(typeof(T))
        {
        }

        public MultiSensorBuilder<T> SetCallback(Action<T> callback)
        {
            config.Callback = (obj) => callback((T)obj);
            return this;
        }
    }

    public class MultiSensorBuilder
    {
        protected readonly MultiSensorConfig config;

        public MultiSensorBuilder(Type type)
        {
            config = new MultiSensorConfig()
            {
                Name = type.Name,
                ClassType = type.AssemblyQualifiedName,
            };
        }

        public IMultiSensorConfig Build()
        {
            return config;
        }

        public static MultiSensorBuilder<TMultiSensor> Create<TMultiSensor>()
            where TMultiSensor : IMultiSensor
        {
            return new MultiSensorBuilder<TMultiSensor>();
        }
    }
}