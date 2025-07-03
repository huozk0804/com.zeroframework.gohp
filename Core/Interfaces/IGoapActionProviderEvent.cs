//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface IGoapActionProviderEvent
    {
        void NoActionFound(IGoal goal);

        // Goals
        event GoalDelegate OnGoalStart;
        void GoalStart(IGoal goal);

        event GoalDelegate OnGoalCompleted;
        void GoalCompleted(IGoal goal);
    }
}
