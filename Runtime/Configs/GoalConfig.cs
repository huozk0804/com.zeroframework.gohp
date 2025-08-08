//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Keystone.Goap
{
    [Serializable]
    public class GoalConfig : IGoalConfig, IClassCallbackConfig
    {
        public GoalConfig() { }

        public GoalConfig(Type type)
        {
            Name = type.Name;
            ClassType = type.AssemblyQualifiedName;
        }

        public string Name { get; set; }
        public string ClassType { get; set; }
        public float BaseCost { get; set; }
        public List<ICondition> Conditions { get; set; } = new();
        public Action<object> Callback { get; set; }

        public static GoalConfig Create<TGoal>()
            where TGoal : IGoal
        {
            return new GoalConfig(typeof(TGoal));
        }
    }
}
