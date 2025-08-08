//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace Keystone.Goap
{
    public class AgentTypeConfig : IAgentTypeConfig
    {
        public AgentTypeConfig(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public List<IActionConfig> Actions { get; set; }
        public List<IGoalConfig> Goals { get; set; }
        public List<ITargetSensorConfig> TargetSensors { get; set; }
        public List<IWorldSensorConfig> WorldSensors { get; set; }
        public List<IMultiSensorConfig> MultiSensors { get; set; }
    }

    public class CapabilityConfig : ICapabilityConfig
    {
        public CapabilityConfig(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public List<IActionConfig> Actions { get; set; }
        public List<IGoalConfig> Goals { get; set; }
        public List<ITargetSensorConfig> TargetSensors { get; set; }
        public List<IWorldSensorConfig> WorldSensors { get; set; }
        public List<IMultiSensorConfig> MultiSensors { get; set; }
    }
}
