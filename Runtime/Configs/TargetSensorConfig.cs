//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class TargetSensorConfig<TSensor> : TargetSensorConfig
        where TSensor : ITargetSensor
    {
        public TargetSensorConfig()
        {
            Name = typeof(TSensor).Name;
            ClassType = typeof(TSensor).AssemblyQualifiedName;
        }

        public TargetSensorConfig(string name)
        {
            Name = name;
            ClassType = typeof(TSensor).AssemblyQualifiedName;
        }
    }

    public class TargetSensorConfig : ITargetSensorConfig, IClassCallbackConfig
    {
        public string Name { get; set; }
        public string ClassType { get; set; }
        public ITargetKey Key { get; set; }
        public Action<object> Callback { get; set; }
    }
}
