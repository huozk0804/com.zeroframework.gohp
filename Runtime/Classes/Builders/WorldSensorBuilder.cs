//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class WorldSensorBuilder<T> : WorldSensorBuilder
        where T : IWorldSensor
    {
        public WorldSensorBuilder(WorldKeyBuilder worldKeyBuilder) : base(typeof(T), worldKeyBuilder)
        {
        }

        public WorldSensorBuilder<T> SetKey<TWorldKey>()
            where TWorldKey : IWorldKey
        {
            config.Key = worldKeyBuilder.GetKey<TWorldKey>();

            return this;
        }

        public WorldSensorBuilder<T> SetCallback(Action<T> callback)
        {
            config.Callback = (obj) => callback((T)obj);
            return this;
        }
    }

    public class WorldSensorBuilder
    {
        protected readonly WorldKeyBuilder worldKeyBuilder;
        protected readonly WorldSensorConfig config;

        public WorldSensorBuilder(Type type, WorldKeyBuilder worldKeyBuilder)
        {
            this.worldKeyBuilder = worldKeyBuilder;
            config = new WorldSensorConfig()
            {
                Name = type.Name,
                ClassType = type.AssemblyQualifiedName,
            };
        }

        public IWorldSensorConfig Build()
        {
            return config;
        }

        public static WorldSensorBuilder<TWorldSensor> Create<TWorldSensor>(WorldKeyBuilder worldKeyBuilder)
            where TWorldSensor : IWorldSensor
        {
            return new WorldSensorBuilder<TWorldSensor>(worldKeyBuilder);
        }
    }
}