//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    public class WorldSensorConfig<TSensor> : WorldSensorConfig
        where TSensor : IWorldSensor
    {
        public WorldSensorConfig()
        {
            Name = typeof(TSensor).Name;
            ClassType = typeof(TSensor).AssemblyQualifiedName;
        }

        public WorldSensorConfig(string name)
        {
            Name = name;
            ClassType = typeof(TSensor).AssemblyQualifiedName;
        }
    }

    public class WorldSensorConfig : IWorldSensorConfig, IClassCallbackConfig
    {
        public string Name { get; set; }
        public string ClassType { get; set; }
        public IWorldKey Key { get; set; }
        public Action<object> Callback { get; set; }
    }
}
