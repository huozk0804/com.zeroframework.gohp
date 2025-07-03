//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public interface IAgentTypeConfig : IConfig
    {
        public List<IActionConfig> Actions { get; }
        public List<IGoalConfig> Goals { get; }

        public List<ITargetSensorConfig> TargetSensors { get; }
        public List<IWorldSensorConfig> WorldSensors { get; }
        public List<IMultiSensorConfig> MultiSensors { get; }
    }

    public interface ICapabilityConfig : IAgentTypeConfig
    {
    }
}