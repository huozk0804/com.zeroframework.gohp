//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public class GoalRequest : IGoalRequest
    {
        public List<IGoal> Goals { get; set; } = new();
        public string Key { get; set; }
    }

    public class GoalResult : IGoalResult
    {
        public IGoal Goal { get; set; }
        public IConnectable[] Plan { get; set; }
        public IGoapAction Action { get; set; }
    }
}
