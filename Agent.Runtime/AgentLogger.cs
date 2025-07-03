//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    public class AgentLogger : LoggerBase<IMonoAgent>
    {
        public override string Name => source.name;

        protected override void RegisterEvents()
        {
            if (source == null)
                return;

            source.Events.OnActionStart += ActionStart;
            source.Events.OnActionStop += ActionStop;
            source.Events.OnActionComplete += ActionComplete;
        }

        protected override void UnregisterEvents()
        {
            if (source == null)
                return;

            source.Events.OnActionStart -= ActionStart;
            source.Events.OnActionStop -= ActionStop;
            source.Events.OnActionComplete -= ActionComplete;
        }

        private void ActionStart(IAction action)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Log($"Action {action?.GetType().GetGenericTypeName()} started");
        }

        private void ActionStop(IAction action)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Log($"Action {action?.GetType().GetGenericTypeName()} stopped");
        }

        private void ActionComplete(IAction action)
        {
            if (config.DebugMode == DebugMode.None)
                return;

            Log($"Action {action?.GetType().GetGenericTypeName()} completed");
        }
    }
}
