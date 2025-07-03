//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public interface IGoalRequest
    {
        List<IGoal> Goals { get; }
        public string Key { get; set; }
    }

    public interface IGoalResult
    {
        IGoal Goal { get; }
        IConnectable[] Plan { get; }
        IGoapAction Action { get; }
    }
}