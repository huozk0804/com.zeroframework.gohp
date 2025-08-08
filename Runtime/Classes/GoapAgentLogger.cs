//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class GoapAgentLogger : LoggerBase<IMonoGoapActionProvider>
    {
        public override string Name => source.name;

        protected override void RegisterEvents()
        {
            if (source == null)
                return;

            // Todo
            source.Events.OnNoActionFound += NoActionFound;
            source.Events.OnGoalStart += GoalStart;
            source.Events.OnGoalCompleted += GoalCompleted;
        }

        protected override void UnregisterEvents()
        {
            if (source == null)
                return;

            // Todo
            source.Events.OnNoActionFound -= NoActionFound;
            source.Events.OnGoalStart -= GoalStart;
            source.Events.OnGoalCompleted -= GoalCompleted;
        }

        private void NoActionFound(IGoalRequest request)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Warning($"No action found for goals {string.Join(", ", request.Goals.Select(x => x.GetType().GetGenericTypeName()))}");
        }

        private void GoalStart(IGoal goal)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Log($"Goal {goal?.GetType().GetGenericTypeName()} started");
        }

        private void GoalCompleted(IGoal goal)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Log($"Goal {goal?.GetType().GetGenericTypeName()} completed");
        }
    }
}
